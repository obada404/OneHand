using AdventureWorks;
using Microsoft.EntityFrameworkCore;
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
    
    public async Task<UserOld> Add(UserOld entity)
    {
        var  userOld = _context.UserOldDBs.Add(entity).Entity;
        await _context.SaveChangesAsync();
        return userOld; 
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

    public UserOld Update(UserOld entity)
    {
        var olduser = _context.UserOldDBs.Find(entity.Id);
        if (olduser != null)
        {
            olduser.Email = entity.Email;
            olduser.Token = entity.Token;
            _context.SaveChangesAsync();
            return olduser;
        }
        return null ;
    }

    public UserOld findByEmailAndId(string email,int Id)
    {
        return _context.UserOldDBs.Single(x => x.Email == email && x.Id == Id);
    }

    public async Task<UserOld> findLoginUser(string email, string password)
    {  //jwt to service 
        var user= await _context.UserOldDBs.FirstAsync(x => x.Email == email && x.Password == password);
        return user;
    }

    public List<UserOld> findUsersGeneric(Func<UserOld, bool> pred)
    {
        return _context.UserOldDBs.Where(pred).ToList();
    }
}