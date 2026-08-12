using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TireEstimateAspNet.Data;
using TireEstimateAspNet.Models;

namespace TireEstimateAspNet.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // フォームから送信されるデータを自動的に受け取る
        [BindProperty]
        public Quote Quote { get; set; } = default!;

        // 1. 編集画面を開いた時（初期表示）
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // 指定されたIDのデータをDBから探す
            var quote = await _context.Quotes.FirstOrDefaultAsync(m => m.Id == id);

            if (quote == null)
            {
                return NotFound();
            }

            Quote = quote;
            return Page();
        }

        // 2. 【更新保存】ボタンを押した時
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 合計金額を再計算
            Quote.TotalAmount = Quote.UnitPrice * Quote.Quantity;

            // EF Core に「このデータは変更された」と伝える
            _context.Attach(Quote).State = EntityState.Modified;

            try
            {
                // DBに UPDATE クエリを発行！
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Quotes.Any(e => e.Id == Quote.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // 更新が終わったら一覧画面に戻る
            return RedirectToPage("./Index");
        }
    }
}