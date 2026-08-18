using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TireEstimateAspNet.Data;
using TireEstimateAspNet.Models;

namespace TireEstimateAspNet.Pages
{
    public class InventoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InventoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // 在庫一覧を画面に渡すためのリスト
        public List<TireInventory> InventoryList { get; set; } = new();

        // ページ読み込み時に DB からタイヤサイズ順で取得
        public async Task OnGetAsync()
        {
            InventoryList = await _context.TireInventories
                .OrderBy(q => q.TireSize)
                .ToListAsync();
        }
    }
}