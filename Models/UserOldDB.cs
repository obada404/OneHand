namespace OneHandTraining.model;

public class UserOldDB
{
    public int Id { get; set; }

    public  string Username { get;  }
    public  string Email { get; set; }

    public  string Password { get; }
    public string? Bio{ get; }
    public string? Image{ get; }

    public UserOldDB( string? username, string? email, string password, string bio, string image )
    {
        Bio = bio;
        Image = image;
        Username = username;
        Email = email;
        Password = password;
    }
}

