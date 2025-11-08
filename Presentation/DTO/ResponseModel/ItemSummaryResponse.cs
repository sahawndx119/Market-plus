using MaretOrg2.Domain;

namespace MaretOrg2.Presentation.DTO.ResponseModel;

public class ItemSummaryResponse
{
    public string Name { get; set; }
    public int Price { get; set; }

    public ItemSummaryResponse(Item item)
    {
        Name = item.Name;
        Price = item.Price;
    }
}