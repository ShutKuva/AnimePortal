using System.Text.Json.Serialization;
using Adapters;
using Adapters.Abstractions;
using AnimePortalAuthServer.Extension;
using AnimePortalAuthServer.Extensions;
using AnimePortalAuthServer.Transformers;
using BLL;
using BLL.Abstractions.Interfaces;
using BLL.Abstractions.Interfaces.Jwt;
using BLL.Jwt;
using Core.DB;
using Core.DI;
using Core.DTOs.Jwt;
using DAL;
using DAL.Abstractions.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Abstraction;
using Services.Abstraction.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o =>
{
    o.AddPolicy("DevCorsPolicy", config =>
    {
        config.AllowAnyHeader().AllowAnyMethod().AllowCredentials();

        if (builder.Environment.IsDevelopment())
        {
            config.WithOrigins(builder.Configuration["Origins:Test"]);
        }
        else
        {
            config.WithOrigins(builder.Configuration["ORIGIN_TEST"]);
        }
    });
});

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(
        new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
}).AddJsonOptions(x=> x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
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
        options.UseNpgsql(builder.Configuration.GetConnectionString("AuthConnectionString"));
    }
    else
    {
        options.UseNpgsql(builder.Configuration["AUTH_SERVER_CONNECTION_STRING"]);
    }
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();

builder.Services.AddScoped<IUserService<JwtUserDto, RegisterUser, LoginUser, RefreshUser>, JwtUserService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAnimeDetailedAdapter, AnimeDetailedAdapter>();
builder.Services.AddScoped<IAnimePreviewAdapter, AnimePreviewAdapter>();

//Configurations
builder.Services.Configure<JwtConfigurations>(jwtConfigurations =>
{
    if (builder.Environment.IsDevelopment())
    {
        jwtConfigurations.Audience = builder.Configuration["JWT:Audience"];
        jwtConfigurations.Issuer = builder.Configuration["JWT:Issuer"];
        jwtConfigurations.AccessLifetime = int.Parse(builder.Configuration["JWT:AccessLifetime"]);
        jwtConfigurations.RefreshLifetime = int.Parse(builder.Configuration["JWT:RefreshLifetime"]);
        jwtConfigurations.AccessSecretCode = builder.Configuration["JWT:AccessSecretCode"];
        jwtConfigurations.RefreshSecretCode = builder.Configuration["JWT:RefreshSecretCode"];
    }
    else
    {
        jwtConfigurations.Audience = builder.Configuration["JWT_AUDIENCE"];
        jwtConfigurations.Issuer = builder.Configuration["JWT_ISSUER"];
        jwtConfigurations.AccessLifetime = int.Parse(builder.Configuration["JWT_ACCESS_LIFETIME"]);
        jwtConfigurations.RefreshLifetime = int.Parse(builder.Configuration["JWT_REFRESH_LIFETIME"]);
        jwtConfigurations.AccessSecretCode = builder.Configuration["JWT_ACCESS_SECRET_CODE"];
        jwtConfigurations.RefreshSecretCode = builder.Configuration["JWT_REFRESH_SECRET_CODE"];
    }
});
builder.Services.Configure<CloudinarySettings>(cloudinaryConfiguration =>
{
    if (builder.Environment.IsDevelopment())
    {
        cloudinaryConfiguration.ApiKey = builder.Configuration["CloudinarySettings:ApiKey"];
        cloudinaryConfiguration.ApiSecret = builder.Configuration["CloudinarySettings:ApiSecret"];
        cloudinaryConfiguration.CloudName = builder.Configuration["CloudinarySettings:CloudName"];
    }
    else
    {
        cloudinaryConfiguration.ApiKey = builder.Configuration["CLOUDINARY_API_KEY"];
        cloudinaryConfiguration.ApiSecret = builder.Configuration["CLOUDINARY_API_SECRET"];
        cloudinaryConfiguration.CloudName = builder.Configuration["CLOUDINARY_CLOUD_NAME"];
    }
});


//Mapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseExceptionHandler();

app.UseMigration();

app.UseHttpsRedirection();

app.UseCors("DevCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
