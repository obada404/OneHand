using OneHandTraining.DTO;
using OneHandTraining.model;

namespace OneHandTraining.Interface;

public interface IArticleService
{
 
        Task<ArticleRequestEnv<Article>> addArticle(ArticleRequest article);
        List<Article> findArticlesByFavorite( String favorited);
        List<Article> findArticlesByAuthor( String author);
        List<Article> findArticlesByTag( String Tag);
        List<Article> findArticlesFeed(String Authorization);
    

}