using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Provide a valid token after registering",
        Name = "Auth",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

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
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
