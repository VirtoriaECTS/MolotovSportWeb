
using Azure.Core.GeoJson;
using Yandex;
using static MudBlazor.CategoryTypes;
using Yandex.Geocoder.Enums;
using Yandex.Geocoder;

namespace MolotovSportWeb.Components.Classes.Servers
{
    public class Geocod
    {

        public async Task cheakAsync()
        {
            var request = new ReverseGeocoderRequest { Latitude = 58.046733, Longitude = 38.841715 };

            var client = new GeocoderClient("ydyd");

            var response = await client.ReverseGeocode(request);

            var firstGeoObject = response.GeoObjectCollection.FeatureMember.FirstOrDefault();
            var addressComponents = firstGeoObject.GeoObject.MetaDataProperty.GeocoderMetaData.Address.Components;
            var country = addressComponents.FirstOrDefault(c => c.Kind.Equals(AddressComponentKind.Country));
            var province = addressComponents.LastOrDefault(c => c.Kind.Equals(AddressComponentKind.Province));
            var area = addressComponents.FirstOrDefault(c => c.Kind.Equals(AddressComponentKind.Area));
            var city = addressComponents.FirstOrDefault(c => c.Kind.Equals(AddressComponentKind.Locality));
            var street = addressComponents.FirstOrDefault(c => c.Kind.Equals(AddressComponentKind.Street));
            var house = addressComponents.FirstOrDefault(c => c.Kind.Equals(AddressComponentKind.House));

            //Assert.Equal("Россия", country.Name);
            //Assert.Equal("Ярославская область", province.Name);
            //Assert.Equal("городской округ Рыбинск", area.Name);
            //Assert.Equal("Рыбинск", city.Name);
            //Assert.Equal("улица Бородулина", street.Name);
            //Assert.Equal("23", house.Name);
        }

    }
}
