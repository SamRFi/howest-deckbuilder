using FluentValidation.AspNetCore;
using Howest.MagicCards.DAL.Contexts;
using Howest.MagicCards.DAL.Repositories.Implementations;
using Howest.MagicCards.DAL.Repositories.Interfaces;
using Howest.MagicCards.Shared.Mapping;
using Howest.MagicCards.Shared.Validation;
using Howest.MagicCards.WebAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile))
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CardDetailDtoValidator>());
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddMemoryCache();

builder.Services.Configure<PagingOptions>(builder.Configuration.GetSection("PagingOptions"));


builder.Services.AddDbContext<MagicCardsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MagicCardsDb")));


builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 1);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1.1", new OpenApiInfo { Title = "Magic Cards API", Version = "1.1" });
    c.SwaggerDoc("v1.5", new OpenApiInfo { Title = "Magic Cards API", Version = "1.5" });
});


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "Magic Cards API V1.1");
        c.SwaggerEndpoint("/swagger/v1.5/swagger.json", "Magic Cards API V1.5");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
