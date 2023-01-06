using OneHandTraining.Interface;
using OneHandTraining.model;

namespace OneHandTraining.Repository;

public class InMemoryUserRepository :IUsersRepository
{

    public UserOld Add(UserOld entity)
    {
        var userOld = new UserOld(entity.Username, entity.Email,entity.Password,"","asdsdasd",$"{Guid.NewGuid()}");
        Repo.UserDb.Add(userOld);
        return userOld;
    }

    public bool Delete(string entity)
    {
       var userOld = Repo.UserDb.Last(x => x.Id ==int.Parse(entity));
       return Repo.UserDb.Remove(userOld);
    }

    public int Update(UserOld entity)
    {

       //var curentuser = Repo.UserDb.Where(x => x.Email == entity.Email).First();
        
        foreach( UserOld cust in Repo.UserDb)
        {
            if (cust.Email == entity.Email ) 
            {
                cust.Email= entity.Email;
                return 1;
            }
        }

        return 0;
    }

    public UserOld findByToken(string token)
    {
        IEnumerable<UserOld> userQuery =
            from user1 in Repo.UserDb
            where user1.Token.Equals(token) 
            select user1;
        
        
        return userQuery.Last();
    }

    public UserOld findLoginUser(string email, string password)
    {
        IEnumerable<UserOld> userQuery =
            from user1 in Repo.UserDb
            where user1.Email.Equals(email) && user1.Password.Equals(password)
            select user1;
        
        
        return userQuery.Last();
    }

    public List<UserOld> findUsersGeneric(Func<UserOld, bool> pred)
    {
        throw new NotImplementedException();
    }

}