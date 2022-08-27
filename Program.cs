using Microsoft.EntityFrameworkCore;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
var UserDb = new List<Users>();
app.MapGet("/", () => "Hello World!");

app.MapPost("/users", async (HttpContext ctx, UserRequestEnv<UserRequest> req)=>
{
    Console.WriteLine(req.User.Email);
    Console.WriteLine(req.User.Password);
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
app.Run("http://localhost:5500");
public record UserRequestEnv<T> ( T User);
public record UserRequest (string Username, string Email, string Password);

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