using System.Globalization;

namespace App.Services
{
    /// <summary>
    /// 시간 관련 서비스를 제공하는 클래스
    /// </summary>
    public class TimeService
    {
        private readonly TimeZoneInfo _koreaTimeZone;

        /// <summary>
        /// TimeService 생성자
        /// </summary>
        public TimeService()
        {
            // 한국 시간대 설정
            try
            {
                _koreaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Seoul");
            }
            catch
            {
                // Fallback for different OS
                _koreaTimeZone = TimeZoneInfo.CreateCustomTimeZone(
                    "KST", 
                    TimeSpan.FromHours(9), 
                    "Korea Standard Time", 
                    "KST");
            }
        }

        /// <summary>
        /// 현재 한국 시간을 가져옵니다
        /// </summary>
        /// <returns>한국 현재 시간</returns>
        public DateTime GetKoreaTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _koreaTimeZone);
        }

        /// <summary>
        /// 현재 시간을 지정된 형식으로 포맷팅합니다
        /// </summary>
        /// <param name="format">시간 형식 (기본값: "yyyy-MM-dd HH:mm:ss")</param>
        /// <returns>포맷된 시간 문자열</returns>
        public string GetFormattedKoreaTime(string format = "yyyy-MM-dd HH:mm:ss")
        {
            return GetKoreaTime().ToString(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 메모용 타임스탬프를 생성합니다
        /// </summary>
        /// <returns>메모용 타임스탬프</returns>
        public string GetMemoTimestamp()
        {
            return GetFormattedKoreaTime("MM/dd HH:mm");
        }

        /// <summary>
        /// 상세 타임스탬프를 생성합니다
        /// </summary>
        /// <returns>상세 타임스탬프</returns>
        public string GetDetailedTimestamp()
        {
            return GetFormattedKoreaTime("yyyy년 MM월 dd일 HH시 mm분 ss초");
        }

        /// <summary>
        /// 요일을 포함한 날짜를 가져옵니다
        /// </summary>
        /// <returns>요일 포함 날짜</returns>
        public string GetDateWithDayOfWeek()
        {
            var now = GetKoreaTime();
            var dayOfWeek = GetKoreanDayOfWeek(now.DayOfWeek);
            return $"{now:yyyy년 MM월 dd일} ({dayOfWeek})";
        }

        /// <summary>
        /// 영어 요일을 한국어로 변환합니다
        /// </summary>
        /// <param name="dayOfWeek">영어 요일</param>
        /// <returns>한국어 요일</returns>
        private static string GetKoreanDayOfWeek(DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Sunday => "일",
                DayOfWeek.Monday => "월",
                DayOfWeek.Tuesday => "화",
                DayOfWeek.Wednesday => "수",
                DayOfWeek.Thursday => "목",
                DayOfWeek.Friday => "금",
                DayOfWeek.Saturday => "토",
                _ => "?"
            };
        }

        /// <summary>
        /// 두 시간 간의 차이를 계산합니다
        /// </summary>
        /// <param name="fromTime">시작 시간</param>
        /// <param name="toTime">종료 시간 (기본값: 현재 시간)</param>
        /// <returns>시간 차이</returns>
        public TimeSpan GetTimeDifference(DateTime fromTime, DateTime? toTime = null)
        {
            var endTime = toTime ?? GetKoreaTime();
            return endTime - fromTime;
        }

        /// <summary>
        /// 시간 차이를 사람이 읽기 쉬운 형태로 변환합니다
        /// </summary>
        /// <param name="timeSpan">시간 차이</param>
        /// <returns>읽기 쉬운 시간 차이</returns>
        public string GetHumanReadableTimeDifference(TimeSpan timeSpan)
        {
            if (timeSpan.TotalDays >= 1)
            {
                return $"{(int)timeSpan.TotalDays}일 전";
            }
            else if (timeSpan.TotalHours >= 1)
            {
                return $"{(int)timeSpan.TotalHours}시간 전";
            }
            else if (timeSpan.TotalMinutes >= 1)
            {
                return $"{(int)timeSpan.TotalMinutes}분 전";
            }
            else
            {
                return "방금 전";
            }
        }

        /// <summary>
        /// 업무 시간 여부를 확인합니다
        /// </summary>
        /// <param name="workStartHour">업무 시작 시간 (기본값: 9시)</param>
        /// <param name="workEndHour">업무 종료 시간 (기본값: 18시)</param>
        /// <returns>업무 시간 여부</returns>
        public bool IsWorkingHours(int workStartHour = 9, int workEndHour = 18)
        {
            var now = GetKoreaTime();
            var isWeekday = now.DayOfWeek != DayOfWeek.Saturday && now.DayOfWeek != DayOfWeek.Sunday;
            var isWorkingHour = now.Hour >= workStartHour && now.Hour < workEndHour;
            
            return isWeekday && isWorkingHour;
        }

        /// <summary>
        /// 다음 업무일을 계산합니다
        /// </summary>
        /// <returns>다음 업무일</returns>
        public DateTime GetNextWorkingDay()
        {
            var date = GetKoreaTime().Date.AddDays(1);
            
            // 주말이면 다음 월요일로
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            
            return date;
        }

        /// <summary>
        /// 시간 기반 인사말을 가져옵니다
        /// </summary>
        /// <returns>시간대별 인사말</returns>
        public string GetGreeting()
        {
            var hour = GetKoreaTime().Hour;
            
            return hour switch
            {
                >= 5 and < 12 => "좋은 아침입니다! ☀️",
                >= 12 and < 18 => "좋은 오후입니다! 🌤️",
                >= 18 and < 22 => "좋은 저녁입니다! 🌅",
                _ => "안녕하세요! 🌙"
            };
        }
    }
}
