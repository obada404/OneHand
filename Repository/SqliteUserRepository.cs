using OneHandTraining.Interface;
using OneHandTraining.model;
using OneHandTraining.Models;

namespace OneHandTraining;

public class SqliteUserRepository :IUsersRepository
{
    readonly private oneHandContext _context;

    public SqliteUserRepository(oneHandContext context)
    {
        _context = context;
    }
    
    public UserOld Add(UserOld entity)
    {

        _context.UserOldDBs.Add(entity);
        _context.SaveChanges(); 
        return entity; 
    }


    public bool Delete(string entity)
    {
        var userOld = _context.UserOldDBs.First(x=> x.Token == entity);
        _context.UserOldDBs.Remove(userOld);
        return _context.SaveChanges()!=0;
    }
    

    public void Delete(UserOld entity)
    {
        _context.UserOldDBs.Remove(entity);
        _context.SaveChanges();
    }

    public int Update(UserOld entity)
    {
        var olduser = _context.UserOldDBs.Find(entity.Id);
        if (olduser != null)
        {
            var ee= _context.Entry(olduser);
            ee.CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return 1;
        }
        return 0;
    }

    public UserOld findByToken(string token)
    {
        return _context.UserOldDBs.Single(x => x.Token == token);
    }

    public UserOld findLoginUser(string email, string password)
    {
        return _context.UserOldDBs.First(x => x.Email == email && x.Password == password);
    }

    public List<UserOld> findUsersGeneric(Func<UserOld, bool> pred)
    {
        return _context.UserOldDBs.Where(pred).ToList();
    }
}