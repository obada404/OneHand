namespace OneHandTraining.model;

public class UserOld
{

    public int Id { get; set; }

    public  string Username { get; set; }
    public  string Email { get; set; }

    public  string Password { get; set; }
    public string? Bio{ get; set; }
    public String? Token { set;get; }
    public string? Image{ get; }
    public List<UserOld>? followers { get; set; } = new();
    public List<UserOld>? following { get; set; }= new();
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

 
}

