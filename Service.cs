using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.DTO;
using OneHandTraining.model;

namespace OneHandTraining;

public class Service
{
  
     static public ProfileRequestEnv<Profile> setProfile(Profile profile)
     {
         Profile pro = new Profile(profile.username, profile.bio,profile.image,profile.following);
         ProfileRequestEnv<Profile> root = new ProfileRequestEnv<Profile>(pro);
         Repo.profileDb.Add(root);
         return root;
     }
     static public ActionResult FindfollowingProfiles(String username)
     {
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
     static public ActionResult Deleteprofiles(String username)
     {
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
     static public IEnumerable<ProfileRequestEnv<Profile>> findProfileByAuthor(String username)
     {
       
         IEnumerable<ProfileRequestEnv<Profile>> profileQuery =
             from Profile in Repo.profileDb
             where Profile.profile.username.Equals(username)
             select Profile;

         return profileQuery;
     }
     
     
     
     
     static public ArticleRequestEnv<Article>  setArticle(ArticleRequest article)
     {
       
         Article curent = new Article(article.slug, article.title, article.description, article.body,
             article.tagList, article.createdAt, article.updatedAt,article.favorited,
             article.favoritesCount,article.author);
     
         Repo.allArticle.addToArticle( curent);
         ArticleRequestEnv<Article> root = new ArticleRequestEnv<Article>(curent);
         return root;
     }
     static public IEnumerable<Article> getaArticlesByFavorited( String favorited)
     {
         IEnumerable<Article> que  =
             from Article in Repo.allArticle.articles.ToArray()
             where Article.author.username.Equals(favorited)
             select Article;
         return que;

     }
     static public IEnumerable<Article> getaArticlesByAuthor( String author)
     {
         IEnumerable<Article> que  =
             from Article in Repo.allArticle.articles.ToArray()
             where Article.author.username.Equals(author)
             select Article;
         return que;

     }
     static public IEnumerable<Article> getaArticlesByTag( String Tag)
     {
         IEnumerable<Article> que  =
             from Article in Repo.allArticle.articles.ToArray()
             where Article.author.username.Equals(Tag)
             select Article;
         return que;

     }
     static public IEnumerable<Article> getArticlesFeed(String Authorization)
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
     
     
     
     
     
     
     
     
     
     
     static public UserOld setUser(UserRequest User)
     {
         var resp = new UserOld(User.Username, User.Email, User.Password,"","asdsdasd",$"{Guid.NewGuid()}");
         Repo.UserDb.Add(resp);
         return resp;
     }
     static public IEnumerable<UserOld> findUserByEmail(loginUserRequest User)
     {
         IEnumerable<UserOld> userQuery =
             from user1 in Repo.UserDb
             where user1.Email.Equals(User.Email) && user1.Password.Equals(User.Password)
             select user1;

         return userQuery;
     }
     static public IEnumerable<UserOld> findUserByToken( String Authorization)
     {
         IEnumerable<UserOld> userQuery =
             from user1 in Repo.UserDb
             where user1.Token.Equals(Authorization) 
             select user1;


         return userQuery;
     }
     static public ActionResult updateEmail( String Authorization ,emailUserRequest User )
     {
         foreach( UserOld cust in Repo.UserDb)
         {
             if (cust.Token == Authorization ) 
             {
                 cust.Email= User.Email;
                 return new ObjectResult(cust){StatusCode = (int)HttpStatusCode.OK};
             }
         }
         return new ObjectResult("no user "){StatusCode = (int)HttpStatusCode.NotFound};

     }
}

public class addUser
{
  
}