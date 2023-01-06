using OneHandTraining.model;

namespace OneHandTraining.Interface;

public interface IArticleRepository
{
    Task<Article> add(Article curent);
    List<Article> findByFavorite(string favorited);
    List<Article> findByAuthor(string author);
    List<Article> findByTag(string tag);
    List<Article> findArticlesFeed(string authorization);
}