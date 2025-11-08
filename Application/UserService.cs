using MaretOrg2.Application.Interface;
using MaretOrg2.Domain;
using MaretOrg2.Infrastructure.Services;

namespace MaretOrg2.Application;

public class UserService : IUserService
{
    private readonly IUserRepository _repo = new UserRepository();
    private readonly IItemRepository _itemRepo = new ItemRepository();

    public int Login(string username, string password)
    {
        int UserId;
        try
        {
            UserId = _repo.GetUserIdByUserName(username, password);
        }
        catch (Exception ex)
        {
            throw new Exception("username or password is incorrect.");
        }

        return UserId;
    }

    public int SignUp(string Username, string Password, string Role, string? RolePass = null)
    {
        if (Username.Length < 3)
        {
            throw new Exception("Username is too short.");
        }

        if (Password.Length < 3)
        {
            throw new Exception("Password is too short.");
        }

        int id;
        try
        {
            var user = new User(Username, Password, Role, RolePass);
            _repo.AddUserToDatabase(user);
            id = _repo.GetUserIdByUserName(Username, Password);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return id;
    }

    public void Logout(int? UserID = null)
    {
        if (UserID == null)
        {
            throw new Exception("Please Login/Signup first");
        }
    }


    public void ChangeInfo(int? userId = null, string? newUserName = null, string? newPassword = null)
    {
        if (userId == null)
        {
            throw new Exception("Please Login/Signup first");
        }

        int id = userId ?? -1;

        if (!string.IsNullOrEmpty(newUserName))
        {
            if (newUserName.Length < 3)
            {
                throw new Exception("Username is too short.");
            }
        }
        else
        {
            newUserName = null;
        }

        if (!string.IsNullOrEmpty(newPassword))
        {
            if (newPassword.Length < 3)
            {
                throw new Exception("Password is too short.");
            }
        }
        else
        {
            newPassword = null;
        }

        var userHolder = _repo.GetUserByUserId(id);
        string UserName = newUserName ?? userHolder.Username;
        string Password = newPassword ?? userHolder.Password;

        userHolder.Username = UserName;
        userHolder.Password = Password;

        _repo.UpdateUserInfo(userHolder);
    }

    public void RemoveItemFromItemList(string? itemName = null, int? userId = null)
    {
        if (userId == null)
        {
            throw new Exception("Please Login/Signup first");
        }

        int id = userId ?? -1;

        var user = _repo.GetUserByUserId(id, true);

        int itemIndex = user.Items.FindIndex(item => item.Name == itemName);
        int itemId = user.Items[itemIndex].ItemId;
        if (itemId == -1)
        {
            throw new Exception("Please select the item by name after you saw them in your purchase list");
        }

        var userHolder = _repo.GetUserByUserId(id, true);
        int wantedIndex = userHolder.Items.FindIndex(item => item.ItemId == itemId);
        if (wantedIndex == -1)
        {
            throw new Exception("There no some item like this !");
        }

        userHolder.Items.RemoveAt(wantedIndex);

        var itemHolder = _itemRepo.GetItemById(itemId, true);
        int uIndex = itemHolder.Users.FindIndex(u => u.UserId == id);
        itemHolder.Users.RemoveAt(uIndex);
        itemHolder.Quantity++;
        _itemRepo.UpdateItem(itemHolder);
    }

    public string GetRoleById(int? userId = null)
    {
        if (userId == null)
        {
            throw new Exception("Please Login/Signup first");
        }

        var id = userId ?? -1;
        return _repo.GetUserRoleById(id);
    }


    public List<Item> GetAllItems(int? userId = null)
    {
        if (userId == null)
        {
            throw new Exception("Please Login/Signup first");
        }

        int id = userId ?? -1;
        var user = _repo.GetUserByUserId(id, true);
        return (user.Items);
    }
}