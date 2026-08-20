using Microsoft.AspNetCore.Mvc;
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


        [BindProperty]
        public TireInventory NewInventory { get; set; } = new ();


        // ページ読み込み時に DB からタイヤサイズ順で取得
        public async Task OnGetAsync()
        {
            InventoryList = await _context.TireInventories
                .OrderBy(q => q.TireSize)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid)
            {
                // 入力値が不正な場合は、在庫一覧を再取得して画面に戻す
                InventoryList = await _context.TireInventories
                    .OrderBy(q => q.TireSize)
                    .ToListAsync();

                return Page();
            }

            // 入力値が有効な場合は、新しい在庫を DB に追加
            _context.TireInventories.Add(NewInventory);

            //SQL Server での自動採番を利用する場合、ID は自動的に設定されるため、ここでは設定しない
            await _context.SaveChangesAsync();

            // 保存後は在庫一覧ページにリダイレクト
            return RedirectToPage("./Inventory");
        }
    }
}