using AutoMapper;
using OneHandTraining.DTO;
using OneHandTraining.Interface;
using OneHandTraining.model;

namespace OneHandTraining.Service;


public class ArticleService : IArticleService
{

    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapperArticle;
    public ArticleService(IArticleRepository articleRepository,IMapper mapperArticle)
    {
        _articleRepository = articleRepository;
        _mapperArticle = mapperArticle;
    }
    
     public async Task<ArticleRequestEnv<ArticleRequest>> addArticle(ArticleRequest article)
     {
       
         var mapArticle = _mapperArticle.Map<ArticleRequest,Article>(article);
         var addedArticle=   await _articleRepository.add(mapArticle,article.authorId);
         
         return new ArticleRequestEnv<ArticleRequest>(_mapperArticle.Map<Article,ArticleRequest>(addedArticle));
     }
      public List<Article> findArticlesByFavorite( String favorited)
      {
          return _articleRepository.findByFavorite(favorited);

      }
      public List<Article> findArticlesByAuthor( String author)
      {
         return _articleRepository.findByAuthor(author);

      }
      public List<Article> findArticlesByTag( String tag)
     {
       return  _articleRepository.findByTag(tag);


     }
      public List<ArticleRequest> findArticlesFeed(string authorization)
     {
        var  result =_articleRepository.findArticlesFeed(authorization);
       var maping= _mapperArticle.Map<IEnumerable<ArticleRequest>>(result);
       return maping.ToList();
     }

      public List<Article> getAllArticles()
      {
          return _articleRepository.getAllArticles();
      }
}
