using Microsoft.EntityFrameworkCore;
using TireEstimateAspNet.Models;

namespace TireEstimateAspNet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 見積もり情報を格納するDbSet（見積テーブル）
        public DbSet<Quote> Quotes { get; set; }

        //　タイヤ在庫情報を格納するDbSet（タイヤ在庫テーブル）
        public DbSet<TireInventory> TireInventories { get; set; }
    }

}
