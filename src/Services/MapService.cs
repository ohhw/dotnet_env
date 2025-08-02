namespace App.Services
{
    /// <summary>
    /// 지도 관련 서비스를 제공하는 클래스
    /// </summary>
    public class MapService
    {
        /// <summary>
        /// 지원되는 지도 서비스 열거형
        /// </summary>
        public enum MapProvider
        {
            Google,
            Naver,
            Kakao
        }

        /// <summary>
        /// 지도 검색 결과를 나타내는 클래스
        /// </summary>
        public class MapSearchResult
        {
            public string Url { get; set; } = string.Empty;
            public string Provider { get; set; } = string.Empty;
            public bool IsEmbeddable { get; set; }
            public string ErrorMessage { get; set; } = string.Empty;
        }

        /// <summary>
        /// 기본 Google 지도 임베드 URL을 가져옵니다
        /// </summary>
        /// <param name="latitude">위도 (기본값: 서울 위도)</param>
        /// <param name="longitude">경도 (기본값: 서울 경도)</param>
        /// <param name="zoom">줌 레벨 (기본값: 13)</param>
        /// <returns>Google 지도 임베드 URL</returns>
        public string GetGoogleMapEmbedUrl(double latitude = 37.4979, double longitude = 127.0276, int zoom = 13)
        {
            return $"https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d12345.67!2d{longitude}!3d{latitude}!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f{zoom}.1!3m3!1m2!1s0x0%3A0x0!2z7ISc7Jq47Yq567OE7IucIOqwlee0lOq1rCDqsJXrgqTroZw!5e0!3m2!1sko!2skr!4v1691234567890!5m2!1sko!2skr";
        }

        /// <summary>
        /// 장소 검색 URL을 생성합니다
        /// </summary>
        /// <param name="query">검색할 장소</param>
        /// <param name="provider">지도 서비스 제공업체</param>
        /// <returns>검색 결과</returns>
        public MapSearchResult GetPlaceSearchUrl(string query, MapProvider provider = MapProvider.Google)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new MapSearchResult
                {
                    ErrorMessage = "검색할 장소를 입력해주세요."
                };
            }

            var encodedQuery = Uri.EscapeDataString(query.Trim());

            return provider switch
            {
                MapProvider.Google => new MapSearchResult
                {
                    Url = $"https://maps.google.com/maps?q={encodedQuery}",
                    Provider = "Google Maps",
                    IsEmbeddable = true
                },
                MapProvider.Naver => new MapSearchResult
                {
                    Url = $"https://map.naver.com/v5/search/{encodedQuery}",
                    Provider = "Naver Map",
                    IsEmbeddable = false
                },
                MapProvider.Kakao => new MapSearchResult
                {
                    Url = $"https://map.kakao.com/link/search/{encodedQuery}",
                    Provider = "Kakao Map",
                    IsEmbeddable = false
                },
                _ => new MapSearchResult
                {
                    ErrorMessage = "지원되지 않는 지도 서비스입니다."
                }
            };
        }

        /// <summary>
        /// 경로 검색 URL을 생성합니다
        /// </summary>
        /// <param name="startLocation">출발지</param>
        /// <param name="endLocation">도착지</param>
        /// <param name="provider">지도 서비스 제공업체</param>
        /// <returns>경로 검색 결과</returns>
        public MapSearchResult GetDirectionsUrl(string startLocation, string endLocation, MapProvider provider = MapProvider.Google)
        {
            if (string.IsNullOrWhiteSpace(startLocation) || string.IsNullOrWhiteSpace(endLocation))
            {
                return new MapSearchResult
                {
                    ErrorMessage = "출발지와 도착지를 모두 입력해주세요."
                };
            }

            var encodedStart = Uri.EscapeDataString(startLocation.Trim());
            var encodedEnd = Uri.EscapeDataString(endLocation.Trim());

            return provider switch
            {
                MapProvider.Google => new MapSearchResult
                {
                    Url = $"https://maps.google.com/maps?saddr={encodedStart}&daddr={encodedEnd}",
                    Provider = "Google Maps",
                    IsEmbeddable = false
                },
                MapProvider.Naver => new MapSearchResult
                {
                    Url = $"https://map.naver.com/p/search/{Uri.EscapeDataString($"{startLocation} {endLocation} 경로")}",
                    Provider = "Naver Map",
                    IsEmbeddable = false
                },
                MapProvider.Kakao => new MapSearchResult
                {
                    Url = $"https://map.kakao.com/link/search/{Uri.EscapeDataString($"{startLocation} {endLocation} 길찾기")}",
                    Provider = "Kakao Map",
                    IsEmbeddable = false
                },
                _ => new MapSearchResult
                {
                    ErrorMessage = "지원되지 않는 지도 서비스입니다."
                }
            };
        }

        /// <summary>
        /// 좌표 기반 검색 URL을 생성합니다
        /// </summary>
        /// <param name="latitude">위도</param>
        /// <param name="longitude">경도</param>
        /// <param name="provider">지도 서비스 제공업체</param>
        /// <returns>좌표 검색 결과</returns>
        public MapSearchResult GetCoordinateSearchUrl(double latitude, double longitude, MapProvider provider = MapProvider.Google)
        {
            return provider switch
            {
                MapProvider.Google => new MapSearchResult
                {
                    Url = $"https://maps.google.com/maps?q={latitude},{longitude}",
                    Provider = "Google Maps",
                    IsEmbeddable = true
                },
                MapProvider.Naver => new MapSearchResult
                {
                    Url = $"https://map.naver.com/v5/search/{latitude},{longitude}",
                    Provider = "Naver Map",
                    IsEmbeddable = false
                },
                MapProvider.Kakao => new MapSearchResult
                {
                    Url = $"https://map.kakao.com/link/map/,{latitude},{longitude}",
                    Provider = "Kakao Map",
                    IsEmbeddable = false
                },
                _ => new MapSearchResult
                {
                    ErrorMessage = "지원되지 않는 지도 서비스입니다."
                }
            };
        }

        /// <summary>
        /// 지도 서비스별 특징을 가져옵니다
        /// </summary>
        /// <param name="provider">지도 서비스 제공업체</param>
        /// <returns>서비스 특징</returns>
        public string GetProviderDescription(MapProvider provider)
        {
            return provider switch
            {
                MapProvider.Google => "전 세계 지도, 위성 이미지, 스트리트 뷰 제공",
                MapProvider.Naver => "한국 지역 상세 정보, 실시간 교통 정보 제공",
                MapProvider.Kakao => "한국 지역 특화, 대중교통 정보 우수",
                _ => "알 수 없는 서비스"
            };
        }

        /// <summary>
        /// 모든 지도 서비스 제공업체 목록을 가져옵니다
        /// </summary>
        /// <returns>제공업체 목록</returns>
        public IEnumerable<(MapProvider Provider, string Name, string Description)> GetAllProviders()
        {
            return new[]
            {
                (MapProvider.Google, "Google Maps", GetProviderDescription(MapProvider.Google)),
                (MapProvider.Naver, "Naver Map", GetProviderDescription(MapProvider.Naver)),
                (MapProvider.Kakao, "Kakao Map", GetProviderDescription(MapProvider.Kakao))
            };
        }

        /// <summary>
        /// 유명한 한국 관광지 목록을 가져옵니다
        /// </summary>
        /// <returns>관광지 목록</returns>
        public IEnumerable<(string Name, double Latitude, double Longitude)> GetPopularKoreanDestinations()
        {
            return new[]
            {
                ("경복궁", 37.5796, 126.9770),
                ("남산타워", 37.5512, 126.9882),
                ("부산 해운대", 35.1584, 129.1604),
                ("제주도 한라산", 33.3617, 126.5292),
                ("경주 불국사", 35.7897, 129.3316),
                ("강릉 정동진", 37.6907, 129.0348),
                ("설악산 국립공원", 38.1197, 128.4656),
                ("전주 한옥마을", 35.8167, 127.1530)
            };
        }

        /// <summary>
        /// 거리 계산 (하버사인 공식 사용)
        /// </summary>
        /// <param name="lat1">첫 번째 지점 위도</param>
        /// <param name="lon1">첫 번째 지점 경도</param>
        /// <param name="lat2">두 번째 지점 위도</param>
        /// <param name="lon2">두 번째 지점 경도</param>
        /// <returns>거리 (킬로미터)</returns>
        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double earthRadius = 6371; // 지구 반지름 (km)
            
            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);
            
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            
            return earthRadius * c;
        }

        /// <summary>
        /// 도를 라디안으로 변환합니다
        /// </summary>
        /// <param name="degrees">도</param>
        /// <returns>라디안</returns>
        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
