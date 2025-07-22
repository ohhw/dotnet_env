using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyConsoleApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<string> Memos { get; set; } = new List<string>();
        public int MemoCount => Memos.Count;
        public string UserName { get; set; } = "";

        public void OnGet(string name = "")
        {
            // SharedData에서 메모 데이터 가져오기
            Memos = SharedData.Memos;
            
            // 사용자 이름 설정
            if (!string.IsNullOrEmpty(name))
            {
                UserName = name;
                // 세션이나 쿠키에 저장할 수도 있지만, 지금은 간단하게 유지
            }
            else
            {
                UserName = "방문자"; // 기본값
            }
        }
        
        public IActionResult OnPost(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Index", new { name = userName });
            }
            return Page();
        }
    }
}
