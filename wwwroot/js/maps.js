// maps.js - 지도 관련 JavaScript (실용적 간소화 버전)

// 글로벌 변수로 API 키 저장 (서버에서 제공)
let googleMapsApiKey = '';

// API 키 설정 함수 (서버에서 호출)
function setGoogleMapsApiKey(apiKey) {
    googleMapsApiKey = apiKey;
}

// 실시간 지도 - 구글만 사용
function switchToGoogle() {
    // Google 지도 iframe 표시 (서울 중심가)
    const mapFrame = document.getElementById('mapFrame');
    mapFrame.style.display = 'block';
    mapFrame.src = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d25320.27!2d126.9780!3d37.5665!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x357ca2eb6b5ace79%3A0x5e0d96d2b6b08b12!2z7ISc7Jq4!5e0!3m2!1sko!2skr!4v1691234567890!5m2!1sko!2skr";
    
    document.getElementById('mapDescription').textContent = '현재: Google 지도 (실시간)';
}

// 경로 검색 - 구글만 사용
function routeSearch() {
    const start = document.getElementById('startLocation').value;
    const end = document.getElementById('endLocation').value;
    
    if (!start.trim() || !end.trim()) {
        alert('🚩 출발지와 목적지를 모두 입력해주세요!');
        return;
    }
    
    // 구글 지도 길찾기
    const url = `https://www.google.com/maps/dir/${encodeURIComponent(start)}/${encodeURIComponent(end)}`;
    window.open(url, '_blank');
    console.log(`Google 지도 길찾기: "${start}" → "${end}"`);
}

// 빠른 장소 검색 - 3개 서비스별 버튼
function quickSearchGoogle() {
    const query = document.getElementById('quickSearchLocation').value;
    if (!query.trim()) {
        alert('🚩 검색할 장소를 입력해주세요!');
        return;
    }
    window.open(`https://maps.google.com/maps?q=${encodeURIComponent(query)}`, '_blank');
    console.log(`Google 지도 검색: "${query}"`);
}

function quickSearchNaver() {
    const query = document.getElementById('quickSearchLocation').value;
    if (!query.trim()) {
        alert('🚩 검색할 장소를 입력해주세요!');
        return;
    }
    window.open(`https://map.naver.com/v5/search/${encodeURIComponent(query)}`, '_blank');
    console.log(`네이버 지도 검색: "${query}"`);
}

function quickSearchKakao() {
    const query = document.getElementById('quickSearchLocation').value;
    if (!query.trim()) {
        alert('🚩 검색할 장소를 입력해주세요!');
        return;
    }
    window.open(`https://map.kakao.com/?q=${encodeURIComponent(query)}`, '_blank');
    console.log(`카카오맵 검색: "${query}"`);
}

// Google Maps 임베드 검색 기능 (API 키는 환경 변수에서 불러옴)
function searchGoogleMapsEmbed(query) {
    if (!query.trim()) {
        alert('🚩 검색할 장소를 입력해주세요!');
        return;
    }
    
    if (!googleMapsApiKey) {
        alert('⚠️ Google Maps API 키가 설정되지 않았습니다.');
        console.error('Google Maps API key not set');
        return;
    }
    
    // API 키는 환경 변수에서 불러옴
    const mapFrame = document.getElementById('mapFrame');
    if (mapFrame) {
        mapFrame.src = `https://www.google.com/maps/embed/v1/search?key=${googleMapsApiKey}&q=${encodeURIComponent(query)}`;
        document.getElementById('mapDescription').textContent = `검색 결과: ${query}`;
    }
}
