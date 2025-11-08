using System.ComponentModel.DataAnnotations;

namespace MaretOrg2.Domain;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string Username {get; set;}
    public string Password {get; set;}
    public string Role {get; set;}
    public List<Item> Items { get; set; } = new();

    private readonly string RolePass = ("King101029");

    public string GetRolePass()
    {
        return RolePass;
    }
    

    public User() { }
    public User(string username, string password, string role, string? rolePass = null)
    {
        Username = username;
        Password = password;
        if (role.ToLower() == "admin")
        {
            if (RolePass == rolePass)
            {
                Role = "Admin";
            }
            else
            {
                throw new Exception("Your password for accessing to admin role is not correct!");
            }
        }
        else
        {
            Role = "User";
        }
    }
}