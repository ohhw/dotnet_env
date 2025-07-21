using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace MyConsoleApp.Pages.Time
{
    public class IndexModel : PageModel
    {
        public string Format { get; set; } = "";
        public string JsonResponse { get; set; } = "";

        public void OnGet(string format = "")
        {
            Format = format;
            
            if (format == "json")
            {
                var timeData = new
                {
                    current_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    date = DateTime.Now.ToString("yyyy-MM-dd"),
                    time = DateTime.Now.ToString("HH:mm:ss"),
                    day_of_week = DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("ko-KR")),
                    year = DateTime.Now.Year,
                    month = DateTime.Now.Month,
                    day = DateTime.Now.Day,
                    hour = DateTime.Now.Hour,
                    minute = DateTime.Now.Minute,
                    second = DateTime.Now.Second,
                    millisecond = DateTime.Now.Millisecond,
                    utc_time = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                    timezone = "KST (GMT+9)",
                    unix_timestamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds(),
                    iso_8601 = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK")
                };
                
                JsonResponse = JsonSerializer.Serialize(timeData, new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            }
        }
    }
}
