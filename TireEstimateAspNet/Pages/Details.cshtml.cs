using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TireEstimateAspNet.Data;
using TireEstimateAspNet.Models;


namespace TireEstimateAspNet.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Quote? Quote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound(); // idがない場合（null）は404エラーを返す
            }

            Quote = await _context.Quotes.FirstOrDefaultAsync(m => m.Id == id);

            if (Quote == null)
            {
                return NotFound(); // 該当見積が存在しない場合も404エラーを返す
            }
            return Page();
        }
    }
}
