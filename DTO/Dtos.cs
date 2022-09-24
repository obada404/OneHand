namespace OneHandTraining.DTO;
using OneHandTraining.model;

public class Dtos
{


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
    public record  ArticleRequestEnv<T> ( T article);


    public record ArticleRequest (string slug, string title, string description, string body, List<string> tagList, DateTime createdAt, DateTime updatedAt, bool favorited, int favoritesCount, Author author);

public record ProfileRequestEnv<T> ( T profile);


public class Users
{
    public  UserOld user {get; }

    public Users(UserOld user)
    {
        this.user = user;
    }

    
}
// record Users(string? UserName, string? Email, string? Password, string? Token,
//     string? Bio, string? Image);

public record UserRequest (string Username, string Email, string Password);
public record loginUserRequest ( string Email, string Password);
public record emailUserRequest ( string Email);

public record UserRequestEnv<T> ( T User);