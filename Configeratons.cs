using AdventureWorks;
using OneHandTraining.Interface;
using OneHandTraining.Repository;
using OneHandTraining.Service;
using OneHandTraining.Validation;

namespace OneHandTraining;

public static class Configeratons
{
    public static void AddRepositoryConfigration(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, SqliteUserRepository>();
        services.AddScoped<IprofileRepository, SqliteProfileRepository>();
        services.AddScoped<IArticleRepository, SqliteArticleRepository>();


        
    
    }
    public static void AddSarviceConfigration(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IprofileService, ProfileService>();
        services.AddScoped<IArticleService, ArticleService>();

       
    
    }

    public static void AddValidationConfigration(this IServiceCollection services)
    {
        services.AddScoped<userValidator>();
        services.AddScoped<ArticleValidator>();

         
        // services.AddScoped(s=>new JwtManager(configuration["Jwt:Key"]));
        services.AddScoped<JwtManager>();
    }
    
}