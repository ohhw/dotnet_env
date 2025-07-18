// ASP.NET Core 웹 애플리케이션 빌더 생성
// args는 명령줄 인수를 받아서 설정에 사용
var builder = WebApplication.CreateBuilder(args);

// 웹 호스트 설정: 모든 IP 주소(0.0.0.0)에서 5000 포트로 접근 가능하도록 설정
// 0.0.0.0은 모든 네트워크 인터페이스를 의미 (localhost, 실제 IP 모두 접근 가능)
builder.WebHost.UseUrls("http://0.0.0.0:5005");

// 설정된 빌더로 웹 애플리케이션 인스턴스 생성
var app = builder.Build();

// 개발 환경에서만 개발자 예외 페이지 사용
// 이 미들웨어는 개발 중 오류 발생 시 자세한 정보를 표시해줍니다
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// 메모 저장을 위한 리스트 (메모리에 저장)
var memos = new List<string>();

// 기본 경로 - HTML 페이지 (메모 목록 포함)
app.MapGet("/", () => 
{
    // 메모 목록 HTML 생성
    var memoListHtml = "";
    if (memos.Count > 0)
    {
        memoListHtml = string.Join("", memos.Select((memo, index) => 
            $"<div class='memo-item'><strong>#{index + 1}</strong> {memo}</div>"));
    }
    else
    {
        memoListHtml = "<div class='no-memo'>📝 아직 등록된 메모가 없습니다.</div>";
    }

    return Results.Content($@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>오현우님의 .NET 테스트 페이지</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 50px; background: #f5f6fa; color: #222; }}
        .container {{ max-width: 800px; margin: 0 auto; }}
        .time-display {{ background: #f0f0f0; padding: 20px; border-radius: 10px; margin: 20px 0; color: #222; }}
        button {{ background: #007bff; color: #fff; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer; margin: 5px; font-weight: bold; }}
        button:hover {{ background: #0056b3; }}
        .memo-section {{ background: #fffbe6; padding: 15px; border-radius: 8px; margin: 20px 0; border: 1px solid #ffeaa7; color: #333; }}
        .memo-link {{ background: #17a2b8; color: #fff !important; }}
        .memo-link:hover {{ background: #138496; }}
        .add-btn {{ background: #28a745; color: #fff; }}
        .add-btn:hover {{ background: #218838; }}
        .delete-btn {{ background: #dc3545; color: #fff; }}
        .delete-btn:hover {{ background: #c82333; }}
        a {{ text-decoration: none; color: #007bff; display: inline-block; padding: 10px 20px; border-radius: 5px; margin: 5px; font-weight: bold; background: #e9ecef; transition: background 0.2s, color 0.2s; }}
        a:hover {{ background: #d6d8db; color: #0056b3; }}
        .memo-list {{ background: #f8f9fa; padding: 20px; border-radius: 10px; margin: 20px 0; border: 1px solid #dee2e6; color: #222; }}
        .memo-item {{ background: #ffffff; padding: 12px; border-radius: 6px; border: 1px solid #e9ecef; margin: 8px 0; color: #222; }}
        .no-memo {{ background: #e2e3e5; color: #6c757d; padding: 20px; border-radius: 8px; text-align: center; border: 1px solid #d6d8db; }}
        .memo-header {{ display: flex; justify-content: space-between; align-items: center; margin-bottom: 15px; }}
        .other-links {{ background: #e9ecef; padding: 15px; border-radius: 8px; margin: 20px 0; color: #222; }}
        h1, h2, h3 {{ color: #222; }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>Hello, World! 오현우님!</h1>
        <div class='time-display'>
            <h2>현재 시간: {DateTime.Now:yyyy-MM-dd HH:mm:ss}</h2>
        </div>
        <button onclick='location.reload()'>시간 새로고침</button>
        
        <div class='memo-section'>
            <div class='memo-header'>
                <h3>📝 메모장 ({memos.Count}개)</h3>
                <div>
                    <button class='add-btn' onclick='addNewMemo()'>✏️ 새 메모 추가</button>
                    {(memos.Count > 0 ? @"<button class='delete-btn' onclick='confirmDelete()'>🗑️ 전체 삭제</button>" : "")}
                </div>
            </div>
            
            <div class='memo-list'>
                {memoListHtml}
            </div>
            
            <div style='text-align: center; margin-top: 15px;'>
                <a href='/viewMemo' class='memo-link' style='background:#17a2b8;color:#fff;'>📋 전체 메모 페이지로 이동</a>
            </div>
        </div>
        
        <div class='other-links'>
            <h3>🔗 기타 기능</h3>
            <a href='/api/time' style='background:#343a40;color:#fff;'>JSON 형식으로 시간 보기</a>
            <a href='/helloworld' style='background:#6c757d;color:#fff;'>Hello World 페이지</a>
        </div>
    </div>
    
    <script>
        function addNewMemo() {{
            var message = prompt('새 메모를 입력하세요:');
            if (message && message.trim()) {{
                window.location.href = '/addMemo?Message=' + encodeURIComponent(message.trim());
            }}
        }}
        
        function confirmDelete() {{
            if (confirm('정말로 모든 메모를 삭제하시겠습니까?')) {{
                window.location.href = '/deleteALL';
            }}
        }}
    </script>
</body>
</html>", "text/html");
});

// 메모 추가 기능 - /addMemo?Message=내용
app.MapGet("/addMemo", (string? Message) => 
{
    if (string.IsNullOrEmpty(Message))
    {
        return Results.Content(@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>메모 추가 오류</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 50px; }
        .container { max-width: 600px; margin: 0 auto; }
        .error { background: #f8d7da; color: #721c24; padding: 15px; border-radius: 8px; border: 1px solid #f5c6cb; }
        button { background: #007bff; color: white; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer; margin: 5px; }
        button:hover { background: #0056b3; }
    </style>
</head>
<body>
    <div class='container'>
        <h1>❌ 메모 추가 실패</h1>
        <div class='error'>
            <p>메시지가 비어있습니다!</p>
            <p>사용법: /addMemo?Message=여기에메모내용</p>
        </div>
        <button onclick='window.location.href=""/"";'>홈으로 돌아가기</button>
        <button onclick='window.location.href=""/viewMemo"";'>메모 보기</button>
    </div>
</body>
</html>", "text/html");
    }
    
    // 메모 추가
    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    memos.Add($"[{timestamp}] {Message}");
    
    return Results.Content($@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>메모 추가 완료</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 50px; }}
        .container {{ max-width: 600px; margin: 0 auto; }}
        .success {{ background: #d4edda; color: #155724; padding: 15px; border-radius: 8px; border: 1px solid #c3e6cb; margin: 20px 0; }}
        .memo-content {{ background: #f8f9fa; padding: 15px; border-radius: 8px; border: 1px solid #dee2e6; margin: 10px 0; }}
        button {{ background: #007bff; color: white; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer; margin: 5px; }}
        button:hover {{ background: #0056b3; }}
        .view-btn {{ background: #28a745; }}
        .view-btn:hover {{ background: #218838; }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>✅ 메모 추가 완료!</h1>
        <div class='success'>
            <p>메모가 성공적으로 추가되었습니다!</p>
        </div>
        <div class='memo-content'>
            <h3>추가된 메모:</h3>
            <p><strong>[{timestamp}]</strong> {Message}</p>
        </div>
        <p><strong>총 메모 개수:</strong> {memos.Count}개</p>
        <button onclick='window.location.href=""/"";'>홈으로 돌아가기</button>
        <button class='view-btn' onclick='window.location.href=""/viewMemo"";'>모든 메모 보기</button>
    </div>
</body>
</html>", "text/html");
});

// 메모 보기 기능 - /viewMemo
app.MapGet("/viewMemo", () => 
{
    var memoList = string.Join("", memos.Select((memo, index) => 
        $"<div class='memo-item'><strong>#{index + 1}</strong> {memo}</div>"));
    
    if (memos.Count == 0)
    {
        memoList = "<div class='no-memo'>📝 등록된 메모가 없습니다.</div>";
    }
    
    return Results.Content($@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>메모 목록</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 50px; }}
        .container {{ max-width: 800px; margin: 0 auto; }}
        .memo-item {{ background: #f8f9fa; padding: 15px; border-radius: 8px; border: 1px solid #dee2e6; margin: 10px 0; }}
        .no-memo {{ background: #fff3cd; color: #856404; padding: 20px; border-radius: 8px; text-align: center; border: 1px solid #ffeaa7; }}
        button {{ background: #007bff; color: white; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer; margin: 5px; }}
        button:hover {{ background: #0056b3; }}
        .delete-btn {{ background: #dc3545; }}
        .delete-btn:hover {{ background: #c82333; }}
        .add-btn {{ background: #28a745; }}
        .add-btn:hover {{ background: #218838; }}
        .header {{ background: #e9ecef; padding: 20px; border-radius: 10px; margin: 20px 0; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>📋 메모 목록</h1>
            <p><strong>총 메모 개수:</strong> {memos.Count}개</p>
            <p><strong>조회 시간:</strong> {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>
        </div>
        
        {memoList}
        
        <div style='margin-top: 30px;'>
            <button onclick='window.location.href=""/"";'>🏠 홈으로</button>
            <button class='add-btn' onclick='addNewMemo()'>✏️ 새 메모 추가</button>
            {(memos.Count > 0 ? @"<button class='delete-btn' onclick='confirmDelete()'>🗑️ 모든 메모 삭제</button>" : "")}
            <button onclick='location.reload()'>🔄 새로고침</button>
        </div>
    </div>
    
    <script>
        function addNewMemo() {{
            var message = prompt('새 메모를 입력하세요:');
            if (message && message.trim()) {{
                window.location.href = '/addMemo?Message=' + encodeURIComponent(message.trim());
            }}
        }}
        
        function confirmDelete() {{
            if (confirm('정말로 모든 메모를 삭제하시겠습니까?')) {{
                window.location.href = '/deleteALL';
            }}
        }}
    </script>
</body>
</html>", "text/html");
});

// 모든 메모 삭제 기능 - /deleteALL
app.MapGet("/deleteALL", () => 
{
    var deletedCount = memos.Count;
    memos.Clear();
    
    return Results.Content($@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>메모 삭제 완료</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 50px; }}
        .container {{ max-width: 600px; margin: 0 auto; }}
        .warning {{ background: #f8d7da; color: #721c24; padding: 20px; border-radius: 8px; border: 1px solid #f5c6cb; margin: 20px 0; text-align: center; }}
        .success {{ background: #d4edda; color: #155724; padding: 15px; border-radius: 8px; border: 1px solid #c3e6cb; margin: 20px 0; }}
        button {{ background: #007bff; color: white; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer; margin: 5px; }}
        button:hover {{ background: #0056b3; }}
        .add-btn {{ background: #28a745; }}
        .add-btn:hover {{ background: #218838; }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>🗑️ 메모 삭제 완료</h1>
        <div class='warning'>
            <h2>⚠️ 모든 메모가 삭제되었습니다!</h2>
            <p><strong>삭제된 메모 개수:</strong> {deletedCount}개</p>
        </div>
        <div class='success'>
            <p>삭제 완료 시간: {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>
        </div>
        <button onclick='window.location.href=""/"";'>🏠 홈으로</button>
        <button onclick='window.location.href=""/viewMemo"";'>📋 메모 목록 (빈 목록)</button>
        <button class='add-btn' onclick='addNewMemo()'>✏️ 새 메모 추가</button>
    </div>
    
    <script>
        function addNewMemo() {{
            var message = prompt('새 메모를 입력하세요:');
            if (message && message.trim()) {{
                window.location.href = '/addMemo?Message=' + encodeURIComponent(message.trim());
            }}
        }}
    </script>
</body>
</html>", "text/html");
});

// Hello World 경로
app.MapGet("/helloworld", (string? name) => 
{
    string userName = string.IsNullOrEmpty(name) ? "오현우님" : name + "님";
    return $"Hello, World! {userName}!";
});

// 시간 확인 경로
app.MapGet("/time", (string? name) => 
{
    string userName = string.IsNullOrEmpty(name) ? "오현우님" : name + "님";
    return $"지금 시간을 확인하세요! {userName}! " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
});

// JSON API 경로
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