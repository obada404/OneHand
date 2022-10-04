namespace OneHandTraining.model;

public class UserOld
{
    public UserOld( string username, string email, string password, string? bio, string? token, string? image)
    {
      
        Username = username;
        Email = email;
        Password = password;
        Bio = bio;
        Token = token;
        Image = image;
    }

    public int Id { get; set; }

    public  string Username { get;  }
    public  string Email { get; set; }

    public  string Password { get; }
    public string? Bio{ get; }
    public String? Token { set;get; }
    public string? Image{ get; }
    public List<UserOld>? followers { get; set; } = new();
    public List<UserOld>? following { get; set; }= new();
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

 
}

