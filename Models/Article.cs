using System.ComponentModel.DataAnnotations.Schema;

namespace OneHandTraining.model;

public class Article
{
    public Article(string slug, string title, string description, string body, List<string> tagList,
        DateTime createdAt, DateTime updatedAt, bool favorited, int favoritesCount, UserOld author)
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
    public int Id { get; set; }
    public string? slug { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string body { get; set; }
    [NotMapped]
     public List<string> tagList { get; set; }
     [NotMapped]

    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public bool favorited { get; set; }
    public int favoritesCount { get; set; }
    public UserOld author { get; set; }
    public List<Comment> Comments { get; set; } = new();
    
}



