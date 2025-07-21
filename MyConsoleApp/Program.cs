// ASP.NET Core 웹 애플리케이션 빌더 생성
// args는 명령줄 인수를 받아서 설정에 사용
var builder = WebApplication.CreateBuilder(args);

// 웹 호스트 설정: 모든 IP 주소(0.0.0.0)에서 5000 포트로 접근 가능하도록 설정
// 0.0.0.0은 모든 네트워크 인터페이스를 의미 (localhost, 실제 IP 모두 접근 가능)
builder.WebHost.UseUrls("http://0.0.0.0:5005");

// 설정된 빌더로 웹 애플리케이션 인스턴스 생성
var app = builder.Build();

// 개발 환경에서만 개발자 예외 페이지 사용
// 이 미들웨어는 개발 중 오류 발생 시 자세한 정보를 표시해줍니다3
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
        <div style='text-align: center; margin: 10px 0;'>
            <button onclick='location.reload()'>시간 새로고침</button>
            <button onclick='window.location.href=""/api/time""' style='background:#343a40;'>JSON 형식으로 시간 보기</button>
        </div>
        
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
            <a href='/helloworld' style='background:#6c757d;color:#fff;'>Hello World 페이지</a>
            <a href='/operator' style='background:#28a745;color:#fff;'>🧮 계산기</a>
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

