using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.Validation;

namespace OneHandTraining.controller;
[ApiController]
public class userController :Controller
{

    [HttpPost]
    [Route("/users")] 
    public ActionResult PostUser([FromBody] UserRequestEnv<UserRequest> req)
    {
     

        var validator = new userValidator();
     var result = validator.Validate(req.User);
     if ( !result.IsValid)
     {
         var errors = result.Errors.Select(x => new { errors = x.ErrorMessage });
         return new JsonResult(errors);
     }
        var resp = new UserOld(req.User.Username, req.User.Email, req.User.Password,"","asdsdasd",$"{Guid.NewGuid()}");
        Repo.UserDb.Add(resp);
        //Json version 
        return new JsonResult(new UserRequestEnv<UserOld>(resp)){StatusCode = (int) HttpStatusCode.OK};
    
        //generic version
        return new ObjectResult(new UserRequestEnv<UserOld>(resp)){StatusCode = (int)HttpStatusCode.Accepted};
    }
    
    [HttpPost]
    [Route("/login")] 
    public ActionResult PostLogin([FromBody] UserRequestEnv<UserRequest> req)
    {
        
        IEnumerable<UserOld> userQuery =
         from user1 in Repo.UserDb
         where user1.Email.Equals(req.User.Email) && user1.Password.Equals(req.User.Password)
         select user1;
        
        
        return new ObjectResult(userQuery.Last()){StatusCode = (int)HttpStatusCode.OK};
    }
    [HttpPost]
    [Route("/users/login")] 
    public ActionResult PostUsersLogin([FromBody] UserRequestEnv<UserRequest> req)
    {
        
        IEnumerable<UserOld> userQuery =
            from user1 in Repo.UserDb
            where user1.Email.Equals(req.User.Email) && user1.Password.Equals(req.User.Password)
            select user1;
        
        
        return new ObjectResult(userQuery.Last()){StatusCode = (int)HttpStatusCode.OK};
    }
    [HttpGet]
    [Route("/user")] 
    public ActionResult GetUser(  )
    {
        

        IEnumerable<UserOld> userQuery =
            from user1 in Repo.UserDb
            where user1.Token.Equals(Request.Headers.Authorization) 
            select user1;
        
        
        return new ObjectResult(userQuery.Last()){StatusCode = (int)HttpStatusCode.OK};
    }

    [HttpPut]
    [Route("/user")]
    public ActionResult PutUser( [FromBody] UserRequestEnv<UserRequest> req)
    {
        foreach( UserOld cust in Repo.UserDb)
     {
         if (cust.Token == Request.Headers.Authorization ) 
         {
             cust.Email= req.User.Email;
             return new ObjectResult(cust){StatusCode = (int)HttpStatusCode.OK};
         }
     }
        return new ObjectResult("no user "){StatusCode = (int)HttpStatusCode.NotFound};
        
    }

    [HttpPost]
    [Route("/profiles")]
    public ActionResult postProfiles([FromBody] ProfileRequestEnv<Profile> req)
    { 
        Profile profile = new Profile(req.profile.username, req.profile.bio, req.profile.image,req.profile.following);
     ProfileRequestEnv<Profile> root = new ProfileRequestEnv<Profile>(profile);
     Repo.profileDb.Add(root);
     return new ObjectResult(root){StatusCode = (int)HttpStatusCode.Accepted};
        
    }
    [HttpGet]
    [Route("/profiles/{username}")]
    public ActionResult Getprofiles(String username){

            IEnumerable<ProfileRequestEnv<Profile>> profileQuery =
                from Profile in Repo.profileDb
                where Profile.profile.username.Equals(username)
                select Profile;
            
            return new ObjectResult(profileQuery.Last()){StatusCode = (int)HttpStatusCode.OK};
        }
    [HttpPost]
    [Route("/profiles/{username}/follow")]
    public ActionResult postFollowProfiles(String username){

        foreach( ProfileRequestEnv<Profile> cust in Repo.profileDb)
        {
            if (cust.profile.username.Equals(username) == true )
            {
                cust.profile.following = true;
                return new ObjectResult(cust){StatusCode = (int)HttpStatusCode.OK};
            }
        }
        return new ObjectResult("user profile "){StatusCode = (int)HttpStatusCode.NoContent};
    }
    [HttpDelete]
    [Route("/profiles/{username}/follow")]
    public ActionResult Deleteprofiles(String username){

     foreach( ProfileRequestEnv<Profile> cust in Repo.profileDb)
     {
         if (cust.profile.username.Equals(username) == true )
         {
             cust.profile.following = false;
             return new ObjectResult(cust){StatusCode = (int)HttpStatusCode.OK};
         }
     }
     return new ObjectResult("user profile "){StatusCode = (int)HttpStatusCode.NoContent};
    }
    
    [HttpGet]
    [Route("/tags")]
    public ActionResult getTags(){

        return new ObjectResult( new tagseRequestEnv<Array>(Repo.tags) ){StatusCode = (int)HttpStatusCode.OK};
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
     Article curent = new Article(req.article.slug, req.article.title, req.article.description, req.article.body,
         req.article.tagList, req.article.createdAt, req.article.updatedAt, req.article.favorited,
         req.article.favoritesCount, req.article.author);
     
     Repo.allArticle.addToArticle( curent);
     ArticleRequestEnv<Article> root = new ArticleRequestEnv<Article>(curent);
     return new ObjectResult(root ){StatusCode = (int)HttpStatusCode.OK};
   
    }
    
    [HttpGet]
    [Route("/articles")]
    public ActionResult getaArticles(){
        if(! (string.IsNullOrEmpty(Request.Query["favorited"])))
        {
            IEnumerable<Article> que  =
                from Article in Repo.allArticle.articles.ToArray()
                where Article.author.username.Equals(Request.Query["favorited"])
                select Article;
            return new ObjectResult( que ){StatusCode = (int)HttpStatusCode.OK};
        }
        
        
        if(! (string.IsNullOrEmpty(Request.Query["author"])))
        {
            IEnumerable<Article> que  =
                from Article in Repo.allArticle.articles.ToArray()
                where Article.author.username.Equals(Request.Query["author"])
                select Article;
            return new ObjectResult( que ){StatusCode = (int)HttpStatusCode.OK};
        }
        
        if(! (string.IsNullOrEmpty(Request.Query["tag"])))
        {
            IEnumerable<Article> que  =
                from Article in Repo.allArticle.articles.ToArray()
                where Article.tagList.Contains(Request.Query["tag"])
                select Article;
            return new ObjectResult( que ){StatusCode = (int)HttpStatusCode.OK};
        }
        
        return new ObjectResult( Repo.allArticle ){StatusCode = (int)HttpStatusCode.OK};
    }
    
    [HttpGet]
    [Route("/articles/feed")]
    public ActionResult getArticlesFeed(){

       
        IEnumerable<UserOld> userQuery =
            from user1 in Repo.UserDb
            where user1.Token.Equals(Request.Headers.Authorization) 
            select user1;
        
        IEnumerable<Article> que  =
            (from Article in Repo.allArticle.articles.ToArray()
            where Article.author.following.Equals(true)
            select Article).Take(5);
        
        return new ObjectResult(que){StatusCode = (int)HttpStatusCode.OK};
    }

    
}
