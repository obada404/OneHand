using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
var UserDb = new List<Users>();
var ArticleDb = new List<RootArticle>();
var profileDb = new List<ProfileRequestEnv<Profile>>();
string[] tags  = {"reactjs", "angularjs"};
allArticle allArticle = new allArticle();
app.MapGet("/", () => "Hello World!");

app.MapPost("/users", async (HttpContext ctx, UserRequestEnv<UserRequest> req)=>
{

    UserOld user = new UserOld(req.User.Username, req.User.Email, req.User.Password,"","",$"{Guid.NewGuid()}");
    Users all = new Users(user);
    UserDb.Add(all);
    return Results.Ok(all);
});
app.MapPost("/users/login", async (HttpContext ctx, UserRequestEnv<UserRequest> req)=>
{


        IEnumerable<Users> userQuery =
        from user1 in UserDb
        where user1.user.Email.Equals(req.User.Email) && user1.user.Password.Equals(req.User.Password)
        select user1;

    return Results.Ok(userQuery.Last());
});
app.MapGet("/user",  (HttpRequest req) =>
{
    
        IEnumerable<Users> userQuery =
        from user1 in UserDb
        where user1.user.Token.Equals( req.Headers["Authorization"])
        select user1;
    
   return Results.Ok(userQuery.Last());
});
app.MapPut("/user",  async (HttpRequest ctx, UserRequestEnv<UserRequest> req) =>
{
    
 
    foreach( Users cust in UserDb)
    {
        if (cust.user.Token == ctx.Headers["Authorization"] ) 
        {
            cust.user.Email= req.User.Email;
            return Results.Ok(cust);
        }
    }
    
    return Results.Problem();
});

app.MapPost("/profiles",  async (HttpRequest ctx, [FromBody] ProfileRequestEnv<Profile> req) =>
{
    
    Profile profile = new Profile(req.profile.username, req.profile.bio, req.profile.image,req.profile.following);
    ProfileRequestEnv<Profile> root = new ProfileRequestEnv<Profile>(profile);
    profileDb.Add(root);
    return Results.Ok(root);

});

app.MapGet("/profiles/{username}",  async (HttpRequest ctx,String username) =>
{

    IEnumerable<ProfileRequestEnv<Profile>> profileQuery =
        from Profile in profileDb
        where Profile.profile.username.Equals(username)
        select Profile;
    
    return Results.Ok(profileQuery.Last());
});
app.MapPost("/profiles/{username}/follow",  async (HttpRequest ctx,String username, [FromBody] ProfileRequestEnv<Profile> req) =>
{
 
    foreach( ProfileRequestEnv<Profile> cust in profileDb)
    {
        if (cust.profile.username.Equals(username) == true )
        {
            cust.profile.following = true;
            return Results.Ok(cust);
        }
    }
    return Results.Problem();
    
}); 

app.MapDelete("/profiles/{username}/follow",  async (HttpRequest ctx,String username) =>
{
 
    foreach( ProfileRequestEnv<Profile> cust in profileDb)
    {
        if (cust.profile.username.Equals(username) == true )
        {
            cust.profile.following = false;
            return Results.Ok(cust);
        }
    }
    return Results.Problem();
});

app.MapGet("/tags",  async (HttpRequest ctx) =>
{
    return new tagseRequestEnv<Array>(tags);


});

app.MapPost("/articles",  async (HttpRequest ctx, [FromBody] ArticleRequestEnv<ArticleRequest> req) =>
{
    Article curent = new Article(req.article.slug, req.article.title, req.article.description, req.article.body,
        req.article.tagList, req.article.createdAt, req.article.updatedAt, req.article.favorited,
        req.article.favoritesCount, req.article.author);
    
    allArticle.addToArticle( curent);
    ArticleRequestEnv<Article> root = new ArticleRequestEnv<Article>(curent);
    return Results.Ok(root);
  
    
});
app.MapGet("/articles",  async () =>
{
    return allArticle;
});
app.MapGet("/articles{author=alpha}",  async (String author) =>
{
        Console.WriteLine(author);
    IEnumerable<Article> que  =
        from Article in allArticle.articles.ToArray()
        where Article.author.username.Equals(author)
        select Article;
    return que;
    
});




app.Run("http://localhost:5500");
public record UserRequest (string Username, string Email, string Password);
public record ArticleRequest (string slug, string title, string description, string body, List<string> tagList, DateTime createdAt, DateTime updatedAt, bool favorited, int favoritesCount, Author author);
public record UserRequestEnv<T> ( T User);
public record  ArticleRequestEnv<T> ( T article);

public record ProfileRequestEnv<T> ( T profile);
public record tagseRequestEnv<T> ( T tags);
// record Users(string? UserName, string? Email, string? Password, string? Token,
//     string? Bio, string? Image);
public class Profile
{
    public Profile(string username, string bio, string image, bool following)
    {
        this.username = username;
        this.bio = bio;
        this.image = image;
        this.following = following;
    }

    public string username { get; set; }
    public string bio { get; set; }
    public string image { get; set; }
    public bool following { get; set; }
}

public class UserOld
{
    public  string Username { get;  }
    public  string Email { get; set; }

    public  string Password { get; }
    //public string? Token;
    public string? Bio{ get; }
    public string? Image{ get; }
    public String? Token { get; }
    
    public UserOld( string? username, string? email, string password, string bio, string image ,String token)
    {
        Bio = bio;
        Image = image;
        Username = username;
        Email = email;
        Password = password;
        Token = token;
    }
}
public class Users
{
    public  UserOld user {get; }

    public Users(UserOld user)
    {
        this.user = user;
    }

    
}
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Article
{
    public Article(string slug, string title, string description, string body, List<string> tagList,
        DateTime createdAt, DateTime updatedAt, bool favorited, int favoritesCount, Author author)
    {
        this.slug = slug;
        this.title = title;
        this.description = description;
        this.body = body;
        this.tagList = tagList;
        this.createdAt = System.DateTime.Today;
        this.updatedAt = System.DateTime.MinValue;
        this.favorited = favorited;
        this.favoritesCount = favoritesCount;
        this.author = author;
    }
    public Article( Article a)
    {
        this.slug =  a.slug;
        this.title = a.title;
        this.description = a.description;
        this.body = a.body;
        this.tagList = a.tagList;
        this.createdAt = a.createdAt;
        this.updatedAt = a.updatedAt;
        this.favorited = a.favorited;
        this.favoritesCount = a.favoritesCount;
        this.author = a.author;
    }
    public Article( )
    {
        this.slug =  "";
        this.title = "";
        this.description = "";
        this.body = "";
        this.tagList = null;
        this.createdAt = System.DateTime.Now;
        this.updatedAt = System.DateTime.Now;
        this.favorited = false;
        this.favoritesCount = 0;
        this.author = null;
    }
    public string slug { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string body { get; set; }
    public List<string> tagList { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public bool favorited { get; set; }
    public int favoritesCount { get; set; }
    public Author author { get; set; }
}

public class Author
{
    public string username { get; set; }
    public string bio { get; set; }
    public string image { get; set; }
    public bool following { get; set; }
}

public class RootArticle
{
    public RootArticle(Article article)
    {
        this.article = article;
    }

    public Article article { get; set; }
}
public class allArticle
{
    public allArticle( )
    {
        this.articles = new List<Article>();
        this.articlesCount = 0;
    }

    public List<Article> articles { get; set; }

    public int articlesCount { get; set; } = 0;

    public bool addToArticle(Article? a)
    {
        articles.Add(a);
        articlesCount++;
        return true;
    }
}