using System.Globalization;

namespace App.Utils
{
    /// <summary>
    /// 날짜 및 시간 관련 유틸리티 클래스
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// 한국 시간대
        /// </summary>
        public static readonly TimeZoneInfo KoreaTimeZone = GetKoreaTimeZone();

        /// <summary>
        /// 한국 시간대를 가져옵니다
        /// </summary>
        /// <returns>한국 시간대</returns>
        private static TimeZoneInfo GetKoreaTimeZone()
        {
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Asia/Seoul");
            }
            catch
            {
                // Fallback for different OS
                return TimeZoneInfo.CreateCustomTimeZone(
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
        public static DateTime GetKoreaTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, KoreaTimeZone);
        }

        /// <summary>
        /// UTC 시간을 한국 시간으로 변환합니다
        /// </summary>
        /// <param name="utcTime">UTC 시간</param>
        /// <returns>한국 시간</returns>
        public static DateTime ConvertToKoreaTime(DateTime utcTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, KoreaTimeZone);
        }

        /// <summary>
        /// 날짜를 다양한 형식으로 포맷팅합니다
        /// </summary>
        /// <param name="dateTime">포맷할 날짜</param>
        /// <param name="format">날짜 형식</param>
        /// <returns>포맷된 날짜 문자열</returns>
        public static string FormatDate(DateTime dateTime, DateFormat format = DateFormat.Standard)
        {
            return format switch
            {
                DateFormat.Standard => dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                DateFormat.Korean => dateTime.ToString("yyyy년 MM월 dd일", CultureInfo.InvariantCulture),
                DateFormat.Short => dateTime.ToString("MM/dd", CultureInfo.InvariantCulture),
                DateFormat.Long => dateTime.ToString("yyyy년 MM월 dd일 dddd", new CultureInfo("ko-KR")),
                DateFormat.ISO => dateTime.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture),
                DateFormat.Timestamp => dateTime.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture),
                _ => dateTime.ToString(CultureInfo.InvariantCulture)
            };
        }

        /// <summary>
        /// 시간을 다양한 형식으로 포맷팅합니다
        /// </summary>
        /// <param name="dateTime">포맷할 시간</param>
        /// <param name="format">시간 형식</param>
        /// <returns>포맷된 시간 문자열</returns>
        public static string FormatTime(DateTime dateTime, TimeFormat format = TimeFormat.Standard)
        {
            return format switch
            {
                TimeFormat.Standard => dateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture),
                TimeFormat.Short => dateTime.ToString("HH:mm", CultureInfo.InvariantCulture),
                TimeFormat.Korean => dateTime.ToString("HH시 mm분", CultureInfo.InvariantCulture),
                TimeFormat.WithSeconds => dateTime.ToString("HH시 mm분 ss초", CultureInfo.InvariantCulture),
                TimeFormat.AmPm => dateTime.ToString("tt h:mm", new CultureInfo("ko-KR")),
                _ => dateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
            };
        }

        /// <summary>
        /// 날짜와 시간을 함께 포맷팅합니다
        /// </summary>
        /// <param name="dateTime">포맷할 날짜시간</param>
        /// <param name="style">포맷 스타일</param>
        /// <returns>포맷된 날짜시간 문자열</returns>
        public static string FormatDateTime(DateTime dateTime, DateTimeStyle style = DateTimeStyle.Standard)
        {
            return style switch
            {
                DateTimeStyle.Standard => dateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                DateTimeStyle.Korean => dateTime.ToString("yyyy년 MM월 dd일 HH시 mm분", CultureInfo.InvariantCulture),
                DateTimeStyle.Short => dateTime.ToString("MM/dd HH:mm", CultureInfo.InvariantCulture),
                DateTimeStyle.Long => dateTime.ToString("yyyy년 MM월 dd일 dddd HH시 mm분 ss초", new CultureInfo("ko-KR")),
                DateTimeStyle.Friendly => GetFriendlyDateTime(dateTime),
                _ => dateTime.ToString(CultureInfo.InvariantCulture)
            };
        }

        /// <summary>
        /// 사용자 친화적인 날짜시간 문자열을 생성합니다
        /// </summary>
        /// <param name="dateTime">날짜시간</param>
        /// <returns>친화적인 날짜시간 문자열</returns>
        public static string GetFriendlyDateTime(DateTime dateTime)
        {
            var now = GetKoreaTime();
            var timeSpan = now - dateTime;

            if (timeSpan.TotalMinutes < 1)
                return "방금 전";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes}분 전";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours}시간 전";
            if (timeSpan.TotalDays < 7)
                return $"{(int)timeSpan.TotalDays}일 전";
            
            return FormatDate(dateTime, DateFormat.Korean);
        }

        /// <summary>
        /// 두 날짜 간의 차이를 계산합니다
        /// </summary>
        /// <param name="startDate">시작 날짜</param>
        /// <param name="endDate">종료 날짜</param>
        /// <returns>날짜 차이 정보</returns>
        public static DateDifference GetDateDifference(DateTime startDate, DateTime endDate)
        {
            var timeSpan = endDate - startDate;
            var totalDays = (int)Math.Abs(timeSpan.TotalDays);
            
            return new DateDifference
            {
                TotalDays = totalDays,
                Years = totalDays / 365,
                Months = totalDays / 30,
                Weeks = totalDays / 7,
                TimeSpan = timeSpan,
                IsPositive = timeSpan.TotalMilliseconds >= 0
            };
        }

        /// <summary>
        /// 요일을 한국어로 가져옵니다
        /// </summary>
        /// <param name="dayOfWeek">요일</param>
        /// <returns>한국어 요일</returns>
        public static string GetKoreanDayOfWeek(DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Sunday => "일요일",
                DayOfWeek.Monday => "월요일",
                DayOfWeek.Tuesday => "화요일",
                DayOfWeek.Wednesday => "수요일",
                DayOfWeek.Thursday => "목요일",
                DayOfWeek.Friday => "금요일",
                DayOfWeek.Saturday => "토요일",
                _ => "알 수 없음"
            };
        }

        /// <summary>
        /// 요일을 한국어 줄임말로 가져옵니다
        /// </summary>
        /// <param name="dayOfWeek">요일</param>
        /// <returns>한국어 요일 줄임말</returns>
        public static string GetKoreanDayOfWeekShort(DayOfWeek dayOfWeek)
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
        /// 업무일 여부를 확인합니다
        /// </summary>
        /// <param name="date">확인할 날짜</param>
        /// <returns>업무일 여부</returns>
        public static bool IsWorkingDay(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        /// <summary>
        /// 다음 업무일을 가져옵니다
        /// </summary>
        /// <param name="date">기준 날짜</param>
        /// <returns>다음 업무일</returns>
        public static DateTime GetNextWorkingDay(DateTime date)
        {
            var nextDay = date.AddDays(1);
            while (!IsWorkingDay(nextDay))
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }
    }

    /// <summary>
    /// 날짜 형식 열거형
    /// </summary>
    public enum DateFormat
    {
        Standard,   // yyyy-MM-dd
        Korean,     // yyyy년 MM월 dd일
        Short,      // MM/dd
        Long,       // yyyy년 MM월 dd일 dddd
        ISO,        // yyyy-MM-ddTHH:mm:ss
        Timestamp   // yyyyMMdd_HHmmss
    }

    /// <summary>
    /// 시간 형식 열거형
    /// </summary>
    public enum TimeFormat
    {
        Standard,    // HH:mm:ss
        Short,       // HH:mm
        Korean,      // HH시 mm분
        WithSeconds, // HH시 mm분 ss초
        AmPm         // 오전/오후 h:mm
    }

    /// <summary>
    /// 날짜시간 스타일 열거형
    /// </summary>
    public enum DateTimeStyle
    {
        Standard, // yyyy-MM-dd HH:mm:ss
        Korean,   // yyyy년 MM월 dd일 HH시 mm분
        Short,    // MM/dd HH:mm
        Long,     // yyyy년 MM월 dd일 dddd HH시 mm분 ss초
        Friendly  // 사용자 친화적 (방금 전, 1분 전 등)
    }

    /// <summary>
    /// 날짜 차이 정보를 나타내는 클래스
    /// </summary>
    public class DateDifference
    {
        public int TotalDays { get; set; }
        public int Years { get; set; }
        public int Months { get; set; }
        public int Weeks { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public bool IsPositive { get; set; }

        public override string ToString()
        {
            if (TotalDays == 0)
                return "같은 날";
            if (TotalDays < 7)
                return $"{TotalDays}일";
            if (TotalDays < 30)
                return $"{Weeks}주";
            if (TotalDays < 365)
                return $"{Months}개월";
            
            return $"{Years}년";
        }
    }
}
