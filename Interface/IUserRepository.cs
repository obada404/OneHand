using OneHandTraining.model;

namespace OneHandTraining.Interface;


public interface IUsersRepository
{
    public int Add(UserOld entity);
    public void Delete(string entity);
    public void Update(UserOld entity);
    public UserOld GetByToken(string token);
    public UserOld GetLoginUser(string email, string password);
    public List<UserOld> GetUsersGeneric(Func<UserOld,bool> pred);  
}