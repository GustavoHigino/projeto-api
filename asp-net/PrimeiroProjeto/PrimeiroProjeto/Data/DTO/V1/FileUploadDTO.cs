using System.ComponentModel.DataAnnotations;

namespace PrimeiroProjeto.Data.DTO.V1
{
    public class FileUploadDTO
    {
        [Required]
        public IFormFile File { get; set; }

    }
}
