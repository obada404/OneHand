using OneHandTraining.DTO;
using OneHandTraining.model;

namespace OneHandTraining.Interface;

public interface IUserService
{
    Task<UserRequest> AdduserAsync(UserRegistrationRequest userRegistration);
    Task<UserRequest> findUserByEmail(loginUserRequest user);
    UserRequest findUserByEmailAndId(string email, int Id);
    UserRequest updateEmail(emailUserRequest user, int id);
}