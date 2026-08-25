using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TireEstimateAspNet.Data;
using TireEstimateAspNet.Models;

namespace TireEstimateAspNet.Pages
{
    public class InventoryEditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InventoryEditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TireInventory TireInventory { get; set; } = default!;

        // 編集画面を開いたとき (IDを基に既存データを取得)
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tire = await _context.TireInventories
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tire == null)
            {
                return NotFound();
            }

            TireInventory = tire;

            return Page();
        }

        // 更新保存ボタンが押されたとき
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // エンティティの状態を「変更あり」に設定して更新フラグを立てる
            _context.Attach(TireInventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TireInventories.Any(e => e.Id == TireInventory.Id))
                {
                    return NotFound();
                }

                throw;
            }

            // 保存完了後は在庫一覧ページへ戻る
            return RedirectToPage("./Inventory");
        }
    }
}