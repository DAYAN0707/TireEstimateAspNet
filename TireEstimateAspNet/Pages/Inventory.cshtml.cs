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

        [BindProperty]
        // 在庫情報を格納するプロパティ
        public TireInventory TireInventory { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        // 検索用のタイヤサイズを格納するプロパティ(URLクエリ文字列からGETで取得するため SupportsGet = true)
        public string SearchTireSize { get; set; } = string.Empty;



        // 在庫一覧を画面に渡すためのリスト
        public List<TireInventory> InventoryList { get; set; } = new();



        [BindProperty]
        public TireInventory NewInventory { get; set; } = new();


        // ページ読み込み時に DB からタイヤサイズ順で取得
        // GETリクエストを非同期で処理
        public async Task OnGetAsync()
        {
            // 1. クエリの土台（SELECT * FROM TireInventories の準備）
            var query = _context.TireInventories.AsQueryable();

            // 2. 検索条件が入力されている場合のみ WHERE 句を追加
            if (!string.IsNullOrEmpty(SearchTireSize))
            {
                // 検索用のタイヤサイズが指定されている場合は、部分一致でフィルタリング
                query = query.Where(q => q.TireSize.Contains(SearchTireSize));
            }

            // 3. 並び替えを行って、最後にデータベースからデータを取得（SQL発行）
            InventoryList = await query
                .OrderBy(q => q.TireSize)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // タイヤサイズが入力されている場合だけ絞り込み
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

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // 指定されたIDの在庫情報を取得
            var tire = await _context.TireInventories.FindAsync(id);
            if (tire != null)
            {
                // 在庫情報が存在する場合は削除
                _context.TireInventories.Remove(tire);
                await _context.SaveChangesAsync();
            }
            // 削除後は在庫一覧ページにリダイレクト
            return RedirectToPage("./Inventory");
        }
    }
}