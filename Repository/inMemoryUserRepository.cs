using OneHandTraining.Interface;
using OneHandTraining.model;

namespace OneHandTraining.Repository;

public class InMemoryUserRepository :IUsersRepository
{

    public async Task<UserOld> Add(UserOld entity)
    {
       // var userOld = new UserOld(entity.Username, entity.Email,entity.Password,"","asdsdasd",$"{Guid.NewGuid()}");
        Repo.UserDb.Add(entity);
        return entity;
    }

    public bool Delete(string entity)
    {
       var userOld = Repo.UserDb.Last(x => x.Id ==int.Parse(entity));
       return Repo.UserDb.Remove(userOld);
    }

    public UserOld Update(UserOld entity)
    {

       //var curentuser = Repo.UserDb.Where(x => x.Email == entity.Email).First();
        
        foreach( UserOld cust in Repo.UserDb)
        {
            if (cust.Email == entity.Email ) 
            {
                cust.Email= entity.Email;
                return cust;
            }
        }

        return null;
    }

    public UserOld findByEmailAndId(string email, int Id)
    {
        throw new NotImplementedException();
    }

    public UserOld findByToken(string token)
    {
        IEnumerable<UserOld> userQuery =
            from user1 in Repo.UserDb
            where user1.Token.Equals(token) 
            select user1;
        
        
        return userQuery.Last();
    }

    public async Task<UserOld> findLoginUser(string email, string password)
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