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
    
    public int Add(UserOld entity)
    {
        if (entity.CreatedOn == null || entity.CreatedOn == default(DateTime))
        {
            entity.CreatedOn = DateTime.Now;
        }
        entity.ModifiedOn = DateTime.Now;
        var x = _inner.Add(entity);
        return x;
    }

    public void Delete(string entity)
    {
        _inner.Delete(entity);
    }

    public void Update(UserOld entity)
    {
        _inner.Update(entity);
    }

    public UserOld GetByToken(string token)
    {
        return _inner.GetByToken(token);
    }

    public UserOld GetLoginUser(string email, string password)
    {
        return _inner.GetLoginUser(email, password);
    }


    public List<UserOld> GetUsersGeneric(Func<UserOld, bool> pred)
    {
        return _inner.GetUsersGeneric(pred);
    }
}
