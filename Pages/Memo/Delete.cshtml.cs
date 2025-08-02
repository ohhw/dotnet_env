using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Memo
{
    public class DeleteModel : PageModel
    {
        public bool IsDeleted { get; set; } = false;
        public int DeletedCount { get; set; } = 0;
        public int CurrentMemoCount => SharedData.Memos.Count;

        public void OnGet()
        {
            // GET 요청 시에는 삭제 확인 화면만 보여줌
        }

        public IActionResult OnPost()
        {
            DeletedCount = SharedData.MemoCount;
            SharedData.ClearAllMemos();
            IsDeleted = true;
            
            return Page();
        }
    }
}
