
using Azure.Core.GeoJson;
using Yandex;
using static MudBlazor.CategoryTypes;
using Yandex.Geocoder.Enums;
using Yandex.Geocoder;
using System.Text.Json;

namespace MolotovSportWeb.Components.Classes.Servers
{
    public class Geocode
    {

        private static readonly HttpClient client = new HttpClient();
        private const string ApiKey = "16bc456d-419e-42b8-8466-a6b4f2a01e25"; // Вставь сюда свой API-ключ

        public async Task<string> GetAddressAsync(double latitude, double longitude)
        {
            string url = $"https://geocode-maps.yandex.ru/1.x/?format=json&geocode={longitude},{latitude}&apikey={ApiKey}&lang=ru_RU";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(responseBody);

                var featureMember = doc.RootElement
                    .GetProperty("response")
                    .GetProperty("GeoObjectCollection")
                    .GetProperty("featureMember");

                if (featureMember.GetArrayLength() > 0)
                {
                    return featureMember[0]
                        .GetProperty("GeoObject")
                        .GetProperty("metaDataProperty")
                        .GetProperty("GeocoderMetaData")
                        .GetProperty("text")
                        .GetString();
                }

                return "Адрес не найден";
            }
            catch (Exception ex)
            {
                return $"Ошибка: {ex.Message}";
            }
        }

    }
}
