using OneHandTraining.model;

namespace OneHandTraining.Interface;


public interface IUsersRepository
{
    public Task<UserOld> Add(UserOld entity);
    public bool Delete(string entity);
    public UserOld Update(UserOld entity);
    public UserOld findByEmailAndId(string email,int Id);
    public Task<UserOld> findLoginUser(string email, string password);
    public List<UserOld> findUsersGeneric(Func<UserOld,bool> pred);  
}