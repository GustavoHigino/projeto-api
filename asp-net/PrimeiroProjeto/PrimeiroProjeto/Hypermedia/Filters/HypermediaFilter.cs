using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PrimeiroProjeto.Hypermedia.Filters
{
    public class HypermediaFilter : ResultFilterAttribute
    {
        private HypermediaFilterOptions
            _hypermediaFilterOptions;
        public HypermediaFilter
            (HypermediaFilterOptions options)
        {
            _hypermediaFilterOptions = options;
        }
        public override void OnResultExecuting
            (ResultExecutingContext context)
        {
            tryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void tryEnrichResult
            (ResultExecutingContext context)
        {
            if(context.Result is 
                OkObjectResult objectResult)
            {
                var enricher =
                    _hypermediaFilterOptions
                    .ContentResponseEnricherList
                    .FirstOrDefault
                    (options =>
                    options.CanEnrich(context));
                enricher?.Enrich(context).Wait();
            }
        }
    }
}
