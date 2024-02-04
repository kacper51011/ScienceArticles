using ScienceArticles.Application.env;
using ScienceArticles.Application.Queries.GetArticlesFromService;
using ScienceArticles.Application.Services;

var builder = WebApplication.CreateBuilder(args);

var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

// Add services to the container.


builder.Services.AddSingleton<IEuropePMCService, EuropePMCService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetArticlesFromServiceQueryHandler)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
