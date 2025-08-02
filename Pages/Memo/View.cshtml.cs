using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Memo
{
    public class ViewModel : PageModel
    {
        public List<string> Memos { get; set; } = new List<string>();

        public void OnGet()
        {
            Memos = SharedData.Memos;
        }
    }
}
