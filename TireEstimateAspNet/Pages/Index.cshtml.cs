using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TireEstimateAspNet.Data; // Dbcontextのあるフォルダ
using TireEstimateAspNet.Models; // Quoteモデルのあるフォルダ

namespace TireEstimateAspNet.Pages
{
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

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            TotalAmount = UnitPrice * Quantity;

            var quote = new Quote
            {
                CustomerName = string.IsNullOrWhiteSpace(CustomerName) ? "ゲスト" : CustomerName,
                TireSize = TireSize,
                UnitPrice = UnitPrice,
                Quantity = Quantity,
                TotalAmount = TotalAmount,
                CreatedAt = DateTime.Now
            };

            _context.Quotes.Add(quote);

            await _context.SaveChangesAsync();

            IsCalculated = true;

            return Page();
        }
    }
}