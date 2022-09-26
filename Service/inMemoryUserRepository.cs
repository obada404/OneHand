using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.Interface;
using OneHandTraining.model;

namespace OneHandTraining;

public class inMemoryUserRepository :IUsersRepository
{
    public int Add(UserOld entity)
    {
        var resp = new UserOld(entity.Username, entity.Email, 
            entity.Password,"","asdsdasd",$"{Guid.NewGuid()}");
        Repo.UserDb.Add(resp);
        return resp.Id;
    }

    public void Delete(string entity)
    {
       
    }

    public void Update(UserOld entity)
    {
        foreach( UserOld cust in Repo.UserDb)
        {
            if (cust.Email == entity.Email ) 
            {
                cust.Email= entity.Email;
               
            }
        }
       
    }

    public UserOld GetByToken(string token)
    {
        IEnumerable<UserOld> userQuery =
            from user1 in Repo.UserDb
            where user1.Token.Equals(token) 
            select user1;
        
        
        return userQuery.Last();
    }

    public UserOld GetLoginUser(string email, string password)
    {
        IEnumerable<UserOld> userQuery =
            from user1 in Repo.UserDb
            where user1.Email.Equals(email) && user1.Password.Equals(password)
            select user1;
        
        
        return userQuery.Last();
    }
/*List<UserOld>*/ 
    public void GetUsersGeneric(Func<UserOld, bool> pred)
    {
        //genaric code 
    }
}