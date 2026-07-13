using PrimeiroProjeto.Configurations;
using System;
using System.Collections.Generic;
using System.Text;
using Testcontainers.MsSql;

namespace PrimeiroProjeto.Tests.IntegrationTests.Tools
{
    public class SqlServerFixture : IAsyncLifetime
    {
        public MsSqlContainer Container { get; }
        public string ConnectionString =>
            Container.GetConnectionString();
        public SqlServerFixture()
        {
            Container = new MsSqlBuilder()
                .WithPassword("@Admin123")
                .Build();
        }
        public async Task InitializeAsync()
        {
            await Container.StartAsync();
            EvolveConfig.ExecuteMigrations
                (ConnectionString);
        }
        public async Task DisposeAsync()
        {
            await Container.DisposeAsync();
        }

    }
}
