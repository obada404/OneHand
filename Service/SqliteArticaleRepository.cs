using OneHandTraining.DTO;
using OneHandTraining.Interface;
using OneHandTraining.model;
using OneHandTraining.Models;

namespace OneHandTraining;

public class SqliteArticaleRepository :IarticaleRepository
{
    readonly private oneHandContext _context;

    public SqliteArticaleRepository(oneHandContext context)
    {
        _context = context;
    }
    public Article add(Article curent)
    {
        _context.Articles.Add(curent);
        _context.SaveChanges();
        return curent;
    }

    public IEnumerable<Article> getByFavorited(string favorited)
    {
        IEnumerable<Article> que  =
            from Article in _context.Articles
            where Article.author.Username.Equals(favorited)
            select Article;

        return que;
    }

    public IEnumerable<Article> getByAuthor(string author)
    {
        IEnumerable<Article> que  =
            from Article in _context.Articles
            where Article.author.Username.Equals(author)
            select Article;

        return que;
    }

    public IEnumerable<Article> getByTag(string tag)
    {
        IEnumerable<Article> que  =
            from Article in _context.Articles
            where Article.tagList.Contains(tag)
            select Article;

        return que;
    }

    public IEnumerable<Article> getArticlesFeed(string authorization)
    {
     var user =   _context.UserOldDBs.Single(x=>x.Token == authorization);
     IEnumerable<Article> que  =
         (from Article in _context.Articles
             where Article.author.following.Equals(true)
             select Article).Take(5);
     return que;
     
    }
}