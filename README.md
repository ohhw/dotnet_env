# .NET TEST - ASP.NET Core Web Application

🎯 **ASP.NET Core 9.0** 기반의 **Razor Pages 아키텍처**를 사용한 다기능 웹 애플리케이션으로, 메모 관리, 과학 계산기, 시간 표시 기능을 제공합니다.

## 🌟 최신 업데이트 (v2.1)

### �️ **브랜딩 개선**
- **브랜드명**: "MyConsoleApp" → **".NET TEST"** 변경
- **전용 포트**: **5005번 포트**로 기본 설정 (http| **📱 라이브 데모** | http://localhost:5005 (로컬 실행 시) |

---

## ⚠️ 중요 참고사항

> **🔥 개발 환경**: `dotnet watch run` 명령어를 사용하면 파일 변경 시 자동으로 빌드되고 브라우저가 새로고침됩니다. 실시간 개발에 최적화!

> **🎯 전용 포트**: 이 애플리케이션은 **포트 5005**에서 실행됩니다. 브라우저에서 `http://localhost:5005`로 접속하세요.

> **📌 메모리 기반 저장소**: 이 애플리케이션은 메모리 기반 저장소를 사용하므로 애플리케이션 재시작 시 모든 데이터가 초기화됩니다. 중요한 메모는 **TXT 파일 내보내기** 기능을 활용해주세요!

> **🔧 개발 환경**: 학습용 프로젝트로 개발되어 실제 프로덕션 환경에서는 데이터베이스 연동 등의 추가 개발이 필요합니다.

---

**🌟 Made with ❤️ by 오현우 | ASP.NET Core 9.0 + Razor Pages | .NET TEST 브랜딩**- **로고 브랜딩**: 헤더 좌측에 명확한 브랜드 표시

### 🎨 **UI/UX 최적화**
- **폰트 크기 조정**: 헤더 네비게이션 폰트를 적절한 크기로 조정 (1.4rem)
- **푸터 폰트**: 깔끔한 소형 폰트로 변경 (0.73rem)
- **네비게이션 정렬**: 메뉴 버튼들을 화면 중앙으로 균형있게 배치
- **개발자 친화적**: `dotnet watch run` 지원으로 실시간 개발 가능

### 🧭 **완벽한 네비게이션 구조**
- **브랜드 영역**: 좌측 ".NET TEST" (홈 링크)
- **중앙 메뉴**: Home, Memo, Time, Calculator, About
- **투명 배경**: 자연스러운 크림슨 테마 통합
- **활성 상태**: 현재 페이지 하이라이트 표시

### � **개발 환경 향상**
- **Hot Reload**: 파일 변경 시 자동 빌드 및 브라우저 새로고침
- **Watch 모드**: `dotnet watch run`으로 실시간 개발 지원
- **전용 포트**: 5005번 포트로 일관된 개발 환경

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
├── Program.cs                    # 메인 애플리케이션 설정 (Razor Pages)
├── SharedData.cs                 # 공유 데이터 클래스 (메모 저장소)
├── MyConsoleApp.csproj          # 프로젝트 설정 파일
├── README.md                    # 프로젝트 문서
├── Pages/                       # Razor Pages (모듈화된 구조)
│   ├── Index.cshtml             # 홈페이지
│   ├── Index.cshtml.cs          # 홈페이지 로직
│   ├── HelloWorld.cshtml        # 정보 페이지
│   ├── Privacy.cshtml           # 개인정보처리방침
│   ├── Privacy.cshtml.cs        # 개인정보처리방침 로직
│   ├── Terms.cshtml             # 이용약관
│   ├── Terms.cshtml.cs          # 이용약관 로직
│   ├── Contact.cshtml           # 문의하기
│   ├── Contact.cshtml.cs        # 문의하기 로직
│   ├── FAQ.cshtml               # 자주묻는질문
│   ├── FAQ.cshtml.cs            # 자주묻는질문 로직
│   ├── Calculator/              # 계산기 기능 (폴더 분리)
│   │   ├── Index.cshtml         # 계산기 UI
│   │   └── Index.cshtml.cs      # 계산기 로직
│   ├── Time/                    # 시간 표시 기능 (폴더 분리)
│   │   ├── Index.cshtml         # 시간 UI
│   │   └── Index.cshtml.cs      # 시간 로직
│   ├── Memo/                    # 메모 관리 기능 (완전 분리)
│   │   ├── Add.cshtml           # 메모 추가 UI
│   │   ├── Add.cshtml.cs        # 메모 추가 로직
│   │   ├── View.cshtml          # 메모 조회 UI
│   │   ├── View.cshtml.cs       # 메모 조회 로직
│   │   ├── Delete.cshtml        # 메모 삭제 UI
│   │   ├── Delete.cshtml.cs     # 메모 삭제 로직
│   │   ├── Export.cshtml        # 메모 내보내기 UI
│   │   └── Export.cshtml.cs     # 메모 내보내기 로직
│   └── Shared/                  # 공통 레이아웃
│       ├── _Layout.cshtml       # 기본 레이아웃 (헤더/푸터 포함)
│       └── _ViewStart.cshtml    # 레이아웃 설정
└── bin/Debug/net9.0/           # 빌드 출력 폴더
```
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
   
   **또는 개발 모드 (Hot Reload)**:
   ```bash
   dotnet watch run
   ```

5. **브라우저에서 접속**:
   ```
   http://localhost:5005
   ```

## 🌐 URL 경로

| 기능 분류 | URL | 설명 |
|-----------|-----|------|
| **메인** | `/` | 홈페이지 및 메모 개요 |
| **기능** | `/Calculator` | 과학 계산기 |
| | `/Time` | 현재 시간 조회 |
| **메모** | `/Memo/Add` | 새 메모 작성 |
| | `/Memo/View` | 메모 목록 보기 |
| | `/Memo/Delete` | 메모 삭제 |
| | `/Memo/Export` | TXT 파일로 내보내기 |
| **정보** | `/HelloWorld` | 애플리케이션 정보 |
| **정책** | `/Privacy` | 개인정보처리방침 |
| | `/Terms` | 이용약관 |
| | `/Contact` | 문의하기 |
| | `/FAQ` | 자주묻는질문 |

## 🎨 디자인 특징

### 🏛️ **크림슨 테마**
- **메인 색상**: `#8B0000` (크림슨 레드) - 헤더/푸터 배경
- **텍스트 색상**: `#D6C9B1` (따뜻한 베이지) - 주요 텍스트
- **호버 색상**: `#E8DCC0` (밝은 베이지) - 상호작용 요소
- **반투명 효과**: `rgba()` 색상으로 세련된 오버레이

### 📐 **최적화된 레이아웃**
- **브랜드 영역**: 좌측 ".NET TEST" 브랜드 로고
- **중앙 네비게이션**: 균형잡힌 메뉴 배치 (Home, Memo, Time, Calculator, About)
- **폰트 크기**: 헤더 1.4rem, 푸터 0.73rem으로 가독성 최적화
- **투명 배경**: 자연스러운 크림슨 테마 통합

### 🔥 **개발자 경험**
- **Hot Reload**: `dotnet watch run`으로 실시간 개발
- **전용 포트**: 5005번 포트로 일관된 개발 환경
- **자동 빌드**: 파일 변경 시 자동 컴파일 및 브라우저 새로고침

### 📱 **반응형 디자인**
- 모바일 및 데스크톱 환경 모두 지원
- 통일된 14px 버튼 폰트 크기
- 일관된 UI/UX 디자인
- 모바일에서 최적화된 네비게이션 메뉴

### 🧭 **네비게이션 시스템**
- **브랜드 로고**: 좌측 ".NET TEST" (홈 링크)
- **중앙 메뉴**: 🏠 Home | 📝 Memo | ⏰ Time | 🧮 Calculator | ℹ️ About
- **하단 푸터**: 개인정보처리방침 | 이용약관 | 문의하기 | FAQ | GitHub
- **호버 효과**: 부드러운 애니메이션과 색상 전환
- **투명 배경**: 자연스러운 메뉴 통합
- **활성 상태**: 현재 페이지 볼드 표시

### 🔧 **모듈화된 구조**
- Razor Pages 아키텍처 적용
- 기능별 폴더 분리 (Calculator/, Time/, Memo/)
- 재사용 가능한 컴포넌트
- 깔끔한 코드 구조

### 💾 데이터 관리
- SharedData 클래스를 통한 중앙 집중식 데이터 관리
- 메모리 기반 저장소 (앱 재시작 시 초기화)
- TXT 파일 내보내기 지원

### 🔒 안정성
- 에러 핸들링 및 검증
- 개발/프로덕션 환경 분리
- 안전한 파일 처리

## 🔄 개발 히스토리

1. **v1.0** (기초): 기본 웹 애플리케이션 및 계산기 기능
2. **v1.1** (메모): 메모 관리 시스템 추가
3. **v1.2** (고급): 고급 수학 함수 및 UI 개선
4. **v1.3** (파일): TXT 파일 내보내기 기능
5. **v1.4** (리팩토링): Razor Pages 아키텍처로 리팩토링
6. **v1.5** (모듈화): 폴더 구조 모듈화 (Calculator/, Time/, Memo/)
7. **v2.0** (테마): 크림슨 테마 + 완전한 네비게이션 시스템
8. **v2.1** (브랜딩): .NET TEST 브랜딩 + UI 최적화 (현재 버전)

### 🆕 **v2.1 주요 변경사항**
- 🏷️ **".NET TEST" 브랜딩** 적용
- 🎯 **전용 포트 5005** 설정
- � **네비게이션 중앙 정렬** 및 폰트 크기 최적화
- 🔥 **Hot Reload 개발 환경** 구축
- ✨ **개발자 친화적** UI/UX 개선

## 🛠️ 기술 스택

- **프레임워크**: ASP.NET Core 9.0
- **아키텍처**: Razor Pages (MVC 패턴)
- **언어**: C# (.NET 9.0)
- **프론트엔드**: HTML5, CSS3, JavaScript (ES6+)
- **스타일링**: 커스텀 CSS (크림슨 & 베이지 테마)
- **데이터 저장**: In-Memory (List<string>)
- **파일 처리**: System.IO, UTF-8 인코딩
- **반응형**: CSS Media Queries
- **버전 관리**: Git (GitHub)

## 🤝 기여하기

이 프로젝트는 **오픈소스**입니다! 다음과 같은 방법으로 기여할 수 있습니다:

### 🔧 **개발 기여**
1. Fork the repository
2. Create feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### � **아이디어 제안**
- **GitHub Issues**를 통한 버그 리포트 및 기능 제안
- **이메일**: xohhwx@gmail.com
- **새로운 기능**: 계산기 함수 추가, UI 개선, 새로운 페이지 등

### 🎨 **디자인 개선**
- 크림슨 테마 확장
- 모바일 UI 최적화
- 접근성(Accessibility) 향상

## 📞 연락처 & 링크

| 항목 | 링크 |
|------|------|
| **🐙 GitHub** | [https://github.com/ohhw/dotnet_env](https://github.com/ohhw/dotnet_env) |
| **📧 이메일** | xohhwx@gmail.com |
| **👨‍💻 개발자** | 오현우 |
| **🏛️ 프로젝트** | .NET 학습 & Razor Pages 실습 |
| **📱 라이브 데모** | http://localhost:5005 (로컬 실행 시) |

## �📄 라이선스

이 프로젝트는 **개인 학습 목적**으로 개발되었습니다.
- ✅ 학습 및 참고 목적 사용 가능
- ✅ 코드 분석 및 개선 제안 환영
- ✅ Fork 및 개인 프로젝트 활용 가능

---

## ⚠️ 중요 참고사항

> **📌 메모리 기반 저장소**: 이 애플리케이션은 메모리 기반 저장소를 사용하므로 애플리케이션 재시작 시 모든 데이터가 초기화됩니다. 중요한 메모는 **TXT 파일 내보내기** 기능을 활용해주세요!

> **🔧 개발 환경**: 학습용 프로젝트로 개발되어 실제 프로덕션 환경에서는 데이터베이스 연동 등의 추가 개발이 필요합니다.

---

**🌟 Made with ❤️ by 오현우 | ASP.NET Core 9.0 + Razor Pages | 크림슨 테마**
