using PrimeiroProjeto.Hypermedia.Abstract;
using PrimeiroProjeto.Hypermedia.Filters;

namespace PrimeiroProjeto.Data.DTO.V1
{ 
    public class PersonDTO : ISupportsHypermedia
    {
        public long Id { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Address {  get; set; }
        public string Gender {  get; set; }
        public bool Enabled { get; set; }
        public List<HypermediaLink> Links
        { get; set; } = [];
    }
}
