using OneHandTraining.Interface;
using OneHandTraining.model;

namespace OneHandTraining.Decorator;


public class SqlAuditDecorator: IUsersRepository 
{
    private readonly SqliteUserRepository _inner;

    public SqlAuditDecorator(SqliteUserRepository inner)
    {
        _inner = inner;
    }
    
    public UserOld Add(UserOld entity)
    {
        if (entity.CreatedOn == null || entity.CreatedOn == default(DateTime))
        {
            entity.CreatedOn = DateTime.Now;
        }
        entity.ModifiedOn = DateTime.Now;
        var x = _inner.Add(entity);
        return x;
    }

    public bool Delete(string entity)
    {
        return _inner.Delete(entity);
    }

    public int Update(UserOld entity)
    {
       return _inner.Update(entity);
    }

    public UserOld findByToken(string token)
    {
        return _inner.findByToken(token);
    }

    public UserOld findLoginUser(string email, string password)
    {
        return _inner.findLoginUser(email, password);
    }


    public List<UserOld> findUsersGeneric(Func<UserOld, bool> pred)
    {
        return _inner.findUsersGeneric(pred);
    }
}
