using OneHandTraining.model;

namespace OneHandTraining.Interface;


public interface IUsersRepository
{
    public UserOld Add(UserOld entity);
    public bool Delete(string entity);
    public int Update(UserOld entity);
    public UserOld findByToken(string token);
    public UserOld findLoginUser(string email, string password);
    public List<UserOld> findUsersGeneric(Func<UserOld,bool> pred);  
}