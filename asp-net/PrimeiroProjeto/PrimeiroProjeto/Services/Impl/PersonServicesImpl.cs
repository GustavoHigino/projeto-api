

using Mapster;
using PrimeiroProjeto.Data.Converter.Impl;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Model;
using PrimeiroProjeto.Repositories;

namespace PrimeiroProjeto.Services.Impl
{
    public class PersonServicesImpl : IPersonServices
    {
        private IPersonRepository _repository;
        private readonly PersonConverter _converter;
        public PersonServicesImpl
            (IPersonRepository repository)
        {
            _repository=repository;
            _converter = new PersonConverter();

        }

        public List<PersonDTO> FindAll()
        {

            return _converter.ParseList
                (_repository.FindAll().ToList());
                
        }
        public PersonDTO FindById(long id)
        {


            return _converter.Parse(
                _repository.FindById(id));
        }
        public PersonDTO Create(PersonDTO person)
        {

            var entity = _converter.Parse
                (person);
            var create = 
                _repository.Create(entity);
            return _converter.Parse(create);
        }
        public PersonDTO Update(PersonDTO person)
        {
            var entity = _converter
                .Parse(person);

            var update=_repository.Update(entity);
            return _converter.Parse(update);

        }

        public void Delete(long id)
        {
            _repository.Delete(id);
            
        }

        public PersonDTO Disable(long id)
        {
            var entity = _repository
                .Disable(id);
            return entity.Adapt<PersonDTO>();

        }
    }
}
