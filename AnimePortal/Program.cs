using AnimePortalAuthServer.Extension;
using AnimePortalAuthServer.Extensions;
using AnimePortalAuthServer.Middlewares;
using BLL;
using BLL.Abstractions.Interfaces;
using BLL.Jwt;
using CloudinaryDotNet;
using Core.DB;
using Core.DI;
using DAL;
using DAL.Abstractions.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
    };
});
builder.Services.AddAuthorization();

//Services
builder.Services.AddDbContext<AuthServerContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseNpgsql(builder.Configuration["ASPNETCORE_AuthConnection"]);
    }
    else
    {
        options.UseNpgsql(builder.Configuration["AUTH_SERVER_CONNECTION_STRING"]);
    }
});
builder.Services.AddScoped(typeof(ICrudService<>), typeof(CrudForEntity<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<JwtGeneralHelper>();
builder.Services.AddScoped<JwtRefresher>();
builder.Services.AddScoped<JWTTokensManipulator>();
builder.Services.AddScoped<IUserManipulator<User>, JwtUserManipulator>();

//Configurations
builder.Services.Configure<JwtConfigurations>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
//Mapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();

app.UseMigration();

app.UseHttpsRedirection();

app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
