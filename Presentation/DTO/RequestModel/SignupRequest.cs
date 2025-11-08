namespace MaretOrg2.Presentation.DTO.RequestModel;

public class SignupRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Rolepassword { get; set; }

    public SignupRequest(string userName, string password, string role, string rolepassword)
    {
        UserName = userName;
        Password = password;
        Role = role;
        Rolepassword = rolepassword;
    }

    public SignupRequest()
    {
    }
}