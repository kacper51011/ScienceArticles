using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ScienceArticles.Application.env;
using ScienceArticles.Application.Queries.GetArticlesFromService;
using ScienceArticles.Application.Services;
using ScienceArticles.Application.Settings;
using ScienceArticles.Domain.Interfaces;
using ScienceArticles.Infrastructure.Db;
using ScienceArticles.Infrastructure.Repositories;
using System.Diagnostics;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//dotenv.net.DotEnv.Load();
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);
builder.Configuration.AddEnvironmentVariables();


// Add services to the container.

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));

builder.Services.AddHttpContextAccessor();




builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["JWTSettings:Audience"],
        ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:SigningKey"]))

    }); ;

var dbSettings = builder.Configuration.GetSection("Db");

var port = dbSettings["Port"];
var server = dbSettings["Server"];
var user = dbSettings["User"];
var database = dbSettings["Database"];
var password = dbSettings["Password"];


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    Debug.WriteLine($"server: {server}");
    Debug.WriteLine($"user: {user}");
    Debug.WriteLine($"passw: {password}");
    options.UseSqlServer(
        $"Data Source={server};Initial Catalog={database};User ID={user};Password={password}; TrustServerCertificate=True;Encrypt=True;MultiSubnetFailover=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("ScienceArticles.API")
        );
}
);

builder.Services.AddSingleton<IEuropePMCService, EuropePMCService>();


builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IArticleRepository, ArticleRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetArticlesFromServiceQueryHandler)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{

    //Including xml comments
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    option.IncludeXmlComments(xmlCommentsFullPath);

    option.SwaggerDoc("ScienceArticlesApiSpecification", new OpenApiInfo
    {
        Title = "Science Articles Api",
        Version = "1",
        Contact = new()
        {
            Name = "Kacper Tylec",
            Email = "kacper.tylec1999@gmail.com",
            Url = new Uri("https://github.com/kacper51011")
        },
        Description="<h3>Main capabilities of Science Articles Api:<h3/>" +
        "<ul>" +
        "<li>It is connected with open-source EuropePMC SOAP web service through wsdl file, which makes it possible to get science publications through that</li>" +
        "<li>It provides basic auth with JWT Tokens, which are used to get UserId in User Articles endpoints </li>" +
        "<li>Created with CLEAN Architecture, with .NET 8, Entity Framework Core and SQL Server </li>" +
        " <ul/>" +
        ""

    }) ;

    //security definition for swagger
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Provide a valid token after registering",
        Name = "Auth",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });


    //security requirement for swagger
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id= "Bearer"

                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint("/swagger/ScienceArticlesApiSpecification/swagger.json", "Science Articles Api");
        setup.RoutePrefix = "";
    });
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
