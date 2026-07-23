using CsvHelper;
using CsvHelper.Configuration;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Files.Exporters.Contract;
using PrimeiroProjeto.Files.Importers.Contract;
using System.Globalization;

namespace PrimeiroProjeto.Files.Importers.Impl
{
    internal class CsvImporter : IFileImporter
    {
        public async Task<List<PersonDTO>> ImportFileAsync
            (Stream fileStream)
        {
            using var reader = new
                StreamReader(fileStream,
                new System.Text.UTF8Encoding(false));
            using var csv = new CsvReader
                (reader, new CsvConfiguration
                (CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    TrimOptions = TrimOptions.Trim,
                    IgnoreBlankLines = true,
                    Delimiter = ";"

                });
            await csv.ReadAsync();
            csv.ReadHeader();
            var persons = new List<PersonDTO>();
            await foreach (var record in csv
                .GetRecordsAsync<dynamic>())
            {
                var person = new PersonDTO
                {
                    FirstName = csv.GetField("first_name"),
                    LastName = csv.GetField("last_name"),
                    Address = csv.GetField("address"),
                    Gender = csv.GetField("gender"),
                    Enabled = true
                };
                persons.Add(person);
            }
            return persons;
        }
    }
}