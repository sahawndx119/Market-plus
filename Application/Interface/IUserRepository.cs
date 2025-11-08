using MaretOrg2.Domain;

namespace MaretOrg2.Application.Interface;

public interface IUserRepository
{
    public void AddUserToDatabase(User user);
    public int GetUserIdByUserName(string userName , string password);
    public User GetUserByUserId(int UserID , bool withItemsFlag = false);
    public void RemoveUserFromDatabaseById(User user);
    
    public void BuyHanlder();
    public void UpdateUserInfo(User user);
    public string GetUserRoleById(int userId);
}