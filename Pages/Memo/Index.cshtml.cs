using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using App.Services;
using App.Models;

namespace App.Pages.Memo
{
    public class IndexModel : PageModel
    {
        private readonly MemoService _memoService;

        public IndexModel(MemoService memoService)
        {
            _memoService = memoService;
        }

        public List<MemoItem> RecentMemos { get; set; } = new List<MemoItem>();

        public async Task OnGetAsync()
        {
            try
            {
                // 최근 메모 6개 가져오기
                var allMemos = await _memoService.GetAllMemosAsync();
                RecentMemos = allMemos
                    .OrderByDescending(m => m.CreatedAt)
                    .Take(6)
                    .ToList();
            }
            catch (Exception ex)
            {
                // 로그 기록 (실제 환경에서는 로거 사용)
                Console.WriteLine($"메모 로드 오류: {ex.Message}");
                RecentMemos = new List<MemoItem>();
            }
        }

        public async Task<IActionResult> OnPostAddMemoAsync(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            {
                // 에러 메시지와 함께 페이지 다시 로드
                TempData["ErrorMessage"] = "제목과 내용을 모두 입력해주세요.";
                return RedirectToPage();
            }

            try
            {
                var newMemo = new MemoItem
                {
                    Id = Guid.NewGuid(),
                    Title = title.Trim(),
                    Content = content.Trim(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _memoService.AddMemoAsync(newMemo);
                TempData["SuccessMessage"] = "메모가 성공적으로 저장되었습니다!";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"메모 저장 오류: {ex.Message}");
                TempData["ErrorMessage"] = "메모 저장 중 오류가 발생했습니다.";
            }

            return RedirectToPage();
        }
    }
}
