using MaretOrg2.Domain;

namespace MaretOrg2.Application.Interface;

public enum Key
{
    Available,
    Unavailable,
}

public interface IUserService
{
    public int Login(string username, string password); // int is for the User id
    public int SignUp(string Username , string Password , string Role , string? RolePass = null);
    public void Logout(int? UserID = null);
    public void ChangeInfo(int? UserID = null, string? newUserName = null , string? newPassword = null);
    public void RemoveItemFromItemList(string? itemName = null , int ? UserID = null);
    
    public string GetRoleById(int? userId = null);

    public List<Item> GetAllItems(int? userId = null);
}