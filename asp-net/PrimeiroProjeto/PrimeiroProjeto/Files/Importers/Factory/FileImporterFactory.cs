using PrimeiroProjeto.Files.Exporters.Contract;
using PrimeiroProjeto.Files.Importers.Contract;
using PrimeiroProjeto.Files.Importers.Impl;

namespace PrimeiroProjeto.Files.Importers.Factory
{
    public class FileImporterFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FileImporterFactory> _logger;
        public FileImporterFactory(
            IServiceProvider serviceProvider,
            ILogger<FileImporterFactory> logger)
        {
            _serviceProvider= serviceProvider;
            _logger= logger;
        }
        public IFileImporter GetImporter(string fileName)
        {
            if(fileName.EndsWith(".csv",
                StringComparison
                .OrdinalIgnoreCase))
            {
                _logger.LogInformation($"Selected" +
                    $" CSV file importer for file {fileName}");
                return new CsvImporter();
                //return _serviceProvider.GetRequiredService
                //    <CsvImporter>();

            }
            else if(fileName.EndsWith(".xlsx",
                StringComparison
                .OrdinalIgnoreCase))
            {
                _logger.LogInformation($"Selected" +
                   $" Excel file importer for file {fileName}");
                
                return _serviceProvider.
                    GetRequiredService<XlsxImporter>();

            }
            else
            {
                _logger.LogError($"Unsupported file" +
                    $" format {fileName}");
                throw new NotSupportedException(
                    $"the file format of {fileName}" +
                    $" is not supported.");
            }
        }
    }
}
