using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

var authSettings = builder.Configuration.GetSection("JWTSettings");


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

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddSingleton<IEuropePMCService, EuropePMCService>();


builder.Services.AddTransient<IUserRepository, UserRepository>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
