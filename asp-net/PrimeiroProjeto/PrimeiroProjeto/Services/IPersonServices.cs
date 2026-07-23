using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Hypermedia.Utils;
using PrimeiroProjeto.Model;

namespace PrimeiroProjeto.Services
{
    public interface IPersonServices
    {
        PersonDTO Create(PersonDTO person);
        PersonDTO FindById(long id);
        List<PersonDTO> FindAll();
        PersonDTO Update(PersonDTO person);
        void Delete(long id);

        PersonDTO Disable(long id);
        List<PersonDTO> FindByName(string firstName,
            string lastName);
        PagedSearchDTO<PersonDTO> FindWithPagedSearch
            (string name, string sortDirection,
            int pageSize, int page);
        Task<List<PersonDTO>>
            MassCreationAsync(
            IFormFile file);

        IActionResult ExportPage(
            int page,
            int pageSize,
            string sortDirection,
            string acceptHeader,
            string name);
    }
}
