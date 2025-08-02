using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Utils
{
    /// <summary>
    /// 문자열 및 데이터 포맷팅 유틸리티 클래스
    /// </summary>
    public static class FormatHelper
    {
        /// <summary>
        /// 한국 원화 형식으로 숫자를 포맷팅합니다
        /// </summary>
        /// <param name="amount">금액</param>
        /// <param name="includeSymbol">원화 기호 포함 여부</param>
        /// <returns>포맷된 금액 문자열</returns>
        public static string FormatCurrency(decimal amount, bool includeSymbol = true)
        {
            var formatted = amount.ToString("#,##0", CultureInfo.InvariantCulture);
            return includeSymbol ? $"{formatted}원" : formatted;
        }

        /// <summary>
        /// 숫자를 3자리마다 쉼표로 구분하여 포맷팅합니다
        /// </summary>
        /// <param name="number">숫자</param>
        /// <returns>포맷된 숫자 문자열</returns>
        public static string FormatNumber(long number)
        {
            return number.ToString("#,##0", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 소수점 숫자를 지정된 자릿수로 포맷팅합니다
        /// </summary>
        /// <param name="number">숫자</param>
        /// <param name="decimalPlaces">소수점 자릿수</param>
        /// <returns>포맷된 숫자 문자열</returns>
        public static string FormatDecimal(double number, int decimalPlaces = 2)
        {
            var format = $"#,##0.{new string('0', decimalPlaces)}";
            return number.ToString(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 백분율로 포맷팅합니다
        /// </summary>
        /// <param name="value">값 (0.0 ~ 1.0)</param>
        /// <param name="decimalPlaces">소수점 자릿수</param>
        /// <returns>백분율 문자열</returns>
        public static string FormatPercentage(double value, int decimalPlaces = 1)
        {
            var percentage = value * 100;
            var format = $"F{decimalPlaces}";
            return $"{percentage.ToString(format, CultureInfo.InvariantCulture)}%";
        }

        /// <summary>
        /// 파일 크기를 사람이 읽기 쉬운 형태로 포맷팅합니다
        /// </summary>
        /// <param name="bytes">바이트 수</param>
        /// <returns>포맷된 파일 크기</returns>
        public static string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            
            return $"{len:F2} {sizes[order]}";
        }

        /// <summary>
        /// 전화번호를 포맷팅합니다
        /// </summary>
        /// <param name="phoneNumber">전화번호</param>
        /// <returns>포맷된 전화번호</returns>
        public static string FormatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return string.Empty;

            // 숫자만 추출
            var digitsOnly = Regex.Replace(phoneNumber, @"[^\d]", "");
            
            return digitsOnly.Length switch
            {
                11 when digitsOnly.StartsWith("010") => $"{digitsOnly[..3]}-{digitsOnly[3..7]}-{digitsOnly[7..]}",
                10 when digitsOnly.StartsWith("02") => $"{digitsOnly[..2]}-{digitsOnly[2..6]}-{digitsOnly[6..]}",
                10 => $"{digitsOnly[..3]}-{digitsOnly[3..6]}-{digitsOnly[6..]}",
                9 => $"{digitsOnly[..2]}-{digitsOnly[2..5]}-{digitsOnly[5..]}",
                8 => $"{digitsOnly[..4]}-{digitsOnly[4..]}",
                _ => phoneNumber
            };
        }

        /// <summary>
        /// 주민등록번호를 마스킹하여 포맷팅합니다
        /// </summary>
        /// <param name="ssn">주민등록번호</param>
        /// <returns>마스킹된 주민등록번호</returns>
        public static string FormatSocialSecurityNumber(string ssn)
        {
            if (string.IsNullOrWhiteSpace(ssn))
                return string.Empty;

            var digitsOnly = Regex.Replace(ssn, @"[^\d]", "");
            
            if (digitsOnly.Length == 13)
            {
                return $"{digitsOnly[..6]}-{digitsOnly[6]}******";
            }
            
            return "잘못된 형식";
        }

        /// <summary>
        /// 문자열을 제한된 길이로 자르고 말줄임표를 추가합니다
        /// </summary>
        /// <param name="text">원본 텍스트</param>
        /// <param name="maxLength">최대 길이</param>
        /// <param name="ellipsis">말줄임표</param>
        /// <returns>잘린 텍스트</returns>
        public static string TruncateText(string text, int maxLength, string ellipsis = "...")
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text ?? string.Empty;

            return text[..(maxLength - ellipsis.Length)] + ellipsis;
        }

        /// <summary>
        /// 문자열의 첫 글자를 대문자로 변환합니다
        /// </summary>
        /// <param name="text">텍스트</param>
        /// <returns>첫 글자가 대문자인 텍스트</returns>
        public static string Capitalize(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (text.Length == 1)
                return text.ToUpper();

            return char.ToUpper(text[0]) + text[1..];
        }

        /// <summary>
        /// 카멜케이스를 스네이크케이스로 변환합니다
        /// </summary>
        /// <param name="camelCase">카멜케이스 문자열</param>
        /// <returns>스네이크케이스 문자열</returns>
        public static string CamelToSnakeCase(string camelCase)
        {
            if (string.IsNullOrEmpty(camelCase))
                return string.Empty;

            var result = new StringBuilder();
            result.Append(char.ToLower(camelCase[0]));

            for (int i = 1; i < camelCase.Length; i++)
            {
                if (char.IsUpper(camelCase[i]))
                {
                    result.Append('_');
                    result.Append(char.ToLower(camelCase[i]));
                }
                else
                {
                    result.Append(camelCase[i]);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// 스네이크케이스를 카멜케이스로 변환합니다
        /// </summary>
        /// <param name="snakeCase">스네이크케이스 문자열</param>
        /// <returns>카멜케이스 문자열</returns>
        public static string SnakeToCamelCase(string snakeCase)
        {
            if (string.IsNullOrEmpty(snakeCase))
                return string.Empty;

            var parts = snakeCase.Split('_');
            var result = new StringBuilder(parts[0].ToLower());

            for (int i = 1; i < parts.Length; i++)
            {
                if (!string.IsNullOrEmpty(parts[i]))
                {
                    result.Append(Capitalize(parts[i].ToLower()));
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// HTML 태그를 제거합니다
        /// </summary>
        /// <param name="html">HTML 텍스트</param>
        /// <returns>태그가 제거된 텍스트</returns>
        public static string RemoveHtmlTags(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return Regex.Replace(html, "<.*?>", string.Empty);
        }

        /// <summary>
        /// 이메일 주소를 마스킹합니다
        /// </summary>
        /// <param name="email">이메일 주소</param>
        /// <returns>마스킹된 이메일 주소</returns>
        public static string MaskEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                return email ?? string.Empty;

            var parts = email.Split('@');
            var localPart = parts[0];
            var domainPart = parts[1];

            if (localPart.Length <= 2)
                return email;

            var maskedLocal = localPart[0] + new string('*', localPart.Length - 2) + localPart[^1];
            return $"{maskedLocal}@{domainPart}";
        }

        /// <summary>
        /// 문자열이 유효한 이메일 형식인지 확인합니다
        /// </summary>
        /// <param name="email">이메일 주소</param>
        /// <returns>유효한 이메일 여부</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        /// <summary>
        /// 한국어 초성을 추출합니다
        /// </summary>
        /// <param name="korean">한국어 텍스트</param>
        /// <returns>초성 문자열</returns>
        public static string ExtractInitials(string korean)
        {
            if (string.IsNullOrEmpty(korean))
                return string.Empty;

            var initials = new StringBuilder();
            var initialChars = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";

            foreach (char c in korean)
            {
                if (c >= '가' && c <= '힣')
                {
                    int index = (c - '가') / (21 * 28);
                    initials.Append(initialChars[index]);
                }
                else if (char.IsLetter(c))
                {
                    initials.Append(c);
                }
            }

            return initials.ToString();
        }

        /// <summary>
        /// 문자열을 Base64로 인코딩합니다
        /// </summary>
        /// <param name="text">원본 텍스트</param>
        /// <returns>Base64 인코딩된 문자열</returns>
        public static string ToBase64(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64 문자열을 디코딩합니다
        /// </summary>
        /// <param name="base64">Base64 문자열</param>
        /// <returns>디코딩된 텍스트</returns>
        public static string FromBase64(string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return string.Empty;

            try
            {
                var bytes = Convert.FromBase64String(base64);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 문자열 배열을 구분자로 연결합니다 (null 및 빈 문자열 제외)
        /// </summary>
        /// <param name="values">문자열 배열</param>
        /// <param name="separator">구분자</param>
        /// <returns>연결된 문자열</returns>
        public static string JoinNonEmpty(string[] values, string separator = ", ")
        {
            return string.Join(separator, values.Where(v => !string.IsNullOrWhiteSpace(v)));
        }

        /// <summary>
        /// 숫자를 한국어 단위로 변환합니다
        /// </summary>
        /// <param name="number">숫자</param>
        /// <returns>한국어 단위 문자열</returns>
        public static string ToKoreanUnit(long number)
        {
            if (number == 0) return "0";

            var units = new[] { "", "만", "억", "조" };
            var result = new StringBuilder();
            int unitIndex = 0;

            while (number > 0 && unitIndex < units.Length)
            {
                var chunk = number % 10000;
                if (chunk > 0)
                {
                    var chunkStr = chunk.ToString();
                    if (result.Length > 0)
                    {
                        result.Insert(0, units[unitIndex]);
                    }
                    result.Insert(0, chunkStr);
                }
                number /= 10000;
                unitIndex++;
            }

            return result.ToString();
        }
    }
}
