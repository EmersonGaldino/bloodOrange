using galdino.bloodOrange.api_v1.Configurations;
using galdino.bloodOrange.api_v1.Dependency;
using galdino.bloodOrange.api_v1.Filters.Error;
using galdino.bloodOrange.api_v1.Filters.Performace;
using galdino.bloodOrange.api_v1.Filters.Security;
using galdino.bloodOrange.api_v1.Mapper;
using galdino.bloodOrange.application.shared.Confiugrations.Application;
using galdino.bloodOrange.application.shared.Interfaces.IConnections.BloodOrange;
using galdino.bloodOrange.application.shared.Interfaces.IConnections.BloodOrangeMongo;
using galdino.bloodOrange.application.shared.Interfaces.IMessages;
using galdino.bloodOrange.data.persistence.Uow.bloodOrange;
using galdino.bloodOrange.data.persistence.Uow.BloodOrangeLogs;
using galdino.bloodOrange.utils.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace galdino.bloodOrange.api_v1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();



            services.AddScoped<IConnectionBloodOrange, UnitOfWorkBloodOrange>(x =>
                new UnitOfWorkBloodOrange(Configuration.GetSection("Configuracoes").GetSection("Connection-BloodOrange").Value));          


            services.AddScoped<IMessaging, Messages>(x => new Messages(Configuration.GetSection("Messages").Value));

            services.Configure<ApplicationConfiguration>(Configuration.GetSection("Aplicacao"));

            services.AddTransient<PerformaceFilters>();
            services.AddTransient<SecurityFilter>();



            AuthConfiguration.Register(services, Configuration);
            AutoMapperConfiguration.Register(services, Configuration);
            DependencyInjectionConfiguration.Register(services, Configuration);


            services.AddMvc(options =>
            {
                options.Filters.AddService<PerformaceFilters>();
                options.Filters.AddService<SecurityFilter>();
                options.Filters.Add(typeof(ErrorFilters));
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);
            SwaggerApiVersioningConfiguration.Register(services);

            var corsBuilder = new CorsPolicyBuilder()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1.0/swagger.json", $"v1.0");


                c.DocumentTitle = "Blood Orange - FCamara";
                c.RoutePrefix = string.Empty;
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DefaultModelsExpandDepth(-1);
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableFilter();
                c.ShowExtensions();
                c.EnableValidator();
            });

            app.UseAuthentication();

            app.UseMvc();

            app.UseCors("SiteCorsPolicy");

        }
    }
}
