

using PrimeiroProjeto.Data.Converter.Impl;
using PrimeiroProjeto.Data.DTO;
using PrimeiroProjeto.Model;
using PrimeiroProjeto.Repositories;

namespace PrimeiroProjeto.Services.Impl
{
    public class PersonServicesImpl : IPersonServices
    {
        private IRepository<Person> _repository;
        private readonly PersonConverter _converter;
        public PersonServicesImpl
            (IRepository<Person> repository)
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

        
    }
}
