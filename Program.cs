using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OneHandTraining;
using OneHandTraining.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<oneHandContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); 
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddRepositoryConfigration();
builder.Services.AddSarviceConfigration();
builder.Services.AddValidationConfigration();

builder.Services.AddFluentValidation(fv =>
{ 
    fv.AutomaticValidationEnabled = false;
    fv.ImplicitlyValidateChildProperties = false;
});

var app = builder.Build();
app.MapControllers();
app.Run("http://localhost:5500");








