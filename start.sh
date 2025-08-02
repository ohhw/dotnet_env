#!/bin/bash

# .NET 애플리케이션 실행 스크립트
# 기존 프로세스 정리 후 새로운 인스턴스 실행

echo "🔍 포트 5005를 사용하는 프로세스 확인 중..."

# 포트 5005를 사용하는 프로세스 찾기
PORT_PROCESS=$(lsof -ti:5005)

if [ -n "$PORT_PROCESS" ]; then
    echo "⚠️  포트 5005를 사용하는 프로세스 발견: $PORT_PROCESS"
    echo "🔄 기존 프로세스 종료 중..."
    
    # 프로세스 종료
    kill -9 $PORT_PROCESS 2>/dev/null
    
    # 잠시 대기
    sleep 2
    
    echo "✅ 기존 프로세스 정리 완료"
else
    echo "✅ 포트 5005는 사용 중이지 않습니다"
fi

echo "🚀 .NET 애플리케이션 시작 중..."
echo "📍 접속 주소: http://localhost:5005"
echo "⏹️  종료하려면 Ctrl+C를 누르세요"
echo ""

# .NET 애플리케이션 실행
dotnet run
