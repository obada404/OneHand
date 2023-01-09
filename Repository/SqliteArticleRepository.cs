using OneHandTraining.Interface;
using OneHandTraining.model;
using OneHandTraining.Models;

namespace OneHandTraining.Repository;

public class SqliteArticleRepository :IArticleRepository
{
    readonly private oneHandContext _context;

    public SqliteArticleRepository(oneHandContext context)
    {
        _context = context;
    }
    public  async Task<Article> add(Article curent, int articleAuthorId)
    {
        curent.author=(await _context.UserOldDBs.FindAsync(articleAuthorId))!;
         _context.Articles.Add(curent);
        await _context.SaveChangesAsync();
        return  curent;
    }

    public List<Article> findByFavorite(string favorited)
    {
        IEnumerable<Article> result  =
            from article in _context.Articles
            where article.author.Username.Equals(favorited)
            select article;

       
        return result.ToList();
    }

    public List<Article> findByAuthor(string author)
    {
        IEnumerable<Article> result  =
            from article in _context.Articles
            where article.author.Username.Equals(author)
            select article;

        return result.ToList();
    }

    public List<Article> findByTag(string tag)
    {
        IEnumerable<Article> result  =
            from article in _context.Articles
            where article.tagList.Contains(tag)
            select article;

        return result.ToList();
    }

    public IEnumerable<Article> findArticlesFeed(string authorization)
    {
     var user =   _context.UserOldDBs.Where(x=>x.Token == authorization).First();
     if (user == null)
         return null;

    var result= _context.Articles.Where(x => x.author.followers.Contains(user));

     return  result.AsEnumerable();

    }

    public List<Article> getAllArticles()
    {
        return _context.Articles.ToList();
    }
}