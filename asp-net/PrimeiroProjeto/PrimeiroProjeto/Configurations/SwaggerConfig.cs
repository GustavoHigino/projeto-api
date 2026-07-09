using Microsoft.OpenApi;

namespace PrimeiroProjeto.Configurations
{
    public static class SwaggerConfig
    {
        private static readonly string AppName =
            "ASP.NET 2026 REST API's from 0 to Azure" +
            " and GCP with .NET 10, Docker e Kubernetes";
        private static readonly string AppDescription =
            $"REST API RESTful developed in course " +
            $"{AppName}";

        public static IServiceCollection AddSwaggerConfig
            (this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new
                    OpenApiInfo
                {
                    Title = AppName,
                    Version = "v1",
                    Description = AppDescription,
                    Contact = new OpenApiContact
                    {
                        Name = "Gustavo",
                        Url = new Uri("https://www.google.com/"),
                        Email="contato@blablablal,blabla"

                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://www.google.com/")
                    }

                });
                options.CustomSchemaIds(Type =>
                Type.FullName);
            }
            );
            return services;
        }
        public static IApplicationBuilder 
            UseSwaggerSpecification
            (this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "v1");
                options.RoutePrefix = "swagger-ui";
                options.DocumentTitle = AppName;
            });
            return app;
        }
    }
}
