using System.ComponentModel.DataAnnotations;

namespace TireEstimateAspNet.Models;

public class Quote
{
    [Key] // 主キー（ID）と明示する
    public int Id { get; set; }

    public string CustomerName { get; set; } = "";
    public string TireSize { get; set; } = "";
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}