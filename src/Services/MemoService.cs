using System.Globalization;
using App.Models;

namespace App.Services
{
    /// <summary>
    /// 메모 관리를 위한 서비스 클래스
    /// </summary>
    public class MemoService
    {
        private static readonly List<MemoItem> _memos = new List<MemoItem>();

        /// <summary>
        /// 새 메모를 추가합니다
        /// </summary>
        /// <param name="memo">추가할 메모 아이템</param>
        /// <returns>추가 성공 여부</returns>
        public Task<bool> AddMemoAsync(MemoItem memo)
        {
            if (memo == null || string.IsNullOrWhiteSpace(memo.Title) || string.IsNullOrWhiteSpace(memo.Content))
            {
                return Task.FromResult(false);
            }

            try
            {
                memo.CreatedAt = DateTime.Now;
                memo.UpdatedAt = DateTime.Now;
                _memos.Add(memo);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        /// <summary>
        /// 새 메모를 추가합니다 (이전 버전 호환)
        /// </summary>
        /// <param name="content">메모 내용</param>
        /// <returns>추가 성공 여부</returns>
        public bool AddMemo(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return false;
            }

            try
            {
                var memo = new MemoItem
                {
                    Title = "빠른 메모",
                    Content = content.Trim()
                };
                _memos.Add(memo);
                
                // 이전 버전과 호환성을 위해 SharedData에도 추가
                SharedData.AddMemo(content.Trim());
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 모든 메모를 가져옵니다
        /// </summary>
        /// <returns>메모 목록</returns>
        public Task<List<MemoItem>> GetAllMemosAsync()
        {
            return Task.FromResult(_memos.ToList());
        }

        /// <summary>
        /// 메모 개수를 가져옵니다
        /// </summary>
        /// <returns>메모 개수</returns>
        public int GetMemoCount()
        {
            return SharedData.MemoCount;
        }

        /// <summary>
        /// 특정 인덱스의 메모를 삭제합니다
        /// </summary>
        /// <param name="index">삭제할 메모 인덱스</param>
        /// <returns>삭제 성공 여부</returns>
        public bool RemoveMemoAt(int index)
        {
            try
            {
                SharedData.RemoveMemoAt(index);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 모든 메모를 삭제합니다
        /// </summary>
        /// <returns>삭제 성공 여부</returns>
        public bool ClearAllMemos()
        {
            try
            {
                SharedData.ClearAllMemos();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 특정 키워드가 포함된 메모를 검색합니다
        /// </summary>
        /// <param name="keyword">검색 키워드</param>
        /// <returns>검색된 메모 목록</returns>
        public IEnumerable<(int Index, string Content)> SearchMemos(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return Enumerable.Empty<(int, string)>();
            }

            var normalizedKeyword = keyword.Trim().ToLowerInvariant();
            
            return SharedData.Memos
                .Select((memo, index) => new { Index = index, Content = memo })
                .Where(item => item.Content.ToLowerInvariant().Contains(normalizedKeyword))
                .Select(item => (item.Index, item.Content));
        }

        /// <summary>
        /// 메모 통계 정보를 가져옵니다
        /// </summary>
        /// <returns>메모 통계</returns>
        public MemoStatistics GetMemoStatistics()
        {
            var memos = SharedData.Memos;
            
            return new MemoStatistics
            {
                TotalCount = memos.Count,
                TotalCharacters = memos.Sum(m => m.Length),
                AverageLength = memos.Count > 0 ? memos.Average(m => m.Length) : 0,
                LongestMemo = memos.OrderByDescending(m => m.Length).FirstOrDefault() ?? "",
                ShortestMemo = memos.OrderBy(m => m.Length).FirstOrDefault() ?? ""
            };
        }
    }

    /// <summary>
    /// 메모 통계 정보를 나타내는 클래스
    /// </summary>
    public class MemoStatistics
    {
        /// <summary>
        /// 총 메모 개수
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 총 문자 수
        /// </summary>
        public int TotalCharacters { get; set; }

        /// <summary>
        /// 평균 길이
        /// </summary>
        public double AverageLength { get; set; }

        /// <summary>
        /// 가장 긴 메모
        /// </summary>
        public string LongestMemo { get; set; } = string.Empty;

        /// <summary>
        /// 가장 짧은 메모
        /// </summary>
        public string ShortestMemo { get; set; } = string.Empty;
    }
}
