using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OneHandTraining;
using OneHandTraining.Interface;
using OneHandTraining.Models;
using OneHandTraining.Repository;
using OneHandTraining.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<oneHandContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); 
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

AddScopedFunction(builder);

builder.Services.AddFluentValidation(fv =>
{
    fv.AutomaticValidationEnabled = false;
    fv.ImplicitlyValidateChildProperties = false;

});

var app = builder.Build();
app.MapControllers();
app.Run("http://localhost:5500");





void AddScopedFunction(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddScoped<IUsersRepository, SqliteUserRepository>();
    webApplicationBuilder.Services.AddScoped<IUserService, UserService>();
    
    webApplicationBuilder.Services.AddScoped<IprofileRepository, SqliteProfileRepository>();
    webApplicationBuilder.Services.AddScoped<IprofileService, ProfileService>();
    
    webApplicationBuilder.Services.AddScoped<IArticleService, ArticleService>();
    webApplicationBuilder.Services.AddScoped<IArticleRepository, SqliteArticleRepository>();
}



