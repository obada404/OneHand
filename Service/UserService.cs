using AdventureWorks;
using AutoMapper;
using OneHandTraining.DTO;
using OneHandTraining.Interface;
using OneHandTraining.model;
namespace OneHandTraining.Service;

public class UserService : IUserService
{
    private readonly JwtManager _jwtManager;
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;

    public UserService(IUsersRepository usersRepository, IMapper mapper,JwtManager jwtManager)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _jwtManager = jwtManager;
    }

    public async Task<UserRequest> AdduserAsync(UserRegistrationRequest userRegistration)
    {

        var userOld = _mapper.Map<UserRegistrationRequest, UserOld>(userRegistration);

        return   _mapper.Map<UserOld, UserRequest>(await _usersRepository.Add(userOld));

    }

    public async Task<UserRequest> findUserByEmail(loginUserRequest User)
    {
        var user = await _usersRepository.findLoginUser(User.Email, User.Password);
        var jwt = _jwtManager.GenerateJwt(user.Id + "", user.Email);
        user.Token = jwt;
        _usersRepository.Update(user);
        return _mapper.Map<UserOld, UserRequest>(user);
    }

    public UserRequest findUserByEmailAndId(string email, int Id)
    {
        
        return _mapper.Map<UserOld,UserRequest>(_usersRepository.findByEmailAndId(email,Id));
    }



public UserRequest updateEmail(emailUserRequest User, int id)
{

    var user = _mapper.Map<emailUserRequest, UserOld>(User);
    user.Id = id;
   return _mapper.Map< UserOld,UserRequest> (_usersRepository.Update(user));
        
        

    }
}