// 계산기 기능 - /operator
app.MapGet("/operator", (double? op1, double? op2, string? opr) => 
{
    string resultHtml = "";
    string op1Value = op1?.ToString() ?? "";
    string op2Value = op2?.ToString() ?? "";
    string oprValue = opr ?? "add";
    
    if (op1.HasValue && !string.IsNullOrEmpty(opr) && 
        (op2.HasValue || new[] {"sqrt", "ln", "exp", "sin", "cos", "tan"}.Contains(opr.ToLower())))
    {
        double result = 0;
        string operation = "";
        string errorMessage = "";
        
        try
        {
            switch (opr.ToLower())
            {
                case "add":
                    if (!op2.HasValue) { errorMessage = "덧셈에는 두 번째 숫자가 필요합니다!"; break; }
                    result = op1.Value + op2.Value;
                    operation = $"{op1.Value} + {op2.Value}";
                    break;
                case "sub":
                    if (!op2.HasValue) { errorMessage = "뺄셈에는 두 번째 숫자가 필요합니다!"; break; }
                    result = op1.Value - op2.Value;
                    operation = $"{op1.Value} - {op2.Value}";
                    break;
                case "mul":
                    if (!op2.HasValue) { errorMessage = "곱셈에는 두 번째 숫자가 필요합니다!"; break; }
                    result = op1.Value * op2.Value;
                    operation = $"{op1.Value} × {op2.Value}";
                    break;
                case "div":
                    if (!op2.HasValue) { errorMessage = "나눗셈에는 두 번째 숫자가 필요합니다!"; break; }
                    if (op2.Value == 0)
                    {
                        errorMessage = "0으로 나눌 수 없습니다!";
                    }
                    else
                    {
                        result = op1.Value / op2.Value;
                        operation = $"{op1.Value} ÷ {op2.Value}";
                    }
                    break;
                case "pow":
                    if (!op2.HasValue) { errorMessage = "거듭제곱에는 두 번째 숫자(지수)가 필요합니다!"; break; }
                    result = Math.Pow(op1.Value, op2.Value);
                    operation = $"{op1.Value}^{op2.Value}";
                    break;
                case "mod":
                    if (!op2.HasValue) { errorMessage = "나머지 연산에는 두 번째 숫자가 필요합니다!"; break; }
                    if (op2.Value == 0)
                    {
                        errorMessage = "0으로 나머지를 구할 수 없습니다!";
                    }
                    else
                    {
                        result = op1.Value % op2.Value;
                        operation = $"{op1.Value} mod {op2.Value}";
                    }
                    break;
                case "log":
                    if (!op2.HasValue) { errorMessage = "로그 연산에는 두 번째 숫자(진수)가 필요합니다!"; break; }
                    if (op1.Value <= 0)
                    {
                        errorMessage = "로그의 밑은 0보다 커야 합니다!";
                    }
                    else if (op2.Value <= 0)
                    {
                        errorMessage = "로그의 진수는 0보다 커야 합니다!";
                    }
                    else
                    {
                        result = Math.Log(op2.Value) / Math.Log(op1.Value);
                        operation = $"log₍{op1.Value}₎({op2.Value})";
                    }
                    break;
                case "sqrt":
                    if (op1.Value < 0)
                    {
                        errorMessage = "음수의 제곱근은 계산할 수 없습니다!";
                    }
                    else
                    {
                        result = Math.Sqrt(op1.Value);
                        operation = $"√{op1.Value}";
                    }
                    break;
                case "sin":
                    result = Math.Sin(op1.Value * Math.PI / 180); // 도 단위
                    operation = $"sin({op1.Value}°)";
                    break;
                case "cos":
                    result = Math.Cos(op1.Value * Math.PI / 180); // 도 단위
                    operation = $"cos({op1.Value}°)";
                    break;
                case "tan":
                    result = Math.Tan(op1.Value * Math.PI / 180); // 도 단위
                    operation = $"tan({op1.Value}°)";
                    break;
                case "ln":
                    if (op1.Value <= 0)
                    {
                        errorMessage = "자연로그의 진수는 0보다 커야 합니다!";
                    }
                    else
                    {
                        result = Math.Log(op1.Value);
                        operation = $"ln({op1.Value})";
                    }
                    break;
                case "exp":
                    result = Math.Exp(op1.Value);
                    operation = $"e^{op1.Value}";
                    break;
                default:
                    errorMessage = "지원하지 않는 연산입니다!";
                    break;
            }
            
            if (string.IsNullOrEmpty(errorMessage))
            {
                resultHtml = $@"
                <div class='result-section success'>
                    <h3>✅ 계산 결과</h3>
                    <div class='calculation'>
                        <p><strong>계산식:</strong> {operation}</p>
                        <p><strong>결과:</strong> <span class='result-value'>{result}</span></p>
                        <p><small>계산 시간: {DateTime.Now:yyyy-MM-dd HH:mm:ss}</small></p>
                    </div>
                </div>";
            }
            else
            {
                resultHtml = $@"
                <div class='result-section error'>
                    <h3>❌ 계산 오류</h3>
                    <p>{errorMessage}</p>
                </div>";
            }
        }
        catch (Exception ex)
        {
            resultHtml = $@"
            <div class='result-section error'>
                <h3>❌ 계산 오류</h3>
                <p>계산 중 오류가 발생했습니다: {ex.Message}</p>
            </div>";
        }
    }
    
    return Results.Content($@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>고급 계산기 - 오현우님의 .NET 페이지</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 50px; background: #f5f6fa; }}
        .container {{ max-width: 900px; margin: 0 auto; }}
        .calculator-section {{ background: #ffffff; padding: 30px; border-radius: 15px; margin: 20px 0; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .input-group {{ margin: 15px 0; }}
        .input-group label {{ display: block; margin-bottom: 8px; font-weight: bold; color: #333; }}
        .input-group input, .input-group select {{ width: 100%; padding: 12px; border: 2px solid #ddd; border-radius: 8px; font-size: 16px; box-sizing: border-box; }}
        .input-group input:focus, .input-group select:focus {{ border-color: #007bff; outline: none; }}
        .button-group {{ text-align: center; margin: 25px 0; }}
        button {{ background: #007bff; color: white; padding: 12px 25px; border: none; border-radius: 8px; cursor: pointer; margin: 5px; font-size: 16px; font-weight: bold; }}
        button:hover {{ background: #0056b3; }}
        .calc-btn {{ background: #28a745; }}
        .calc-btn:hover {{ background: #218838; }}
        .reset-btn {{ background: #6c757d; }}
        .reset-btn:hover {{ background: #545b62; }}
        .const-btn {{ background: #fd7e14; margin: 3px; padding: 8px 15px; font-size: 14px; min-width: 160px; text-align: center; }}
        .const-btn:hover {{ background: #e8681a; }}
        .result-section {{ margin: 20px 0; padding: 20px; border-radius: 10px; }}
        .result-section.success {{ background: #d4edda; border: 1px solid #c3e6cb; color: #155724; }}
        .result-section.error {{ background: #f8d7da; border: 1px solid #f5c6cb; color: #721c24; }}
        .calculation {{ background: #f8f9fa; padding: 15px; border-radius: 8px; margin: 10px 0; }}
        .result-value {{ font-size: 24px; font-weight: bold; color: #007bff; }}
        .header {{ background: #e9ecef; padding: 20px; border-radius: 10px; margin: 20px 0; text-align: center; }}
        .operator-grid {{ display: grid; grid-template-columns: 1fr 1fr; gap: 15px; }}
        .form-section {{ background: #f8f9fa; padding: 20px; border-radius: 10px; border: 1px solid #dee2e6; }}
        .constants-section {{ background: #fff3cd; padding: 15px; border-radius: 8px; margin: 15px 0; border: 1px solid #ffeaa7; }}
        h1, h2, h3 {{ color: #333; }}
        .input-display {{ background: #e9ecef; padding: 10px; border-radius: 5px; margin: 10px 0; font-family: monospace; }}
        .operation-categories {{ display: grid; grid-template-columns: 1fr 1fr 1fr; gap: 10px; margin: 15px 0; }}
        .category {{ background: #f8f9fa; padding: 10px; border-radius: 5px; border: 1px solid #dee2e6; }}
        .category h5 {{ margin: 5px 0; color: #495057; }}
        select optgroup {{ font-weight: bold; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🧮 고급 과학 계산기</h1>
            <p>기본 연산부터 삼각함수, 로그, 지수함수까지 지원하는 과학 계산기입니다.</p>
        </div>
        
        <div class='calculator-section'>
            <form method='get' action='/operator'>
                <div class='form-section'>
                    <h3>📊 계산 입력</h3>
                    
                    <div class='constants-section'>
                        <h4>🔢 수학 상수</h4>
                        <button type='button' class='const-btn' onclick='setConstant(""e"", {Math.E})'>자연상수 e ≈ {Math.E:F6}</button>
                        <button type='button' class='const-btn' onclick='setConstant(""π"", {Math.PI})'>원주율 π ≈ {Math.PI:F6}</button>
                        <button type='button' class='const-btn' onclick='setConstant(""√2"", {Math.Sqrt(2)})'>√2 ≈ {Math.Sqrt(2):F6}</button>
                        <button type='button' class='const-btn' onclick='setConstant(""φ"", {(1 + Math.Sqrt(5)) / 2})'>황금비 φ ≈ {(1 + Math.Sqrt(5)) / 2:F6}</button>
                    </div>
                    
                    <div class='operator-grid'>
                        <div class='input-group'>
                            <label for='op1'>첫 번째 숫자 (op1):</label>
                            <input type='text' id='op1' name='op1' value='{op1Value}' placeholder='예: 123.45 또는 상수 클릭' required>
                        </div>
                        
                        <div class='input-group'>
                            <label for='op2'>두 번째 숫자 (op2):</label>
                            <input type='text' id='op2' name='op2' value='{op2Value}' placeholder='예: 67.89 (일부 함수는 불필요)'>
                        </div>
                    </div>
                    
                    <div class='input-group'>
                        <label for='opr'>연산자 (opr):</label>
                        <select id='opr' name='opr' required onchange='updateInputVisibility()'>
                            <optgroup label='기본 연산'>
                                <option value='add' {(oprValue == "add" ? "selected" : "")}>덧셈 (+)</option>
                                <option value='sub' {(oprValue == "sub" ? "selected" : "")}>뺄셈 (-)</option>
                                <option value='mul' {(oprValue == "mul" ? "selected" : "")}>곱셈 (×)</option>
                                <option value='div' {(oprValue == "div" ? "selected" : "")}>나눗셈 (÷)</option>
                                <option value='mod' {(oprValue == "mod" ? "selected" : "")}>나머지 (mod)</option>
                            </optgroup>
                            <optgroup label='고급 연산'>
                                <option value='pow' {(oprValue == "pow" ? "selected" : "")}>거듭제곱 (^)</option>
                                <option value='sqrt' {(oprValue == "sqrt" ? "selected" : "")}>제곱근 (√) - op1만 사용</option>
                                <option value='log' {(oprValue == "log" ? "selected" : "")}>로그 (log₍op1₎(op2))</option>
                                <option value='ln' {(oprValue == "ln" ? "selected" : "")}>자연로그 (ln) - op1만 사용</option>
                                <option value='exp' {(oprValue == "exp" ? "selected" : "")}>지수함수 (e^op1) - op1만 사용</option>
                            </optgroup>
                            <optgroup label='삼각함수 (도 단위)'>
                                <option value='sin' {(oprValue == "sin" ? "selected" : "")}>사인 (sin) - op1만 사용</option>
                                <option value='cos' {(oprValue == "cos" ? "selected" : "")}>코사인 (cos) - op1만 사용</option>
                                <option value='tan' {(oprValue == "tan" ? "selected" : "")}>탄젠트 (tan) - op1만 사용</option>
                            </optgroup>
                        </select>
                    </div>
                    
                    <div class='operation-categories'>
                        <div class='category'>
                            <h5>🔢 기본 연산</h5>
                            <p>사칙연산, 나머지</p>
                        </div>
                        <div class='category'>
                            <h5>📐 고급 수학</h5>
                            <p>거듭제곱, 제곱근, 로그</p>
                        </div>
                        <div class='category'>
                            <h5>📊 삼각함수</h5>
                            <p>sin, cos, tan (도 단위)</p>
                        </div>
                    </div>
                    
                    <div class='button-group'>
                        <button type='submit' class='calc-btn'>🔢 계산하기</button>
                        <button type='button' class='reset-btn' onclick='resetForm()'>🔄 다시하기</button>
                    </div>
                </div>
            </form>
            
            {resultHtml}
            
            {(op1.HasValue && !string.IsNullOrEmpty(opr) ? $@"
            <div class='input-display'>
                <h4>📝 입력된 값:</h4>
                <p><strong>op1:</strong> {op1.Value}</p>
                {(op2.HasValue ? $"<p><strong>op2:</strong> {op2.Value}</p>" : "")}
                <p><strong>opr:</strong> {opr}</p>
            </div>" : "")}
        </div>
        
        <div style='text-align: center; margin: 20px 0;'>
            <button onclick='window.location.href=""/"";'>🏠 홈으로 돌아가기</button>
        </div>
        
        <div style='background: #fff3cd; padding: 15px; border-radius: 8px; margin: 20px 0; border: 1px solid #ffeaa7;'>
            <h4>💡 사용법:</h4>
            <ul style='margin: 10px 0; padding-left: 20px;'>
                <li><strong>기본 연산:</strong> 두 숫자 모두 필요 (덧셈, 뺄셈, 곱셈, 나눗셈, 나머지, 거듭제곱, 로그)</li>
                <li><strong>단일 함수:</strong> op1만 사용 (제곱근, 자연로그, 지수함수, 삼각함수)</li>
                <li><strong>삼각함수:</strong> 각도는 도(degree) 단위로 입력</li>
                <li><strong>수학 상수:</strong> 버튼을 클릭하여 정확한 값 자동 입력</li>
                <li><strong>예시:</strong> sin(30°) = 0.5, ln(e) = 1, √9 = 3, 2^3 = 8</li>
            </ul>
        </div>
    </div>
    
    <script>
        function resetForm() {{
            document.getElementById('op1').value = '';
            document.getElementById('op2').value = '';
            document.getElementById('opr').value = 'add';
            updateInputVisibility();
            window.location.href = '/operator';
        }}
        
        function setConstant(name, value) {{
            document.getElementById('op1').value = value;
            document.getElementById('op1').style.borderColor = '#28a745';
            setTimeout(function() {{
                document.getElementById('op1').style.borderColor = '#ddd';
            }}, 1000);
        }}
        
        function updateInputVisibility() {{
            var operation = document.getElementById('opr').value;
            var op2Group = document.getElementById('op2').parentElement;
            var singleOperations = ['sqrt', 'ln', 'exp', 'sin', 'cos', 'tan'];
            
            if (singleOperations.includes(operation)) {{
                op2Group.style.opacity = '0.5';
                document.getElementById('op2').required = false;
                document.getElementById('op2').placeholder = '(이 연산에는 필요하지 않음)';
            }} else {{
                op2Group.style.opacity = '1';
                document.getElementById('op2').required = true;
                document.getElementById('op2').placeholder = '예: 67.89';
            }}
        }}
        
        // 입력 검증
        document.getElementById('op1').addEventListener('input', function(e) {{
            var value = e.target.value;
            if (value && isNaN(value)) {{
                e.target.style.borderColor = '#dc3545';
            }} else {{
                e.target.style.borderColor = '#ddd';
            }}
        }});
        
        document.getElementById('op2').addEventListener('input', function(e) {{
            var value = e.target.value;
            if (value && isNaN(value)) {{
                e.target.style.borderColor = '#dc3545';
            }} else {{
                e.target.style.borderColor = '#ddd';
            }}
        }});
        
        // 페이지 로드 시 입력 가시성 업데이트
        updateInputVisibility();
    </script>
</body>
</html>", "text/html");
});

// 웹 애플리케이션 실행 시작
// 이 메소드는 블로킹되어 서버가 종료될 때까지 계속 실행됨
// Ctrl+C로 종료 가능
app.Run();