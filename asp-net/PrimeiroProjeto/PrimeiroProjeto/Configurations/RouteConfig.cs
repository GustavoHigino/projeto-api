namespace PrimeiroProjeto.Configurations
{
    public static class RouteConfig
    {
        public static IServiceCollection AddRouteConfig(
            this IServiceCollection collection)
        {
            collection.Configure<RouteOptions>
                (options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            return collection;
        }
    }
}
