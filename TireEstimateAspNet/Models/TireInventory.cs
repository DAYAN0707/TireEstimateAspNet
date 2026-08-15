namespace TireEstimateAspNet.Models
{
    public class TireInventory
    {
        public int Id { get; set; }

        // タイヤサイズ (例: 195/65R15)
        public string TireSize { get; set; } = string.Empty;

        // タイヤの種類 (例: サマータイヤ / スタッドレス)
        public string Season { get; set; } = string.Empty;

        // 在庫本数
        public int StockQuantity { get; set; }

        // 単価
        public int UnitPrice { get; set; }
    }
}