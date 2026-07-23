using PrimeiroProjeto.Files.Exporters.Contract;
using PrimeiroProjeto.Files.Exporters.Impl;
using PrimeiroProjeto.Files.Importers.Factory;

namespace PrimeiroProjeto.Files.Exporters.Factory
{
    public class FileExporterFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FileExporterFactory> _logger;
        public FileExporterFactory(
            IServiceProvider serviceProvider,
            ILogger<FileExporterFactory> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        public IFileExporter GetExporter(string acceptHeader)
        {
            if(string.Equals(acceptHeader,
                MediaTypes.ApplicationXlsx,
                StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation
                    ($"Selected excel file exporter" +
                    $" for media type {acceptHeader}");
                return _serviceProvider.GetService<XlsxExporter>();
            }
            else if(string.Equals(acceptHeader,
                MediaTypes.ApplicationCsv,
                StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation
                    ($"Selected csv file exporter" +
                    $" for media type {acceptHeader}");
                return _serviceProvider.GetService<CsvExporter>();
            }
            else
            {
                _logger.LogError($"Unsupported media" +
                    $" type {acceptHeader}");
                throw new NotSupportedException
                    ($"The media Type of " +
                    $"{acceptHeader} is not supported");
            }
        }
    }
}
