//


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
using OneHandTraining.Models;
using OneHandTraining.Validation;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

var builder = WebApplication.CreateBuilder(args);
//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<oneHandContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); 
//builder.Services.AddScoped<IValidator<UserRequest>, userValidator>();
builder.Services.AddFluentValidation(fv =>
{
    fv.AutomaticValidationEnabled =false;
    fv.ImplicitlyValidateChildProperties = false;

});

var app = builder.Build();

//
// app.UseSwagger();
// app.UseSwaggerUI();
//
// // app.UseAuthentication();
// // app.UseAuthorization();
//
// /*
//  * pre request (middleware)
//  * controller (10-20 middleware) /minumal api (1-2)
//  * post reqquest (middleware)
//  */
//
// app.MapPost("/users", async   (HttpContext ctx, UserRequestEnv<UserRequest>   req   )=>
// {
//
//     
//     var validator = new userValidator();
//     var result = validator.Validate(req.User);
//     if ( !result.IsValid)
//     {
//         var errors = result.Errors.Select(x => new { errors = x.ErrorMessage });
//         return Results.BadRequest(errors);
//     }
//
//     UserOld user = new UserOld(req.User.Username, req.User.Email, req.User.Password,"","",$"{Guid.NewGuid()}");
//     Users all = new Users(user);
//     UserDb.Add(all);
//     return Results.Ok(all);
// });
// app.MapPost("/users/login", async (HttpContext ctx, UserRequestEnv<UserRequest> req)=>
// {
//
//
//         IEnumerable<Users> userQuery =
//         from user1 in UserDb
//         where user1.user.Email.Equals(req.User.Email) && user1.user.Password.Equals(req.User.Password)
//         select user1;
//
//     return Results.Ok(userQuery.Last());
// });
// app.MapGet("/user",  (HttpRequest req) =>
// {
//     
//         IEnumerable<Users> userQuery =
//         from user1 in UserDb
//         where user1.user.Token.Equals( req.Headers["Authorization"])
//         select user1;
//     
//    return Results.Ok(userQuery.Last());
// });
// app.MapPut("/user",  async (HttpRequest ctx, UserRequestEnv<UserRequest> req) =>
// {
//     
//  
//     foreach( Users cust in UserDb)
//     {
//         if (cust.user.Token == ctx.Headers["Authorization"] ) 
//         {
//             cust.user.Email= req.User.Email;
//             return Results.Ok(cust);
//         }
//     }
//     
//     return Results.Problem();
// });
//
// app.MapPost("/profiles",  async (HttpRequest ctx, [FromBody] ProfileRequestEnv<Profile> req) =>
// {
//     
//     Profile profile = new Profile(req.profile.username, req.profile.bio, req.profile.image,req.profile.following);
//     ProfileRequestEnv<Profile> root = new ProfileRequestEnv<Profile>(profile);
//     profileDb.Add(root);
//     return Results.Ok(root);
//
// });
//
// app.MapGet("/profiles/{username}",  async (HttpRequest ctx,String username) =>
// {
//
//     IEnumerable<ProfileRequestEnv<Profile>> profileQuery =
//         from Profile in profileDb
//         where Profile.profile.username.Equals(username)
//         select Profile;
//     
//     return Results.Ok(profileQuery.Last());
// });
// app.MapPost("/profiles/{username}/follow",  async (HttpRequest ctx,String username, [FromBody] ProfileRequestEnv<Profile> req) =>
// {
//  
//     foreach( ProfileRequestEnv<Profile> cust in profileDb)
//     {
//         if (cust.profile.username.Equals(username) == true )
//         {
//             cust.profile.following = true;
//             return Results.Ok(cust);
//         }
//     }
//     return Results.Problem();
//     
// }); 
//
// app.MapDelete("/profiles/{username}/follow",  async (HttpRequest ctx,String username) =>
// {
//  
//     foreach( ProfileRequestEnv<Profile> cust in profileDb)
//     {
//         if (cust.profile.username.Equals(username) == true )
//         {
//             cust.profile.following = false;
//             return Results.Ok(cust);
//         }
//     }
//     return Results.Problem();
// });
//
// app.MapGet("/tags",  async (HttpRequest ctx) =>
// {
//     return new tagseRequestEnv<Array>(tags);
//
//
// });
//
// app.MapPost("/articles",  async (HttpRequest ctx, [FromBody] ArticleRequestEnv<ArticleRequest> req) =>
// {
//     
//     var validator = new ArticleValidator();
//     var result = validator.Validate(req.article);
//     if ( !result.IsValid)
//     {
//         var errors = result.Errors.Select(x => new { errors = x.ErrorMessage });
//         return Results.BadRequest(errors);
//     }
//     Article curent = new Article(req.article.slug, req.article.title, req.article.description, req.article.body,
//         req.article.tagList, req.article.createdAt, req.article.updatedAt, req.article.favorited,
//         req.article.favoritesCount, req.article.author);
//     
//     allArticle.addToArticle( curent);
//     ArticleRequestEnv<Article> root = new ArticleRequestEnv<Article>(curent);
//     return Results.Ok(root);
//   
//     
// });
// app.MapGet("/articles",  async () =>
// {
//     return allArticle;
// });
// app.MapGet("/articles/{author}",  async (String author) =>
// {
//         Console.WriteLine(author);
//     IEnumerable<Article> que  =
//         from Article in allArticle.articles.ToArray()
//         where Article.author.username.Equals(author)
//         select Article;
//         
//     return que;
//     
// });
//
//
//
//
app.MapControllers();
app.Run("http://localhost:5500");



public record tagseRequestEnv<T> ( T tags);

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);



