using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyConsoleApp.Pages.Memo
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public string NewMessage { get; set; } = "";
        
        public string Message { get; set; } = "";

        public void OnGet(string message = "")
        {
            // URL 파라미터로 메시지가 전달된 경우 (기존 방식 호환)
            if (!string.IsNullOrEmpty(message))
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SharedData.Memos.Add($"[{timestamp}] {message}");
                Message = message;
            }
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(NewMessage?.Trim()))
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SharedData.Memos.Add($"[{timestamp}] {NewMessage.Trim()}");
                Message = NewMessage.Trim();
                NewMessage = ""; // 폼 클리어
            }

            return Page();
        }
    }
}
