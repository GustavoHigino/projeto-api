using PrimeiroProjeto.Hypermedia.Abstract;
using System.Xml.Serialization;

namespace PrimeiroProjeto.Model
{
    public class PagedSearch<T> 
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set;}
        public string SortDirections { get; set; } = "asc";
        public int TotalResult { get; set; }
        public List<T> List { get; set; }
        
    }
}
