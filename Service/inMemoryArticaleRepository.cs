using OneHandTraining.DTO;
using OneHandTraining.Interface;
using OneHandTraining.model;

namespace OneHandTraining;

public class inMemoryArticaleRepository :IarticaleRepository
{
    public Article add(Article curent)
    {
        Repo.allArticle.addToArticle( curent);
        return curent;
    }

    public IEnumerable<Article> getByFavorited(string favorited)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Article> getByAuthor(string author)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Article> getByTag(string tag)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Article> getArticlesFeed(string authorization)
    {
        throw new NotImplementedException();
    }
}