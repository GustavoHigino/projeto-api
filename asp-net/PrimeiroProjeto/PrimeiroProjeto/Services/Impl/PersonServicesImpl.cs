



using Mapster;
using Microsoft.AspNetCore.Mvc;

using PrimeiroProjeto.Data.Converter.Impl;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Files.Exporters.Factory;
using PrimeiroProjeto.Files.Importers.Factory;
using PrimeiroProjeto.Hypermedia.Utils;
using PrimeiroProjeto.Model;
using PrimeiroProjeto.Repositories;

namespace PrimeiroProjeto.Services.Impl
{
    public class PersonServicesImpl : IPersonServices
    {
        private IPersonRepository _repository;
        private readonly PersonConverter _converter;
        private readonly FileImporterFactory _fileImporterFactory;
        private readonly FileExporterFactory _fileExporterFactory;
        private readonly ILogger<PersonServicesImpl> _logger;
        public PersonServicesImpl
            (IPersonRepository repository,
            FileImporterFactory fileImporterFactory,
            ILogger<PersonServicesImpl> logger,
            FileExporterFactory fileExporterFactory)
        {
            _logger = logger;
            _fileImporterFactory = fileImporterFactory;
            _repository=repository;
            _converter = new PersonConverter();
            _fileExporterFactory = fileExporterFactory;

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

        public List<PersonDTO> FindByName
            (string firstName, string lastName)
        {
            return _repository.FindByName
                (firstName, lastName).Adapt
                <List<PersonDTO>>();
        }

        public PagedSearchDTO<PersonDTO> 
            FindWithPagedSearch
            (string name, string sortDirection,
            int pageSize, int page)
        {
            
            var result = _repository
                .FindWithPagedSearch(name,sortDirection,
                pageSize,page);
            

            return result
                .Adapt<PagedSearchDTO<PersonDTO>>();
        }

        public async Task<List<PersonDTO>> MassCreationAsync
            (IFormFile file)
        {
            if (file==null || file.Length == 0)
            {
                _logger.LogError("File is null or empty");
                throw new ArgumentException("File is null" +
                    " or empty.");
            }
            using var stream = 
                file.OpenReadStream();
            var fileName = file.FileName;
            try
            {
                var importer =
                    _fileImporterFactory.GetImporter
                    (fileName);
                var persons = await importer
                    .ImportFileAsync(stream);
                var entities = persons
                    .Select(dto=>_repository
                    .Create(dto.Adapt<Person>()))
                    .ToList();
                return entities.Adapt<List<PersonDTO>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during" +
                    " mass creation from " +
                    "file: {FileName}", file.FileName);
                throw;
            }
        }

        public IActionResult ExportPage(int page,
            int pageSize,
            string sortDirection,
            string acceptHeader,
            string name)
        {
            _logger.LogInformation($"exporting page:" +
                $"{page}, {pageSize}, {sortDirection}," +
                $"{acceptHeader}");
            var content = FindWithPagedSearch
                (name,sortDirection,pageSize,page);
            try
            {
                var exporter = _fileExporterFactory
                .GetExporter(acceptHeader);
                var people = content.List
                    .Adapt<List<PersonDTO>>();
                return exporter.ExportFile(people);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex,$"Unsuported export" +
                    $"format requestesd {acceptHeader}");
                throw;
            }

            
        }
    }
}
