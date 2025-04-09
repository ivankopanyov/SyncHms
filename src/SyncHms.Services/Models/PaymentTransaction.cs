namespace SyncHms.Services.Models;

public class PaymentTransaction
{
    [JsonProperty(PropertyName = "OrderNum")]
    public long OrderNumber { get; set; }

    [JsonProperty(PropertyName = "CloseTime")]
    public DateTime CloseDateTime { get; set; }
    
    [JsonProperty(PropertyName = "FiscalChequeNumber")]
    public string? FiscalChequeNumber { get; set; }

    [JsonProperty(PropertyName = "RestaurantSection.Id")]
    public string RestaurantSectionId { get; set; }
    
    [JsonProperty(PropertyName = "RestaurantSection")]
    public string RestaurantSectionName { get; set; }
    
    [JsonProperty(PropertyName = "DishCategory.Id")]
    public string? DishCategoryId { get; set; }
    
    [JsonProperty(PropertyName = "DishCategory")]
    public string? DishCategoryName { get; set; }
    
    [JsonProperty(PropertyName = "DishId")]
    public string DishId { get; set; }
    
    [JsonProperty(PropertyName = "DishName")]
    public string DishName { get; set; }
    
    [JsonProperty(PropertyName = "DishAmountInt")]
    public decimal DishAmountInt { get; set; }
    
    [JsonProperty(PropertyName = "PayTypes.GUID")]
    public string PayTypeId { get; set; }
    
    [JsonProperty(PropertyName = "PayTypes")]
    public string PayTypeName { get; set; }
    
    [JsonProperty(PropertyName = "DishSumInt")]
    public decimal DishSumInt { get; set; }
    
    [JsonProperty(PropertyName = "DiscountSum")]
    public decimal DiscountSum { get; set; }
    
    [JsonProperty(PropertyName = "IncreaseSum")]
    public decimal IncreaseSum { get; set; }
    
    [JsonProperty(PropertyName = "DishReturnSum")]
    public decimal DishReturnSum { get; set; }

    public override string ToString()
    {
        return $"CheckNumber: {OrderNumber}, Date: {CloseDateTime:yyyy-MM-dd HH:mm:s}, Section: {RestaurantSectionName}"
               + $"\nPosition: {DishName}, Category: {DishCategoryName}, PayType: {PayTypeName}"
               + $"\nSum: {DishSumInt + DishReturnSum}, Discount: {DiscountSum}, Increase: {IncreaseSum}";
    }
}
