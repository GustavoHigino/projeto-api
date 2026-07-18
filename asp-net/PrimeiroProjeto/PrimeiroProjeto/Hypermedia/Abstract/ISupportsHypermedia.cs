using PrimeiroProjeto.Hypermedia.Filters;

namespace PrimeiroProjeto.Hypermedia.Abstract
{
    public interface ISupportsHypermedia
    {
        List<HypermediaLink> Links { get; set; }
    }
}
