// ASP.NET Core 웹 애플리케이션의 메인 진입점
// 이 파일은 애플리케이션의 구성과 시작을 담당합니다.

// 웹 애플리케이션 빌더 생성
var builder = WebApplication.CreateBuilder(args);

// 서버 URL 설정 - 포트 5005 단일 HTTP 포트로 설정 (모든 네트워크 인터페이스에서 접근 가능)
builder.WebHost.UseUrls("http://0.0.0.0:5005");

// 서비스 컨테이너에 필요한 서비스 추가
builder.Services.AddRazorPages(); // Razor Pages 지원 추가

// Antiforgery 옵션 설정 (CSRF 보호)
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

// 웹 애플리케이션 인스턴스 생성
var app = builder.Build();

// HTTP 요청 파이프라인 구성
if (!app.Environment.IsDevelopment())
{
    // 운영 환경에서의 예외 처리
    app.UseExceptionHandler("/Error");
}

// 정적 파일 제공 (CSS, JS, 이미지 등)
app.UseStaticFiles();

// 라우팅 미들웨어 사용
app.UseRouting();

// Razor Pages 매핑 추가 - 이제 모든 페이지가 Razor Pages로 작동
app.MapRazorPages();

// 웹 애플리케이션 실행 시작
// 이 메소드는 블로킹되어 서버가 종료될 때까지 계속 실행됨
// Ctrl+C로 종료 가능
app.Run();
