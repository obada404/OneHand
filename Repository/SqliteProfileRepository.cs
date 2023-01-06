using AutoMapper;
using OneHandTraining.DTO;
using OneHandTraining.Interface;
using OneHandTraining.model;
using OneHandTraining.Models;
using Profile = OneHandTraining.model.Profile;

namespace OneHandTraining.Repository;

public class SqliteProfileRepository:IprofileRepository
{
    private readonly IMapper _mapper;
    private readonly oneHandContext _context;
    public SqliteProfileRepository(oneHandContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public ProfileRequestEnv<Profile> FollowProfile(string username, string email)
    {
        var user = _context.UserOldDBs.First(x => x.Username == email);
        var follwingUser = _context.UserOldDBs.First(x => x.Username == username);
        if (user.followers != null) user.followers.Add(follwingUser);
        _context.SaveChanges();
        return new ProfileRequestEnv<Profile>(_mapper.Map<UserOld, Profile>(user));
    }
    public ProfileRequestEnv<Profile> unFollowProfile(string username,string email)
    {
        var user = _context.UserOldDBs.First(x => x.Username == email);
        var follwingUser = _context.UserOldDBs.First(x => x.Username == username);
        if (user.followers != null) user.followers.Remove(follwingUser);
        _context.SaveChanges();
        return new ProfileRequestEnv<Profile>(_mapper.Map<UserOld, Profile>(user));
    }
    public List<UserOld> findProfileByAuthor(string username)
    {
        var result = _context.UserOldDBs.Where(x => x.Username == username).ToList();
        return result;
    }
}