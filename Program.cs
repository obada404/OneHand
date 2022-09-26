using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using System.Reflection;
using System.Web.Http.ModelBinding;
using System.Web.WebPages;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using OneHandTraining;
using OneHandTraining.Interface;
using OneHandTraining.Models;
using OneHandTraining.Validation;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<oneHandContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); 
//builder.Services.AddScoped<IValidator<UserRequest>, userValidator>();
builder.Services.AddScoped<IUsersRepository, SqliteUserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddFluentValidation(fv =>
{
    fv.AutomaticValidationEnabled =false;
    fv.ImplicitlyValidateChildProperties = false;

});

var app = builder.Build();
app.MapControllers();
app.Run("http://localhost:5500");



public record tagseRequestEnv<T> ( T tags);




