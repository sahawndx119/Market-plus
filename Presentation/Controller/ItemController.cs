using MaretOrg2.Application;
using MaretOrg2.Domain;
using MaretOrg2.Presentation.DTO.IDHolders;
using MaretOrg2.Presentation.DTO.RequestModel;
using MaretOrg2.Presentation.DTO.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace MaretOrg2.Presentation.Controller;

[ApiController]
[Route("api/item")]
public class ItemController : ControllerBase
{
    UserService _userService = new UserService();
    ItemService _ItemService = new ItemService();

    [HttpPost("add-item")]
    public IActionResult AddItem([FromBody] AddItemRequest request)
    {
        try
        {
            _ItemService.AddItem(request.Name, request.Price, request.Quantity , UserContext.CurrentRole);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Item added successfully");
    }


    [HttpGet("get-all")]
    public IActionResult GetAll()
    {
        var list = _ItemService.GetItems();
        List<ItemSummaryResponseWithQuantity> listOutput = new();
        foreach (var item in list)
        {
            listOutput.Add(new ItemSummaryResponseWithQuantity(item));
        }

        return Ok(listOutput);
    }

    [HttpGet("item-by-name/{name}")]
    public IActionResult ShowByName([FromRoute] string name)
    {
        try
        {
            var holder = (_ItemService.Search(name));
            ItemContext.CurrentItemId = holder.ItemId;
            return Ok(new ItemSummaryResponse(holder));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("buy")]
    public IActionResult Buy()
    {
        try
        {
            _ItemService.Buy(ItemContext.CurrentItemId, UserContext.CurrentUserId);
            ItemContext.CurrentItemId = null;
        }
        catch (Exception e)
        {
            ItemContext.CurrentItemId = null;
            return BadRequest(e.Message);
        }

        return Ok("The item has been successfully added to your purchase list");
    }

    [HttpPut("update-info")]
    public IActionResult UpdateInfo([FromBody] UpdateItemRequest request)
    {
        try
        {
            _ItemService.updateItem(ItemContext.CurrentItemId, request.Name, request.Price, request.Quantity , UserContext.CurrentRole);
            ItemContext.CurrentItemId = null;
        }
        catch (Exception e)
        {
            ItemContext.CurrentItemId = null;
            return BadRequest(e.Message);
        }
        return Ok("Item has been successfully updated");
    }

    [HttpDelete("delete-item")]
    public IActionResult DeleteItem()
    {
        try
        {
            _ItemService.deleteItem(ItemContext.CurrentItemId , UserContext.CurrentRole);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok("The item has been deleted successfully");
    }
}