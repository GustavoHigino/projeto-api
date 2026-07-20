namespace PrimeiroProjeto.Configurations
{
    public static class CorsConfig
    {
        private static string[] GetAllowedOrigins
            (IConfiguration configuration)
        {
            return configuration.GetSection
                ("Cors:Origins")
                .Get<string[]>() ?? Array.Empty<string>();
        }
        public static void AddCorsConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var origins =
                GetAllowedOrigins(configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("LocalPolicy",
                    policy =>
                    policy.WithOrigins(
                        "http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );
                options.AddPolicy
                ("MultipleOriginPolicy",
                    policy =>
                    policy.WithOrigins(
                        origins
                        )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );
                options.AddPolicy
                ("DefaultPolicy",
                    policy =>
                    policy.WithOrigins(
                        
                        )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );
            });
        }
        public static IApplicationBuilder UseCorsConfiguration(
            this IApplicationBuilder app,
            IConfiguration configuration)
        {
            var origins =
                GetAllowedOrigins(configuration);
            app.Use(async
                (context, next) =>
            {
                var selfOrigin = $"{context.Request.Scheme}://{context.Request.Host}";
                var origin = context
                .Request.Headers["Origin"].ToString();
                if (!string.IsNullOrEmpty(origin)&&
                    !origin.Equals(selfOrigin,
                    StringComparison
                    .OrdinalIgnoreCase) &&
                     !origins.Contains(origin
                    , StringComparer
                    .OrdinalIgnoreCase)
                    )
                {
                    context.Response.StatusCode =
                    StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync(
                        "Cors origin not allowed.");
                    return;
                }
                await next();
            });
            //app.UseCors();
            app.UseCors("DefaultPolicy");
            return app;
        }
    }
}
