namespace OneHandTraining.model;
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
