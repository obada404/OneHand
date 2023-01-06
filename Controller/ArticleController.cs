using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.Validation;
using OneHandTraining.DTO;
using OneHandTraining.Interface;

namespace OneHandTraining.controller;

public class ArticleController :Controller
{
    private readonly IArticleService _articleService;
    private readonly ArticleValidator _articleValidator;
    public ArticleController(IArticleService articleService ,ArticleValidator articleValidator)
    {
        _articleService = articleService;
        _articleValidator = articleValidator;
    }

    [HttpPost]
    [Route("/articles")]
    public async Task<ObjectResult> PostArticles([FromBody] ArticleRequestEnv<ArticleRequest> req){
        var result = _articleValidator.Validate(req.article);
        if ( !result.IsValid)
        {
            var errors = result.Errors.Select(x => new { errors = x.ErrorMessage });
            return new ObjectResult( errors ){StatusCode = (int)HttpStatusCode.BadRequest};
        }

        var root = await _articleService.addArticle(req.article);
        return new ObjectResult(root ){StatusCode = (int)HttpStatusCode.OK};
   
    }

    [HttpGet]
    [Route("/articles")]
    public ActionResult getaArticles(){
        if(! (string.IsNullOrEmpty(Request.Query["favorited"])))
        {
            var result = _articleService.findArticlesByFavorite((Request.Query["favorited"]));
            return new ObjectResult( result ){StatusCode = (int)HttpStatusCode.OK};
        }

        if(! (string.IsNullOrEmpty(Request.Query["author"])))
        {
            var result = _articleService.findArticlesByAuthor((Request.Query["author"]));
            return new ObjectResult( result ){StatusCode = (int)HttpStatusCode.OK};
        }
        
        if(! (string.IsNullOrEmpty(Request.Query["tag"])))
        {
            var result = _articleService.findArticlesByTag((Request.Query["tag"]));
            return new ObjectResult( result ){StatusCode = (int)HttpStatusCode.OK};
        }
        
        return new ObjectResult( Repo.allArticle ){StatusCode = (int)HttpStatusCode.OK};
    }

    [HttpGet]
    [Route("/articles/feed")]
    public ActionResult getArticlesFeed()
    {
        var result = _articleService.findArticlesFeed(Request.Headers.Authorization);
        return new ObjectResult(result){StatusCode = (int)HttpStatusCode.OK};
    }
}