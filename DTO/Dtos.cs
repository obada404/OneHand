namespace OneHandTraining.DTO;
using OneHandTraining.model;

public class Dtos
{


}
public record tagseRequestEnv<T> ( T tags);


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

        public bool addToArticle(Article? article)
        {
            if (article != null) articles.Add(article);
            articlesCount++;
            return true;
        }
    }
public record  ArticleRequestEnv<T> ( T article);


public record ArticleRequest (string slug, string title,
    string description, string body, List<string> tagList,
    DateTime createdAt, DateTime updatedAt, bool favorited, 
    int favoritesCount, int authorId);

public record ProfileRequestEnv<T> ( T profile);


public record UserRegistrationRequest (string Username, 
    string Email, string Password);
public record loginUserRequest ( string Email, string Password);
public record emailUserRequest ( string Email);

public class UserRequest
{
    public int Id { get; set; }

    public  string Username { get; set; }
    public  string Email { get; set; }

    public string? Bio{ get; set; }
    public String? Token { set;get; }
    public string? Image{ get; set; }

};
public record UserRequestEnv<T> ( T User);