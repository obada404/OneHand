using AutoMapper;
using OneHandTraining.DTO;
using OneHandTraining.Interface;
using OneHandTraining.model;
using OneHandTraining.Models;
using Profile = OneHandTraining.model.Profile;

namespace OneHandTraining.Repository;

public class SqliteProfileRepository:IprofileRepository
{
    private readonly oneHandContext _context;
    public SqliteProfileRepository(oneHandContext context,IMapper mapper)
    {
        _context = context;
    }
    public UserOld? FollowProfile(string username, string email)
    {
        var user = _context.UserOldDBs.First(x => x.Username == username);
        var follwingUser = _context.UserOldDBs.First(x => x.Email == email);
        if (user != null && follwingUser !=null)
        {
            user.followers.Add(follwingUser);
            follwingUser.following.Add(user);
            
        }
        _context.SaveChanges();
        return user;
    }
    public UserOld? unFollowProfile(string username, string email)
    {
        var user = _context.UserOldDBs.First(x => x.Username == email);
        var follwingUser = _context.UserOldDBs.First(x => x.Username == username);
        if (user != null )
        {
            user.followers.Remove(follwingUser);
        }
        _context.SaveChanges();
        return (user);
    }
    public List<UserOld> findProfileByAuthor(string username)
    {
        var result = _context.UserOldDBs.Where(x => x.Username == username).ToList();
        return result;
    }
}