

using PrimeiroProjeto.Data.Converter.Impl;
using PrimeiroProjeto.Data.DTO.V2;
using PrimeiroProjeto.Model;
using PrimeiroProjeto.Repositories;

namespace PrimeiroProjeto.Services.Impl
{
    public class PersonServicesImplV2 
    {
        private IRepository<Person> _repository;
        private readonly PersonConverterV2 _converter;
        public PersonServicesImplV2
            (IRepository<Person> repository)
        {
            _repository=repository;
            _converter = new PersonConverterV2();

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

    }
}
