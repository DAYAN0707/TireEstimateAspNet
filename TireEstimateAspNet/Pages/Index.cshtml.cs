using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TireEstimateAspNet.Data; // Dbcontextのあるフォルダ
using TireEstimateAspNet.Models; // Quoteモデルのあるフォルダ
using Microsoft.EntityFrameworkCore; // DbContextを使用するために必要

namespace TireEstimateAspNet.Pages
{

// Indexページのモデル
public class IndexModel : PageModel
{
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string CustomerName { get; set; } = string.Empty;

        [BindProperty]
        public string TireSize { get; set; } = string.Empty;

        [BindProperty]
        public int UnitPrice { get; set; } = 10000;

        [BindProperty]
        public int Quantity { get; set; } = 4;

        public int TotalAmount { get; set; }
        public bool IsCalculated { get; set; } = false;

        // 一覧を表示するためのプロパティ（画面に表示する見積一覧を入れておく箱）
        public List<Quote> QuoteList { get; set; } = new();

        // ページが読み込まれたときに、データベースから見積もりの一覧を取得する
        public async Task OnGetAsync()
        {
            QuoteList = await _context.Quotes
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }


        //　見積もりを計算してデータベースに保存するためのPOSTハンドラー
        public async Task<IActionResult> OnPostAsync()
        {
            TotalAmount = UnitPrice * Quantity;

            //　データベースに見積もりを保存する
            var quote = new Quote
            {
                CustomerName = string.IsNullOrWhiteSpace(CustomerName) ? "ゲスト" : CustomerName,
                TireSize = TireSize,
                UnitPrice = UnitPrice,
                Quantity = Quantity,
                TotalAmount = TotalAmount,
                CreatedAt = DateTime.Now
            };

            //　Quotesテーブルを見る
            _context.Quotes.Add(quote);

            await _context.SaveChangesAsync();

            IsCalculated = true;

            //　データベースから最新の見積もり一覧を取得する（保存→一覧取得→画面更新）
            QuoteList = await _context.Quotes
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
            //　List<Quote> に変換→QuoteList に入れる

            // ページを再表示する
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote != null)
            {
                _context.Quotes.Remove(quote);
                await _context.SaveChangesAsync();
            }
            // データベースから最新の見積もり一覧を取得する
            QuoteList = await _context.Quotes
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
            return Page();
        }
        
        public async Task<IActionResult> OnPostClearAsync(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
            {
                return NotFound();
            }
            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            // 削除後に再読み込みで画面の二重送信を防ぎ、OnGetAsyncが最新の見積もり一覧を取得する
            return RedirectToPage();
        }
    }
}