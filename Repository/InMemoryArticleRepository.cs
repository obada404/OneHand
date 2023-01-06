using OneHandTraining.Interface;
using OneHandTraining.model;

namespace OneHandTraining.Repository;

public class InMemoryArticleRepository :IArticleRepository
{
    public async Task<Article> add(Article curent)
    {
        Repo.allArticle.addToArticle( curent);
        return curent;
    }

    public List<Article> findByFavorite(string favorited)
    {
       var result = Repo.allArticle.articles.Where(x=>x.author.Username == favorited);
       return result.ToList();
    }

    public List<Article> findByAuthor(string author)
    {
        var result = Repo.allArticle.articles.Where(x=>x.author.Username == author);
        return result.ToList();
        
    }

    public List<Article> findByTag(string tag)
    {
        var result = Repo.allArticle.articles.Where(x=>x.tagList.Contains(tag));
        return result.ToList();    
    }

    public List<Article> findArticlesFeed(string authorization)
    {
        throw new NotImplementedException();
    }
}