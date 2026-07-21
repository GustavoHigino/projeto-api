using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Services;

namespace PrimeiroProjeto.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class FileController : ControllerBase
    {
        private readonly IFileServices _fileServices;
        private readonly ILogger<FileController>
            _logger;
        public FileController
            (IFileServices fileServices,
            ILogger<FileController> logger)
        {
            _logger = logger;
            _fileServices = fileServices;
        }
        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType(200, Type =  typeof
            (byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octet-stream")]
        public IActionResult DownloadFile(string fileName)
        {
            var buffer = _fileServices.
                GetFile(fileName);
            if (buffer == null || buffer.Length == 0)
            {
                return NoContent();
            }
            var contentType = $"application/{Path
                .GetExtension(fileName)
                .TrimStart('.')}";
            return File(buffer,
                contentType, fileName);
        }



        [HttpPost("uploadFile")]
        [ProducesResponseType(200,
            Type =typeof(FileDatailDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json"
            , "application/xml")]
        public async Task<IActionResult>UploadFile
            ([FromForm]FileUploadDTO input)
        {
            var fileDetail = await _fileServices.
                SaveFileToDisk(input.File);
            _logger.LogInformation($"File" +
                $"{fileDetail} uploaded successfully.");
            return Ok(fileDetail);


        }
        [HttpPost("uploadMultipleFiles")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(200,
            Type = typeof(List<FileDatailDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json"
            , "application/xml")]
        public async Task<IActionResult>
            UploadMultipleFiles(
            [FromForm] MultipleFilesUploadDTO input)
        {
            var details = await
                _fileServices.SaveFilesToDisk
                (input.Files);
            return Ok(details);
        }
    }
}
