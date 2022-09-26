using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.DTO;
using OneHandTraining.Interface;
using OneHandTraining.model;

namespace OneHandTraining;

public interface IUserService
{
    UserOld Adduser(UserRequest User);
    UserOld findUserByEmail(loginUserRequest User);
    UserOld findUserByToken( String Authorization);
    UserOld updateEmail(string Authorization, emailUserRequest User);
}

public class UserService : IUserService
{
    
    private readonly IUsersRepository _usersRepository;

    public UserService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
     public UserOld Adduser(UserRequest User)
    {
        
        var resp = new UserOld(User.Username, User.Email, 
            User.Password,"","asdsdasd",$"{Guid.NewGuid()}");
        _usersRepository.Add(resp);
        return resp;
        
    }

     public UserOld findUserByEmail(loginUserRequest User)
     {

       
       return   _usersRepository.GetLoginUser(User.Email,User.Password);
    }

     public UserOld findUserByToken( String Authorization)
    {
        return _usersRepository.GetByToken(Authorization);

        
    }

     public UserOld updateEmail(string Authorization, emailUserRequest User)
    {
        var user = _usersRepository.GetByToken(Authorization);
        var resp = new UserOld(user.Username, User.Email,
            user.Password,$"{Authorization}",user.Bio,user.Image);
        _usersRepository.Update(resp);
        return resp;
        
        

    }
}