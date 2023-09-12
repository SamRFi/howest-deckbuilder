global using GraphQL.Server;
global using GraphQL.Server.Ui.Playground;
global using Howest.MagicCards.DAL.Contexts;
global using Howest.MagicCards.DAL.Repositories.Implementations;
global using Howest.MagicCards.DAL.Repositories.Interfaces;
global using Howest.MagicCards.GraphQL.GraphQL.Schemas;
global using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureDatabase(builder.Services, builder.Configuration);
ConfigureGraphQL(builder.Services);

var app = builder.Build();

app.UseGraphQL<RootSchema>();
app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions()
{
    EditorTheme = EditorTheme.Light
});

app.Run();

void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
{
    services.AddDbContext<MagicCardsDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("MagicCardsDb")));

    services.AddScoped<ICardRepository, CardRepository>();
    services.AddScoped<IArtistRepository, ArtistRepository>();
}

void ConfigureGraphQL(IServiceCollection services)
{
    services.AddScoped<RootSchema>();
    services.AddGraphQL()
            .AddGraphTypes(typeof(RootSchema), ServiceLifetime.Scoped)
            .AddDataLoader()
            .AddSystemTextJson();
}
