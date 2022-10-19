using OneHandTraining.DTO;
using OneHandTraining.model;

namespace OneHandTraining.Interface;

public interface IarticaleRepository
{
    Article add(Article curent);
    IEnumerable<Article> getByFavorited(string favorited);
    IEnumerable<Article> getByAuthor(string author);
    IEnumerable<Article> getByTag(string tag);
    IEnumerable<Article> getArticlesFeed(string authorization);
}