using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    /// <summary>
    /// 메모 아이템을 나타내는 모델 클래스
    /// </summary>
    public class MemoItem
    {
        /// <summary>
        /// 메모의 고유 식별자
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 메모 제목
        /// </summary>
        [Required(ErrorMessage = "메모 제목은 필수입니다.")]
        [StringLength(200, ErrorMessage = "제목은 200자를 초과할 수 없습니다.")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 메모 내용
        /// </summary>
        [Required(ErrorMessage = "메모 내용은 필수입니다.")]
        [StringLength(10000, ErrorMessage = "내용은 10,000자를 초과할 수 없습니다.")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 메모 생성 일시
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 메모 수정 일시
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 메모 카테고리 (선택사항)
        /// </summary>
        [StringLength(50, ErrorMessage = "카테고리는 50자를 초과할 수 없습니다.")]
        public string? Category { get; set; }

        /// <summary>
        /// 메모 중요도 (1-5, 기본값 3)
        /// </summary>
        [Range(1, 5, ErrorMessage = "중요도는 1부터 5까지의 값이어야 합니다.")]
        public int Priority { get; set; } = 3;

        /// <summary>
        /// 메모가 즐겨찾기에 추가되었는지 여부
        /// </summary>
        public bool IsFavorite { get; set; } = false;

        /// <summary>
        /// 메모 태그들 (쉼표로 구분)
        /// </summary>
        [StringLength(500, ErrorMessage = "태그는 500자를 초과할 수 없습니다.")]
        public string? Tags { get; set; }

        /// <summary>
        /// 메모 생성자
        /// </summary>
        public MemoItem()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// 메모 생성자 (제목과 내용 포함)
        /// </summary>
        /// <param name="title">메모 제목</param>
        /// <param name="content">메모 내용</param>
        public MemoItem(string title, string content) : this()
        {
            Title = title ?? string.Empty;
            Content = content ?? string.Empty;
        }

        /// <summary>
        /// 메모의 짧은 미리보기 텍스트 반환
        /// </summary>
        /// <param name="maxLength">최대 길이 (기본값: 100)</param>
        /// <returns>미리보기 텍스트</returns>
        public string GetPreview(int maxLength = 100)
        {
            if (string.IsNullOrWhiteSpace(Content))
                return "내용 없음";

            if (Content.Length <= maxLength)
                return Content;

            return Content.Substring(0, maxLength) + "...";
        }

        /// <summary>
        /// 메모의 태그 배열 반환
        /// </summary>
        /// <returns>태그 배열</returns>
        public string[] GetTagsArray()
        {
            if (string.IsNullOrWhiteSpace(Tags))
                return Array.Empty<string>();

            return Tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(tag => tag.Trim())
                      .Where(tag => !string.IsNullOrWhiteSpace(tag))
                      .ToArray();
        }

        /// <summary>
        /// 메모 업데이트 시간 갱신
        /// </summary>
        public void Touch()
        {
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// 메모 정보를 문자열로 반환
        /// </summary>
        /// <returns>메모 정보 문자열</returns>
        public override string ToString()
        {
            return $"[{CreatedAt:yyyy-MM-dd HH:mm}] {Title}: {GetPreview(50)}";
        }
    }
}
