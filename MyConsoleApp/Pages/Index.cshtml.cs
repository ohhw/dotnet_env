using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyConsoleApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<string> Memos { get; set; } = new List<string>();
        public int MemoCount => Memos.Count;

        public void OnGet()
        {
            // SharedData에서 메모 데이터 가져오기
            Memos = SharedData.Memos;
        }
    }
}
