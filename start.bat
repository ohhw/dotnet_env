@echo off
chcp 65001 >nul

echo 🔍 포트 5005를 사용하는 프로세스 확인 중...

REM 포트 5005를 사용하는 프로세스 찾기
for /f "tokens=5" %%a in ('netstat -aon ^| findstr ":5005"') do (
    set PORT_PID=%%a
    goto :found
)
goto :notfound

:found
echo ⚠️  포트 5005를 사용하는 프로세스 발견: %PORT_PID%
echo 🔄 기존 프로세스 종료 중...
taskkill /F /PID %PORT_PID% >nul 2>&1
timeout /t 2 >nul
echo ✅ 기존 프로세스 정리 완료
goto :start

:notfound
echo ✅ 포트 5005는 사용 중이지 않습니다

:start
echo 🚀 .NET 애플리케이션 시작 중...
echo 📍 접속 주소: http://localhost:5005
echo ⏹️  종료하려면 Ctrl+C를 누르세요
echo.

REM .NET 애플리케이션 실행
dotnet run
