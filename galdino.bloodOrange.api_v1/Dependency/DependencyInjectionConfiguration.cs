using galdino.bloodOrange.application.Service.Base;
using galdino.bloodOrange.data.persistence.Repository.Base;
using galdino.bloodOrnage.application.core.Entity.User;
using galdino.bloodOrnage.application.core.Interface.IRepository.User;
using galdino.bloodOrnage.application.core.Interface.IService.User;
using galdino.bloodOrnage.application.core.Service.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace galdino.bloodOrange.api_v1.Dependency
{
    public class DependencyInjectionConfiguration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<IUnitOfWork, UnitOfWork>();

            //AppService
            RegistrarInterfaces(services, typeof(AppServiceBase<UserDomain, IUserService, IUserRepository>), "Service", "AppService");

            //Service
            RegistrarInterfaces(services, typeof(ServiceBase<UserDomain, IUserRepository>), "Service", "Service");

            //Repositorios
            RegistrarInterfaces(services, typeof(RepositoryBase<UserDomain>), "Repository", "Repository");


        }
        private static void RegistrarInterfaces(IServiceCollection services, Type typeBase, string containsInNamespace, string sulfix)
        {
            var types = typeBase
                .Assembly
                .GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace) &&
                               type.Namespace.Contains(containsInNamespace) &&
                               type.Name.EndsWith(sulfix) &&
                               !type.IsGenericType &&
                               type.IsClass &&
                               type.GetInterfaces().Any());

            foreach (var type in types)
            {
                var interfaceType = type
                    .GetInterfaces()?
                    .FirstOrDefault(t => t.Name == $"I{type.Name}");
                if (interfaceType == null)
                {
                    continue;
                }
                services.AddScoped(interfaceType, type);
            }
        }
    }
}
