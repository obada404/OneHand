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
    
    public int Add(UserOld entity)
    {

        _context.UserOldDBs.Add(entity);
        _context.SaveChanges(); 
        return entity.Id; 
    }


    public void Delete(string entity)
    {
        var u = _context.UserOldDBs.First(x=> x.Token == entity);
        _context.UserOldDBs.Remove(u);
        _context.SaveChanges();
    }
    

    public void Delete(UserOld entity)
    {
        _context.UserOldDBs.Remove(entity);
        _context.SaveChanges();
    }

    public void Update(UserOld entity)
    {
        _context.UserOldDBs.Update(entity);
        _context.SaveChanges();
    }

    public UserOld GetByToken(string token)
    {
        return _context.UserOldDBs.Single(x => x.Token == token);
    }

    public UserOld GetLoginUser(string email, string password)
    {
        return _context.UserOldDBs.First(x => x.Email == email && x.Password == password);
    }

    public List<UserOld> GetUsersGeneric(Func<UserOld, bool> pred)
    {
        return _context.UserOldDBs.Where(pred).ToList();
    }
}