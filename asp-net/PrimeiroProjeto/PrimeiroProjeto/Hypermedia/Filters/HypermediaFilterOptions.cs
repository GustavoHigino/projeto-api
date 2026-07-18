using PrimeiroProjeto.Hypermedia.Abstract;

namespace PrimeiroProjeto.Hypermedia.Filters
{
    public class HypermediaFilterOptions
    {
        public List<IResponseEnricher>
            ContentResponseEnricherList
        {  get; set; } = [];
    }
}
