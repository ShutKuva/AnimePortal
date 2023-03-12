using AnimePortalAuthServer.Extensions;
using AnimePortalAuthServer.Middlewares;
using BLL;
using BLL.Abstractions;
using BLL.Jwt;
using Core.DB;
using Core.DI;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthServerConnectionString"));
});
builder.Services.AddScoped(typeof(ICrudService<>), typeof(CrudForEntity<>));
builder.Services.AddScoped<JwtGeneralHelper>();
builder.Services.AddScoped<JwtRefresher>();
builder.Services.AddScoped<JWTTokensManipulator>();
builder.Services.AddScoped<IUserManipulator<User>, JwtUserManipulator>();

//Configurations
builder.Services.Configure<JwtConfigurations>(builder.Configuration.GetSection("JWT"));

//Mapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMigration();

app.UseErrorHandling();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
