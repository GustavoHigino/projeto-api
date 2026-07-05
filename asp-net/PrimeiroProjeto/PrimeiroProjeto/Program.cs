using PrimeiroProjeto.Configurations;
using PrimeiroProjeto.Repositories;
using PrimeiroProjeto.Repositories.Impl;
using PrimeiroProjeto.Services;
using PrimeiroProjeto.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogLogging();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDatabaseConfiguration
    (builder.Configuration);
builder.Services.AddEvolveConfiguration(
    builder.Configuration,
    builder.Environment);
builder.Services.AddScoped<IPersonServices
    , PersonServicesImpl>();
builder.Services.AddScoped(typeof(IRepository<>), 
    typeof(GenericRepository<>));
builder.Services.AddScoped<IBookService, BookServiceImpl>();
builder.Services.AddScoped<PersonServicesImplV2>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
