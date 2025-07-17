// ASP.NET Core 웹 애플리케이션 빌더 생성
// args는 명령줄 인수를 받아서 설정에 사용
var builder = WebApplication.CreateBuilder(args);

// 웹 호스트 설정: 모든 IP 주소(0.0.0.0)에서 5000 포트로 접근 가능하도록 설정
// 0.0.0.0은 모든 네트워크 인터페이스를 의미 (localhost, 실제 IP 모두 접근 가능)
builder.WebHost.UseUrls("http://0.0.0.0:5000");

// 설정된 빌더로 웹 애플리케이션 인스턴스 생성
var app = builder.Build();

// 개발 환경에서만 개발자 예외 페이지 사용
// 이 미들웨어는 개발 중 오류 발생 시 자세한 정보를 표시해줍니다
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// 기본 경로 - HTML 페이지로 변경
// HTTP GET 요청을 "/" 경로에 매핑하여 완전한 HTML 페이지 반환
// Results.Content()는 HTML 콘텐츠를 "text/html" MIME 타입으로 반환
app.MapGet("/", () => Results.Content(@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>오현우님의 .NET 테스트 페이지</title>
    <style>
        /* CSS 스타일 정의 */
        body { font-family: Arial, sans-serif; margin: 50px; }
        .container { max-width: 600px; margin: 0 auto; }
        .time-display { background: #f0f0f0; padding: 20px; border-radius: 10px; margin: 20px 0; }
        button { background: #007bff; color: white; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer; }
        button:hover { background: #0056b3; }
    </style>
</head>
<body>
    <div class='container'>
        <h1>Hello, World! 오현우님!</h1>
        <div class='time-display'>
            <!-- C# DateTime.Now 코드가 서버에서 실행되어 현재 시간을 HTML에 삽입 -->
            <h2>현재 시간: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"</h2>
        </div>
        <!-- JavaScript onclick 이벤트로 페이지 새로고침 기능 -->
        <button onclick='location.reload()'>시간 새로고침</button>
        <br><br>
        <!-- JSON API 링크 -->
        <a href='/api/time'>JSON 형식으로 시간 보기</a>
        <br><br>
        <a href='/helloworld'>Hello World 페이지</a>
    </div>
</body>
</html>
", "text/html"));

// Hello World 경로 - 기존 기본 경로를 /helloworld로 변경
// HTTP GET 요청을 "/helloworld" 경로에 매핑, name 파라미터를 선택적으로 받음
// 예: http://localhost:5000/helloworld 또는 http://localhost:5000/helloworld?name=철수
app.MapGet("/helloworld", (string? name) => 
{
    // name이 비어있거나 null이면 "오현우님", 아니면 입력받은 이름 + "님"
    string userName = string.IsNullOrEmpty(name) ? "오현우님" : name + "님";
    // 문자열 보간($)을 사용해 동적으로 인사말 생성
    return $"Hello, World! {userName}!";
});

// 시간 확인 경로 (쿼리 파라미터 지원)
// HTTP GET 요청을 "/time" 경로에 매핑, name 파라미터를 선택적으로 받음
// 예: http://localhost:5000/time?name=영희
app.MapGet("/time", (string? name) => 
{
    // name이 비어있거나 null이면 "오현우님", 아니면 입력받은 이름 + "님"
    string userName = string.IsNullOrEmpty(name) ? "오현우님" : name + "님";
    // 현재 시간을 "yyyy-MM-dd HH:mm:ss" 형식으로 포맷팅하여 메시지와 함께 반환
    return $"지금 시간을 확인하세요! {userName}! " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
});

// JSON API 경로를 HTML 페이지로 변경
app.MapGet("/api/time", () => Results.Content(@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>시간 확인 - JSON API</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 50px; }
        .container { max-width: 600px; margin: 0 auto; }
        .json-display { background: #f8f9fa; padding: 20px; border-radius: 10px; margin: 20px 0; border: 1px solid #dee2e6; }
        .time-info { background: #e9ecef; padding: 15px; border-radius: 8px; margin: 10px 0; }
        button { background: #007bff; color: white; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer; margin: 5px; }
        button:hover { background: #0056b3; }
        .back-btn { background: #28a745; }
        .back-btn:hover { background: #218838; }
        pre { background: #f1f3f4; padding: 15px; border-radius: 5px; overflow-x: auto; }
    </style>
</head>
<body>
    <div class='container'>
        <h1>JSON API 시간 정보</h1>
        
        <div class='json-display'>
            <h3>JSON 형식 데이터:</h3>
            <pre>{
  ""message"": ""지금 시간을 확인하세요!"",
  ""currentTime"": """ + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @""",
  ""timestamp"": " + DateTime.Now.Ticks + @"
}</pre>
        </div>
        
        <div class='time-info'>
            <p><strong>현재 시간:</strong> " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"</p>
            <p><strong>타임스탬프:</strong> " + DateTime.Now.Ticks + @"</p>
            <p><strong>요청 시간:</strong> " + DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초") + @"</p>
        </div>
        
        <button onclick='location.reload()'>새로고침</button>
        <button class='back-btn' onclick='window.location.href=""/"";'>홈으로</button>
    </div>
</body>
</html>
", "text/html"));

// 웹 애플리케이션 실행 시작
// 이 메소드는 블로킹되어 서버가 종료될 때까지 계속 실행됨
// Ctrl+C로 종료 가능
app.Run();