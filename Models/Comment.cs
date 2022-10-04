namespace OneHandTraining.model;

public class Comment
{
    public int Id { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string body { get; set; }
    public UserOld author { get; set; }
    
}
