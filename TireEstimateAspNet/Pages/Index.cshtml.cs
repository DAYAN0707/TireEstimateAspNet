using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TireEstimateAspNet.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int UnitPrice { get; set; } = 10000;

        [BindProperty]
        public int Quantity { get; set; } = 4;

        public int TotalAmount { get; set; }
        public bool IsCalculated { get; set; } = false;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            TotalAmount = UnitPrice * Quantity;
            IsCalculated = true;
        }
    }
}