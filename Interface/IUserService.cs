using OneHandTraining.DTO;
using OneHandTraining.model;

namespace OneHandTraining.Interface;

public interface IUserService
{
    UserRequest Adduser(UserRequest user);
    UserOld findUserByEmail(loginUserRequest user);
    UserOld findUserByToken( String authorization);
    UserOld updateEmail(string authorization, emailUserRequest user);
}