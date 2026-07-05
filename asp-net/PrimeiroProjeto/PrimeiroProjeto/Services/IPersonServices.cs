using PrimeiroProjeto.Data.DTO.V1;
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
    }
}
