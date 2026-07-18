using PrimeiroProjeto.Configurations;
using PrimeiroProjeto.Hypermedia.Filters;
using PrimeiroProjeto.Repositories;
using PrimeiroProjeto.Repositories.Impl;
using PrimeiroProjeto.Services;
using PrimeiroProjeto.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogLogging();
// Add services to the container.

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<HypermediaFilter>();
    })
    .AddContentNegotiation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenAPIConfig();
builder.Services.AddSwaggerConfig();
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
builder.Services.AddRouteConfig();

builder.Services.AddCorsConfiguration
    (builder.Configuration);
builder.Services.AddHATEOASCOnfiguration();
builder.Services.AddScoped<IPersonRepository,
    PersonRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
//app.UseCorsConfiguration();
app.UseCorsConfiguration(builder.Configuration);

app.MapControllers();
app.UseHATEOASRoutes();

app.UseSwaggerSpecification();
app.UseScalarConfiguration();

app.Run();
