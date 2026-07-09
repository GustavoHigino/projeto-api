using Microsoft.OpenApi;

namespace PrimeiroProjeto.Configurations
{
    public static class OpenAPIConfig
    {
        private static readonly string AppName = 
            "ASP.NET 2026 REST API's from 0 to Azure" +
            " and GCP with .NET 10, Docker e Kubernetes";
        private static readonly string AppDescription =
            $"REST API RESTful developed in course " +
            $"{AppName}";
        public static IServiceCollection AddOpenAPIConfig
            (this IServiceCollection services)
        {
            services.AddSingleton
                (new OpenApiInfo
                {
                    Title = AppName,
                    Version = "v1",
                    Description = AppDescription,
                    Contact = new OpenApiContact
                    {
                        Name = "Gustavo",
                        Url = new Uri(@"https://www.google.com/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri(@"https://www.google.com/")
                    }
                });
            return services;
        }
    }
}
