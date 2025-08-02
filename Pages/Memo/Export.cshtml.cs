using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace App.Pages.Memo
{
    public class ExportModel : PageModel
    {
        [BindProperty]
        public string FilePath { get; set; } = "";
        
        [BindProperty]
        public string ExportFormat { get; set; } = "numbered";
        
        public string ResultMessage { get; set; } = "";
        public bool IsSuccess { get; set; } = false;
        public string SavedFilePath { get; set; } = "";
        
        public List<string> Memos => SharedData.Memos;
        public int MemoCount => SharedData.Memos.Count;

        public void OnGet()
        {
            // GET 요청 시 기본값 설정
        }

        public IActionResult OnPost()
        {
            if (SharedData.Memos.Count == 0)
            {
                ResultMessage = "내보낼 메모가 없습니다.";
                IsSuccess = false;
                return Page();
            }

            if (string.IsNullOrEmpty(FilePath?.Trim()))
            {
                ResultMessage = "파일 경로를 입력해주세요.";
                IsSuccess = false;
                return Page();
            }

            try
            {
                var content = GenerateContent();
                System.IO.File.WriteAllText(FilePath.Trim(), content, Encoding.UTF8);
                
                ResultMessage = $"성공적으로 저장되었습니다! ({SharedData.Memos.Count}개 메모)";
                IsSuccess = true;
                SavedFilePath = FilePath.Trim();
            }
            catch (Exception ex)
            {
                ResultMessage = $"파일 저장 중 오류가 발생했습니다: {ex.Message}";
                IsSuccess = false;
            }

            return Page();
        }

        private string GenerateContent()
        {
            var content = new StringBuilder();
            
            switch (ExportFormat)
            {
                case "numbered":
                    content.AppendLine("=== 메모 목록 (번호 매기기) ===\n");
                    for (int i = 0; i < SharedData.Memos.Count; i++)
                    {
                        content.AppendLine($"{i + 1}. {SharedData.Memos[i]}");
                    }
                    break;
                    
                case "timestamp":
                    content.AppendLine("=== 메모 목록 (시간순) ===\n");
                    foreach (var memo in SharedData.Memos.OrderBy(m => m))
                    {
                        content.AppendLine(memo);
                    }
                    break;
                    
                case "simple":
                    foreach (var memo in SharedData.Memos)
                    {
                        // [timestamp] 부분 제거하고 메모 내용만
                        var cleanMemo = memo.Contains("] ") ? memo.Substring(memo.IndexOf("] ") + 2) : memo;
                        content.AppendLine(cleanMemo);
                    }
                    break;
                    
                case "detailed":
                    content.AppendLine("=== 상세 메모 목록 ===");
                    content.AppendLine($"생성일시: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    content.AppendLine($"총 메모 개수: {SharedData.Memos.Count}개\n");
                    
                    for (int i = 0; i < SharedData.Memos.Count; i++)
                    {
                        content.AppendLine($"[{i + 1:D3}] {SharedData.Memos[i]}");
                    }
                    break;
            }
            
            content.AppendLine($"\n--- 파일 생성 시간: {DateTime.Now:yyyy-MM-dd HH:mm:ss} ---");
            return content.ToString();
        }
    }
}
