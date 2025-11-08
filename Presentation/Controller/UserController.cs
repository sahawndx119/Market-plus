using MaretOrg2.Application;
using MaretOrg2.Application.Interface;
using MaretOrg2.Domain;
using MaretOrg2.Presentation.DTO.IDHolders;
using MaretOrg2.Presentation.DTO.RequestModel;
using MaretOrg2.Presentation.DTO.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace MaretOrg2.Presentation.Controller;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    UserService userService = new UserService();
    ItemService itemService = new ItemService();

    [HttpPost("signUp")]
    public IActionResult SignUp([FromBody] SignupRequest user)
    {
        int id;
        try
        {
            id = userService.SignUp(user.UserName, user.Password, user.Role, user.Rolepassword);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        UserContext.CurrentUserId = id;
        UserContext.CurrentRole = user.Role;
        return Ok("You have successfully signed up");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest user)
    {
        int id;
        string role;
        try
        {
            id = userService.Login(user.Username, user.Password);
            UserContext.CurrentUserId = id;
            role = userService.GetRoleById(id);
            UserContext.CurrentRole = role;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        UserContext.CurrentRole = userService.GetRoleById(id);

        return Ok("You have successfully logged in");
    }

    [HttpPut("Change-info")]
    public IActionResult ChangeInfo([FromBody] ChangeInfoRequest user)
    {
        try
        {
            userService.ChangeInfo(UserContext.CurrentUserId, user.UserName, user.Password);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Your info has been changed successfully");
    }

    [HttpGet("purchase-list")]
    public IActionResult PurchasesList()
    {
        List<ItemSummaryResponse> items = new();
        try
        {
            var list = userService.GetAllItems(UserContext.CurrentUserId);
            foreach (var item in list)
            {
                items.Add(new ItemSummaryResponse(item));
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }



        return Ok(items);
    }

    [HttpDelete("remove-item-from-list/{name}")]
    public IActionResult RemoveItemFromList([FromRoute] string name)
    {
        try
        {
            userService.RemoveItemFromItemList(name, UserContext.CurrentUserId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("The item has been removed from the list successfully");
    }

    [HttpDelete("logout")]
    public IActionResult Logout()
    {
        if (UserContext.CurrentUserId == null)
        {
            return BadRequest("You have to sign up/login first");
        }
        else
        {
            UserContext.CurrentUserId = null;
            UserContext.CurrentRole = null;
        }
        return Ok("You have successfully logged out");
    }

}