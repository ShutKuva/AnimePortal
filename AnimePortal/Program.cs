using AnimePortalAuthServer.Extension;
using AnimePortalAuthServer.Extensions;
using AnimePortalAuthServer.Transformers;
using BLL;
using BLL.Abstractions.Interfaces;
using BLL.Jwt;
using Core.DB;
using Core.DI;
using DAL;
using DAL.Abstractions.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Abstraction;
using Services.Abstraction.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(
        new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});
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
builder.Services.AddScoped(typeof(ICrudService<>), typeof(CrudForEntity<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<JwtGeneralHelper>();
builder.Services.AddScoped<JwtRefresher>();
builder.Services.AddScoped<JWTTokensManipulator>();
builder.Services.AddScoped<IUserManipulator<User>, JwtUserManipulator>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IAnimeService, AnimeService>();

//Configurations
builder.Services.Configure<JwtConfigurations>(jwtConfigurations =>
{
    if (builder.Environment.IsDevelopment())
    {
        jwtConfigurations.Audience = builder.Configuration["JWT:Audience"];
        jwtConfigurations.Issuer = builder.Configuration["JWT:Issuer"];
        jwtConfigurations.Lifetime = int.Parse(builder.Configuration["JWT:Lifetime"]);
        jwtConfigurations.RefreshLifetime = int.Parse(builder.Configuration["JWT:RefreshLifetime"]);
        jwtConfigurations.SecretCode = builder.Configuration["JWT:SecretCode"];
    }
    else
    {
        jwtConfigurations.Audience = builder.Configuration["JWT_AUDIENCE"];
        jwtConfigurations.Issuer = builder.Configuration["JWT_ISSUER"];
        jwtConfigurations.Lifetime = int.Parse(builder.Configuration["JWT_LIFETIME"]);
        jwtConfigurations.RefreshLifetime = int.Parse(builder.Configuration["JWT_REFRESH_LIFETIME"]);
        jwtConfigurations.SecretCode = builder.Configuration["JWT_SECRET_CODE"];
    }
});
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));


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

app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
