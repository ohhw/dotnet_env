namespace App
{
    /// <summary>
    /// 애플리케이션 전역에서 사용되는 공유 데이터를 관리하는 클래스
    /// </summary>
    public static class SharedData
    {
        /// <summary>
        /// 사용자 메모 리스트
        /// </summary>
        public static List<string> Memos { get; set; } = new List<string>();

        /// <summary>
        /// 메모 개수 반환
        /// </summary>
        public static int MemoCount => Memos.Count;

        /// <summary>
        /// 새 메모 추가
        /// </summary>
        /// <param name="message">메모 내용</param>
        public static void AddMemo(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                Memos.Add($"[{timestamp}] {message.Trim()}");
            }
        }

        /// <summary>
        /// 모든 메모 삭제
        /// </summary>
        public static void ClearAllMemos()
        {
            Memos.Clear();
        }

        /// <summary>
        /// 특정 인덱스의 메모 삭제
        /// </summary>
        /// <param name="index">삭제할 메모의 인덱스</param>
        /// <returns>삭제 성공 여부</returns>
        public static bool RemoveMemoAt(int index)
        {
            if (index >= 0 && index < Memos.Count)
            {
                Memos.RemoveAt(index);
                return true;
            }
            return false;
        }
    }
}
