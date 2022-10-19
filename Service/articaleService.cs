using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.DTO;
using OneHandTraining.Interface;
using OneHandTraining.model;

namespace OneHandTraining;

public interface IarticaleService
{
    ArticleRequestEnv<Article>  setArticle(ArticleRequest article);
    IEnumerable<Article> getArticlesByFavorited( String favorited);
    IEnumerable<Article> getaArticlesByAuthor( String author);
    IEnumerable<Article> getaArticlesByTag( String Tag);
    IEnumerable<Article> getArticlesFeed(String Authorization);
}

public class articaleService : IarticaleService
{

    private readonly IarticaleRepository _articaleRepository;

    public articaleService(IarticaleRepository articaleRepository)
    {
        _articaleRepository = articaleRepository;
    }
    
     public ArticleRequestEnv<Article>  setArticle(ArticleRequest article)
     {
       
         Article curent = new Article(article.slug, article.title, article.description, article.body,
             article.tagList, article.createdAt, article.updatedAt,article.favorited,
             article.favoritesCount,article.author);

       var art =   _articaleRepository.add(curent);
       ArticleRequestEnv<Article> root = new ArticleRequestEnv<Article>(curent);

         return root;
     }
      public IEnumerable<Article> getArticlesByFavorited( String favorited)
      {
          _articaleRepository.getByFavorited(favorited);
         IEnumerable<Article> que  =
             from Article in Repo.allArticle.articles.ToArray()
             where Article.author.Username.Equals(favorited)
             select Article;
         return que;

     }
      public IEnumerable<Article> getaArticlesByAuthor( String author)
     {
         _articaleRepository.getByAuthor(author);

         IEnumerable<Article> que  =
             from Article in Repo.allArticle.articles.ToArray()
             where Article.author.Username.Equals(author)
             select Article;
         return que;

     }
      public IEnumerable<Article> getaArticlesByTag( String Tag)
     {
         _articaleRepository.getByTag(Tag);

         IEnumerable<Article> que  =
             from Article in Repo.allArticle.articles.ToArray()
             where Article.author.Username.Equals(Tag)
             select Article;
         return que;

     }
      public IEnumerable<Article> getArticlesFeed(String Authorization)
     {
         _articaleRepository.getArticlesFeed(Authorization);

         IEnumerable<UserOld> userQuery =
             from user1 in Repo.UserDb
             where user1.Token.Equals(Authorization) 
             select user1;
        
         IEnumerable<Article> que  =
             (from Article in Repo.allArticle.articles.ToArray()
                 where Article.author.following.Equals(true)
                 select Article).Take(5);
         return que;

     }
}
