using PrimeiroProjeto.Hypermedia.Enricher;
using PrimeiroProjeto.Hypermedia.Filters;

namespace PrimeiroProjeto.Configurations
{
    public static class HATEOASConfig
    {
        public static IServiceCollection
            AddHATEOASCOnfiguration(
            this IServiceCollection services)
        {
            var filterOptions =
                new HypermediaFilterOptions();
            filterOptions.ContentResponseEnricherList
                .Add(new PersonEnricher())
                ;
            filterOptions.ContentResponseEnricherList.
                Add(new BookEnricher());
            services.AddSingleton
                (filterOptions);
            services.AddScoped<HypermediaFilter>();
            return services;
        }
        public static void UseHATEOASRoutes(
            this IEndpointRouteBuilder app)
        {
            app.MapControllerRoute
                ("Default", 
                "{controller=values}/v1/{id?}");
        }
    }
}
