using PrimeiroProjeto.Data.DTO.V1;

namespace PrimeiroProjeto.Files.Importers.Contract
{
    public interface IFileImporter
    {
        Task<List<PersonDTO>> ImportFileAsync(Stream
            fileStream);
    }
}
