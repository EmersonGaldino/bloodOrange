using Dapper;
using galdino.bloodOrange.application.shared.Interfaces.IUnitOfWork;
using galdino.bloodOrnage.application.core.Exception.Table;
using galdino.bloodOrnage.application.core.Interface.IRepository.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace galdino.bloodOrange.data.persistence.Repository.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        #region Constructor
        protected readonly IUnitOfWork _uow;
        protected readonly string TableName;

        public RepositoryBase(IUnitOfWork uow)
        {
            Initiator.Initiator.RegisterModels();
            _uow = uow;
            TableName = ClassToTable(typeof(T).Name);
        }
        public IUnitOfWork GetUow()
        {
            return _uow;
        }
        #endregion

        protected string ColumnToProperty(string column)
        {
            column = Regex.Replace(column, "^str_", "_");
            column = Regex.Replace(column, "^l_", "_");

            var sb = new StringBuilder();
            var toUpper = false;

            foreach (var i in column)
            {
                if (i == '_')
                {
                    toUpper = true;
                    continue;
                }

                if (toUpper)
                {
                    sb.Append(char.ToUpper(i));
                    toUpper = false;
                }
                else
                {
                    sb.Append(i);
                }

            }

            return sb.ToString();
        }

        protected string PropertyToColumn(string property, TypeCode type)
        {
            var sb = new StringBuilder();

            switch (type)
            {
                case TypeCode.String:
                    sb.Append("str");
                    break;
                case TypeCode.Boolean:
                    sb.Append("l");
                    break;
            }

            foreach (var i in property)
            {
                if (char.IsUpper(i))
                {
                    sb.Append($"_{char.ToLower(i)}");
                }
                else
                {
                    sb.Append(i);
                }

            }

            return sb.ToString();
        }

        protected string ClassToTable(string className)
        {
            if (typeof(T).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() is TableAttribute tableAttr
            )
                return tableAttr.Name;

            var sb = new StringBuilder("tb");

            foreach (var i in className)
            {
                if (char.IsUpper(i))
                {
                    sb.Append($"_{char.ToLower(i)}");
                }
                else
                {
                    sb.Append(i);
                }

            }

            return sb.ToString();
        }

        protected string TableToClass(string tableName)
        {
            var sb = new StringBuilder();
            var toUpper = false;

            foreach (var i in Regex.Replace(tableName, "^tb", ""))
            {
                if (i == '_')
                {
                    toUpper = true;
                    continue;
                }

                if (toUpper)
                {
                    sb.Append(char.ToUpper(i));
                    toUpper = false;
                }
                else
                {
                    sb.Append(i);
                }

            }

            return sb.ToString();
        }


        public static string GetKeyField()
        {

            return typeof(T).GetProperties()
                .FirstOrDefault(x => x.GetCustomAttribute<ColumnAttribute>() != null &&
                                     x.GetCustomAttribute<KeyAttribute>() != null)
                .GetCustomAttribute<ColumnAttribute>().Name;
        }

        public static string GetInsertSql(T source, DynamicParameters parameters)
        {
            var sql = $@"INSERT INTO {GetTableName()}
                            (
                                {GetInsertFields()}
                            )
                        VALUES
                            {GetInsert(source, parameters)} 
                        RETURNING *";
            return sql;
        }

        public static string GetInsertSql(IList<T> source, DynamicParameters parameters)
        {
            var sql = $@"INSERT INTO {GetTableName()}
                            (
                                {GetInsertFields()}
                            )
                        VALUES
                            {GetInsert(source, parameters)}
                        RETURNING *";
            return sql;
        }

        public static string GetTableName()
        {
            if (typeof(T).GetCustomAttribute<TableAttribute>() == null)
                throw new TableNameNotImplementedDomainException($"{nameof(T)} nao possui {nameof(TableAttribute)}");

            var table = typeof(T).GetCustomAttribute<TableAttribute>().Name;

            return table;
        }

        public static string GetInsertFields()
        {
            var values = string.Join(",", typeof(T).GetProperties()
                .Where(
                    x => x.GetCustomAttribute(typeof(KeyAttribute)) == null &&
                         x.GetCustomAttribute(typeof(DefaultValueColumnAttribute)) == null &&
                         x.GetCustomAttribute<ColumnAttribute>() != null)
                .Select(x => x.GetCustomAttribute<ColumnAttribute>().Name));

            return values;
        }

        public static string GetFields(bool includeSuperClass = true, string alias = null)
        {
            var values = string.Join(",",
                typeof(T).GetProperties(!includeSuperClass
                        ? BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance
                        : BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.GetCustomAttribute<ColumnAttribute>() != null)
                    .Select(x =>
                        (alias != null ? $"{alias}." : "") +
                        x.GetCustomAttribute<ColumnAttribute>().Name));

            return values;
        }

        public static string GetInsert(IList<T> source, DynamicParameters parameters)
        {
            var values = string.Join(",",
                source.Select((item, idx) =>
                {
                    var fields = new List<string>();
                    FillFieldsWithoutKeyAttribute(parameters, item, fields, idx);

                    return $"({string.Join(",", fields)})";

                }).ToArray());
            return values;
        }

        public static string GetInsert(T source, DynamicParameters parameters)
        {
            var fields = new List<string>();

            FillFieldsWithoutKeyAttribute(parameters, source, fields);

            return $"({string.Join(",", fields)})";
        }

        private static void FillFieldsWithoutKeyAttribute(DynamicParameters parameters, T item, List<string> fields,
            int idx = 1)
        {
            foreach (var field in item
                .GetType()
                .GetProperties()
                .Where(x =>
                    x.GetCustomAttribute(typeof(KeyAttribute)) == null &&
                    x.GetCustomAttribute(typeof(DefaultValueColumnAttribute)) == null &&
                    x.GetCustomAttribute<ColumnAttribute>() != null))
            {
                fields.Add($"@{field.Name}_{idx}");
                parameters.Add($"@{field.Name}_{idx}", field.GetValue(item));
            }
        }

        private static string GetWhereByIds(DynamicParameters parameters, IList<object> ids)
        {
            var keyFieldName = GetKeyField();
            return string.Join(" or ",
                ids.Select((it, idx) =>
                {
                    parameters.Add($"{keyFieldName}_{idx}", it);
                    return $"{keyFieldName} = @{keyFieldName}_{idx}";
                }));
        }

        public async Task<IList<T>> ListByIdsAsync(IList<object> ids)
        {
            try
            {
                var parameters = new DynamicParameters();
                var dados = await _uow.GetConnection().QueryAsync<T>(
                    $"select {GetFields()} from {GetTableName()} where {GetWhereByIds(parameters, ids)}",
                    parameters,
                    _uow.GetTransaction());
                var ret = dados.AsList();
                return ret;
            }
            finally
            {
                _uow.Release();
            }
        }

        protected static string GetColumnName(string fieldName)
        {
            if (typeof(T).GetProperty(fieldName) == null)
            {
                throw new Exception();
            }
            var columnAttribute = typeof(T).GetProperty(fieldName).GetCustomAttribute<ColumnAttribute>()?.Name;
            return columnAttribute ?? fieldName;
        }

      
        protected void ShallowClone(T souce, T destiny)
        {
            var properties = GetPublicAndNotVirtualProperties(souce.GetType());

            foreach (var fields in properties)
            {
                var propertyDestiny = destiny.GetType().GetProperty(fields.Name);
                propertyDestiny.SetValue(destiny, fields.GetValue(souce));
            }
        }
     
        protected void ShallowClone(IList<T> souce, IList<T> destiny)
        {
            var totalItemsSource = ((IList)souce).Count;
            var totalItemsDestiny = ((IList)destiny).Count;

            if (totalItemsSource != totalItemsDestiny)
                throw new Exception($"{nameof(souce)} and {nameof(destiny)} have different sizes");

            var properties = GetPublicAndNotVirtualProperties(souce.GetType().GetGenericArguments()[0]);

            for (int i = 0; i < totalItemsSource; i++)
            {
                foreach (var fields in properties)
                {
                    var propertyDestiny = destiny[i].GetType().GetProperty(fields.Name);
                    propertyDestiny.SetValue(destiny[i], fields.GetValue(souce[i]));
                }
            }
        }
       
        internal IList<PropertyInfo> GetPublicAndNotVirtualProperties(Type type)
        {
            return type.GetProperties()
                .Where(p => p.GetMethod.IsPublic && !p.GetMethod.IsVirtual).ToList();
        }

        public async Task SaveAsync(T item)
        {
            try
            {
                var parameters = new DynamicParameters();
                var sql = GetInsertSql(item, parameters);
                var retorno = await _uow.GetConnection().QueryFirstAsync<T>(sql, parameters, _uow.GetTransaction());
                ShallowClone(retorno, item);
            }
            finally
            {
                _uow.Release();
            }
        }

        public async Task SaveAsync(IList<T> itens)
        {
            try
            {
                var parameters = new DynamicParameters();
                var sql = GetInsertSql(itens, parameters);
                var data = await _uow.GetConnection().QueryAsync<T>(sql, parameters, _uow.GetTransaction());
                ShallowClone(data.AsList(), itens);
            }
            finally
            {
                _uow.Release();
            }
        }


    }
}
