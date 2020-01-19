using AutoMapper;
using galdino.bloodOrange.api_v1.Models.Interface;
using galdino.bloodOrange.utils.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace galdino.bloodOrange.api_v1.Mapper
{
    public class AutoMapperConfiguration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                CreateMap(mc);
                mc.AddProfile<CustomMappingProfile>();
                mc.AddProfile<DomainToModelViewProfile>();
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        private static void CreateMap(IMapperConfigurationExpression mc)
        {
            //mapeamento das viewModels tipadas
            typeof(IModelView<>).Assembly.GetTypes()?.ToList().Where(vm =>
                    vm.IsAssignableToGenericType(typeof(IModelView<>)) && !vm.IsAbstract && !vm.IsInterface
                )
                .Where(vm =>
                    !JaMapeadoNoProfile(mc, vm.GetInterface(typeof(IModelView<>).Name).GetGenericArguments()[0], vm)
                    )
                .ToList()
                .ForEach(vm =>
                {
                    mc.CreateMap(vm.GetInterface(typeof(IModelView<>).Name).GetGenericArguments()[0], vm);
                });
            //mapeamento das viewModels tipadas
            typeof(IViewModel<>).Assembly.GetTypes()?.ToList().Where(vm =>
                    vm.IsAssignableToGenericType(typeof(IViewModel<>)) && !vm.IsAbstract && !vm.IsInterface
                ).Where(vm =>
                    !JaMapeadoNoProfile(mc, vm, vm.GetInterface(typeof(IViewModel<>).Name).GetGenericArguments()[0])
                    )
                .ToList()
                .ForEach(vm =>
                {
                    mc.CreateMap(vm, vm.GetInterface(typeof(IViewModel<>).Name).GetGenericArguments()[0]);
                });
        }

        private static bool JaMapeadoNoProfile(IMapperConfigurationExpression mc, Type origem, Type destino)
        {
            return ((AutoMapper.Configuration.MapperConfigurationExpression)mc).Profiles.SelectMany(x => x.TypeMapConfigs)
                .Any(x =>
                    x.SourceType == origem && x.DestinationType == destino
                    );
        }
    }
}
