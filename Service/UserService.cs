using AutoMapper;
using OneHandTraining.DTO;
using OneHandTraining.Interface;
using OneHandTraining.model;
namespace OneHandTraining.Service;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;

    public UserService(IUsersRepository usersRepository,IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
     public UserRequest Adduser(UserRequest user)
     {
         
          var userOld = _mapper.Map<UserRequest, UserOld>(user);
          return _mapper.Map<UserOld,UserRequest>(_usersRepository.Add(userOld));
        
    }

     public UserOld findUserByEmail(loginUserRequest User)
     {
         return   _usersRepository.findLoginUser(User.Email,User.Password);
     }

     public UserOld findUserByToken( String Authorization)
    {
        return _usersRepository.findByToken(Authorization);
    }

     public UserOld updateEmail(string Authorization, emailUserRequest User)
    {
        var user = _usersRepository.findByToken(Authorization);
        var resp = new UserOld(user.Username, User.Email,
            user.Password,$"{Authorization}",user.Bio,user.Image);
        _usersRepository.Update(resp);
        return resp;
        
        

    }
}