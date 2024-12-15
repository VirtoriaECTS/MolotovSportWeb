using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace MolotovSportWeb.Components.Classes.Servers
{
    public class YandexGeocoderService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "16bc456d-419e-42b8-8466-a6b4f2a01e25";
        private const string BaseUrl = "https://geocode-maps.yandex.ru/1.x/";

        public YandexGeocoderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAddressAsync(double longitude, double latitude)
        {
            string requestUrl = $"{BaseUrl}?apikey={ApiKey}&geocode={longitude},{latitude}&format=json";
            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");

            var json = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(json);

            // Извлекаем "formatted" адрес
            var formattedAddress = document
                .RootElement
                .GetProperty("response")
                .GetProperty("GeoObjectCollection")
                .GetProperty("featureMember")[0]
                .GetProperty("GeoObject")
                .GetProperty("metaDataProperty")
                .GetProperty("GeocoderMetaData")
                .GetProperty("Address")
                .GetProperty("formatted")
                .GetString();

            return formattedAddress ?? "Address not found";
        }
    }
}

