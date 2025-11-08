using MaretOrg2.Domain;

namespace MaretOrg2.Presentation.DTO.ResponseModel;

public class ItemSummaryResponseWithQuantity(Item item)
{
    public string Name { get; set; } = item.Name;
    public int Price { get; set; } = item.Price;

    public int Quantity { get; set; } = item.Quantity;
}