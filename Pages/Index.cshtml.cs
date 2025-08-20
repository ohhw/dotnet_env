using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using App;

namespace App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        
        public List<string> Memos { get; set; } = new List<string>();
        public int MemoCount => Memos.Count;
        public string UserName { get; set; } = "";
        public string GoogleMapsApiKey { get; set; } = "";

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet(string name = "")
        {
            // SharedData에서 메모 데이터 가져오기
            Memos = SharedData.Memos;
            
            // API 키는 환경 변수에서 불러옴
            GoogleMapsApiKey = _configuration["GoogleMaps:ApiKey"] ?? 
                              Environment.GetEnvironmentVariable("GOOGLE_MAPS_API_KEY") ?? "";
            
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
        
        public IActionResult OnPost(string userName, string newMemo, string action)
        {
            // 메모 추가 기능
            if (action == "add" && !string.IsNullOrEmpty(newMemo))
            {
                SharedData.AddMemo(newMemo);
                return RedirectToPage("/Index");
            }
            
            // 사용자명 설정 기능
            if (!string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Index", new { name = userName });
            }
            return Page();
        }
    }
}
