using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.Validation;
using OneHandTraining.model;
using OneHandTraining.DTO;
using OneHandTraining.Models;

namespace OneHandTraining.controller;

public class articaleController :Controller
{
    private userController _userController;
    private readonly IarticaleService _articaleService;

    public articaleController(IarticaleService articaleService,userController userController1)
    {
        _articaleService = articaleService;
        _userController = userController1;
    }

    [HttpPost]
    [Route("/articles")]
    public ActionResult PostArticles([FromBody] ArticleRequestEnv<ArticleRequest> req){
        var validator = new ArticleValidator();
        var result = validator.Validate(req.article);
        if ( !result.IsValid)
        {
            var errors = result.Errors.Select(x => new { errors = x.ErrorMessage });
            return new ObjectResult( errors ){StatusCode = (int)HttpStatusCode.BadRequest};
         
         
        }

        var root = _articaleService.setArticle(req.article);
        return new ObjectResult(root ){StatusCode = (int)HttpStatusCode.OK};
   
    }

    [HttpGet]
    [Route("/articles")]
    public ActionResult getaArticles(){
        if(! (string.IsNullOrEmpty(_userController.Request.Query["favorited"])))
        {
            var que = _articaleService.getaArticlesByFavorited((_userController.Request.Query["favorited"]));
            return new ObjectResult( que ){StatusCode = (int)HttpStatusCode.OK};
        }
        
        
        if(! (string.IsNullOrEmpty(_userController.Request.Query["author"])))
        {
            var que = _articaleService.getaArticlesByAuthor((_userController.Request.Query["author"]));
            return new ObjectResult( que ){StatusCode = (int)HttpStatusCode.OK};
        }
        
        if(! (string.IsNullOrEmpty(_userController.Request.Query["tag"])))
        {
            var que = _articaleService.getaArticlesByTag((_userController.Request.Query["tag"]));
            return new ObjectResult( que ){StatusCode = (int)HttpStatusCode.OK};
        }
        
        return new ObjectResult( Repo.allArticle ){StatusCode = (int)HttpStatusCode.OK};
    }

    [HttpGet]
    [Route("/articles/feed")]
    public ActionResult getArticlesFeed()
    {



        var que = _articaleService.getArticlesFeed(_userController.Request.Headers.Authorization);
        return new ObjectResult(que){StatusCode = (int)HttpStatusCode.OK};
    }
}