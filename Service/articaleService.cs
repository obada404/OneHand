using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.DTO;
using OneHandTraining.model;

namespace OneHandTraining;

public interface IarticaleService
{
    ArticleRequestEnv<Article>  setArticle(ArticleRequest article);
    IEnumerable<Article> getaArticlesByFavorited( String favorited);
    IEnumerable<Article> getaArticlesByAuthor( String author);
    IEnumerable<Article> getaArticlesByTag( String Tag);
    IEnumerable<Article> getArticlesFeed(String Authorization);
}

public class articaleService : IarticaleService
{
     public ArticleRequestEnv<Article>  setArticle(ArticleRequest article)
     {
       
         Article curent = new Article(article.slug, article.title, article.description, article.body,
             article.tagList, article.createdAt, article.updatedAt,article.favorited,
             article.favoritesCount,article.author);
     
         Repo.allArticle.addToArticle( curent);
         ArticleRequestEnv<Article> root = new ArticleRequestEnv<Article>(curent);
         return root;
     }
      public IEnumerable<Article> getaArticlesByFavorited( String favorited)
     {
         IEnumerable<Article> que  =
             from Article in Repo.allArticle.articles.ToArray()
             where Article.author.Username.Equals(favorited)
             select Article;
         return que;

     }
      public IEnumerable<Article> getaArticlesByAuthor( String author)
     {
         IEnumerable<Article> que  =
             from Article in Repo.allArticle.articles.ToArray()
             where Article.author.Username.Equals(author)
             select Article;
         return que;

     }
      public IEnumerable<Article> getaArticlesByTag( String Tag)
     {
         IEnumerable<Article> que  =
             from Article in Repo.allArticle.articles.ToArray()
             where Article.author.Username.Equals(Tag)
             select Article;
         return que;

     }
      public IEnumerable<Article> getArticlesFeed(String Authorization)
     {
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
