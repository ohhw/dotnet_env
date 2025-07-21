# MyConsoleApp - ASP.NET Core Web Application

🎯 **ASP.NET Core 9.0** 기반의 다기능 웹 애플리케이션으로, 메모 관리, 과학 계산기, 시간 표시 기능을 제공합니다.

## 📋 주요 기능

### 🗒️ 메모 관리 시스템
- **메모 추가**: 텍스트 메모를 생성하고 저장
- **메모 조회**: 등록된 모든 메모를 목록으로 확인
- **메모 삭제**: 불필요한 메모를 개별 삭제
- **메모 내보내기**: TXT 파일로 다양한 형식으로 내보내기
  - 번호 매김 형식
  - 타임스탬프 형식
  - 단순 형식
  - 상세 형식

### 🧮 과학 계산기
- **기본 연산**: 사칙연산 (+, -, ×, ÷)
- **고급 수학 함수**:
  - 삼각함수: sin, cos, tan
  - 로그함수: log (상용로그), ln (자연로그)
  - 지수함수: exp (e^x)
  - 기타: sqrt (제곱근), abs (절댓값)
- **수학 상수**: π (파이), e (자연상수)
- **실시간 계산 결과 표시**

### ⏰ 시간 표시
- **현재 시간 조회**: 다양한 형식으로 시간 표시
- **JSON 형식 출력**: 구조화된 시간 데이터 제공
- **UTC/로컬 시간 지원**

## 🏗️ 프로젝트 구조

```
MyConsoleApp/
├── Program.cs                    # 메인 애플리케이션 설정
├── SharedData.cs                 # 공유 데이터 클래스 (메모 저장소)
├── MyConsoleApp.csproj          # 프로젝트 설정 파일
├── Pages/                       # Razor Pages
│   ├── Index.cshtml             # 홈페이지
│   ├── Index.cshtml.cs          # 홈페이지 로직
│   ├── HelloWorld.cshtml        # 정보 페이지
│   ├── Calculator/              # 계산기 기능
│   │   ├── Index.cshtml         # 계산기 UI
│   │   └── Index.cshtml.cs      # 계산기 로직
│   ├── Time/                    # 시간 표시 기능
│   │   ├── Index.cshtml         # 시간 UI
│   │   └── Index.cshtml.cs      # 시간 로직
│   ├── Memo/                    # 메모 관리 기능
│   │   ├── Add.cshtml           # 메모 추가 UI
│   │   ├── Add.cshtml.cs        # 메모 추가 로직
│   │   ├── View.cshtml          # 메모 조회 UI
│   │   ├── View.cshtml.cs       # 메모 조회 로직
│   │   ├── Delete.cshtml        # 메모 삭제 UI
│   │   ├── Delete.cshtml.cs     # 메모 삭제 로직
│   │   ├── Export.cshtml        # 메모 내보내기 UI
│   │   └── Export.cshtml.cs     # 메모 내보내기 로직
│   └── Shared/                  # 공통 레이아웃
│       ├── _Layout.cshtml       # 기본 레이아웃
│       └── _ViewStart.cshtml    # 레이아웃 설정
└── bin/Debug/net9.0/           # 빌드 출력 폴더
```

## 🛠️ 기술 스택

- **프레임워크**: ASP.NET Core 9.0
- **언어**: C# (.NET 9.0)
- **웹 기술**: Razor Pages, HTML, CSS, JavaScript
- **아키텍처**: MVC 패턴 (Razor Pages)
- **데이터 저장**: In-Memory (List<string>)
- **파일 처리**: System.IO, UTF-8 인코딩

## 🚀 실행 방법

### 필수 요구사항
- .NET 9.0 SDK
- Windows 10/11 또는 Linux/macOS

### 실행 단계

1. **프로젝트 클론**:
   ```bash
   git clone [repository-url]
   cd MyConsoleApp
   ```

2. **패키지 복원**:
   ```bash
   dotnet restore
   ```

3. **빌드**:
   ```bash
   dotnet build
   ```

4. **실행**:
   ```bash
   dotnet run
   ```

5. **브라우저에서 접속**:
   ```
   http://localhost:5005
   ```

## 🌐 URL 경로

| 기능 | URL | 설명 |
|------|-----|------|
| 홈페이지 | `/` | 메인 페이지 및 메모 개요 |
| 계산기 | `/Calculator` | 과학 계산기 |
| 시간 표시 | `/Time` | 현재 시간 조회 |
| 메모 추가 | `/Memo/Add` | 새 메모 작성 |
| 메모 조회 | `/Memo/View` | 메모 목록 보기 |
| 메모 삭제 | `/Memo/Delete` | 메모 삭제 |
| 메모 내보내기 | `/Memo/Export` | TXT 파일로 내보내기 |
| 정보 페이지 | `/HelloWorld` | 애플리케이션 정보 |

## 🎨 주요 특징

### 📱 반응형 디자인
- 모바일 및 데스크톱 환경 모두 지원
- 통일된 14px 버튼 폰트 크기
- 일관된 UI/UX 디자인

### 🔧 모듈화된 구조
- Razor Pages 아키텍처 적용
- 기능별 폴더 분리
- 재사용 가능한 컴포넌트

### 💾 데이터 관리
- SharedData 클래스를 통한 중앙 집중식 데이터 관리
- 메모리 기반 저장소 (앱 재시작 시 초기화)
- TXT 파일 내보내기 지원

### 🔒 안정성
- 에러 핸들링 및 검증
- 개발/프로덕션 환경 분리
- 안전한 파일 처리

## 🔄 개발 히스토리

1. **v1.0**: 기본 웹 애플리케이션 및 계산기 기능
2. **v1.1**: 메모 관리 시스템 추가
3. **v1.2**: 고급 수학 함수 및 UI 개선
4. **v1.3**: TXT 파일 내보내기 기능
5. **v1.4**: Razor Pages 아키텍처로 리팩토링
6. **v1.5**: 폴더 구조 모듈화 (현재 버전)

## 🤝 기여하기

1. Fork the repository
2. Create feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 라이선스

이 프로젝트는 개인 학습 목적으로 개발되었습니다.

## 👨‍💻 개발자

**오현우** - .NET 학습 프로젝트

---

> **Note**: 이 애플리케이션은 메모리 기반 저장소를 사용하므로 애플리케이션 재시작 시 데이터가 초기화됩니다. 영구 저장이 필요한 경우 데이터베이스 연동을 고려해보세요.
