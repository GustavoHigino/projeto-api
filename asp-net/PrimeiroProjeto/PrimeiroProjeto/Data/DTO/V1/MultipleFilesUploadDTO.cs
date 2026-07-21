using System.ComponentModel.DataAnnotations;

namespace PrimeiroProjeto.Data.DTO.V1
{
    public class MultipleFilesUploadDTO
    {
        [Required]
        public List<IFormFile> Files { get; set; }
    }
}
