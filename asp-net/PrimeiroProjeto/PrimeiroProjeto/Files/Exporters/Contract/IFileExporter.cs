using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Data.DTO.V1;

namespace PrimeiroProjeto.Files.Exporters.Contract
{
    public interface IFileExporter
    {
        FileContentResult ExportFile(List<PersonDTO> people);

    }
}
