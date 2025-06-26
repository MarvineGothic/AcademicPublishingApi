using AcademicPublishingApi.Data;
using AcademicPublishingApi.Data.Migrations.InMemoryData;
using AcademicPublishingApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure the in-memory database.
builder.Services.AddAppDbContext("AcademicDb");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IResearchArticleRepository, ResearchArticleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.SeedInMemoryData();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }