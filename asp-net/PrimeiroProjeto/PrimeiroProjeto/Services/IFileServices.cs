using PrimeiroProjeto.Data.DTO.V1;

namespace PrimeiroProjeto.Services
{
    public interface IFileServices
    {
        byte[] GetFile(string fileName);
        Task<FileDatailDTO> SaveFileToDisk(IFormFile file);
        Task<List<FileDatailDTO>> SaveFilesToDisk
            (List<IFormFile> files);
    }
}
