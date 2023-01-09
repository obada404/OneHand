using OneHandTraining.model;

namespace OneHandTraining.Interface;

public interface IArticleRepository
{
    Task<Article> add(Article curent, int articleAuthorId);
    List<Article> findByFavorite(string favorited);
    List<Article> findByAuthor(string author);
    List<Article> findByTag(string tag);
    IEnumerable<Article> findArticlesFeed(string authorization);
    List<Article> getAllArticles();
}