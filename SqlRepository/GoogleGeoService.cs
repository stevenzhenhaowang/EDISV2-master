using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SqlRepository
{
    /// <summary>
    /// Wrapper of Google Place API
    /// Created By Kaida Meng
    /// </summary>
    public class GoogleGeoService
    {
        /// <summary>
        /// Please replace this apikey with production key
        /// </summary>
        //private const string ApiKey1 = "AIzaSyDyJgyTkUUZpGAkr_sMKNvAe14pP7kHLhc";
        private const string ApiKey = "AIzaSyD1tZCgZfZVbpKZL2ceuCwl0AprGSQn4Oc";
        //private const string ApiKey = "AIzaSyCEcmhjJDqbd0-LT22yZHd0yFeqbjFNZjc";
        private XElement resultDocument = null;
        public GoogleGeoService(string addressPoint)
        {
            using (var client = new WebClient())
            {

                string seachurl = "https://maps.googleapis.com/maps/api/geocode/xml?address=" + addressPoint + "&key=" + ApiKey;
                //string[] geocodeInfo = client.DownloadString(seachurl).Split(',');
                //return (Convert.ToDouble(geocodeInfo[2]));
                var result = client.DownloadString(seachurl);
                resultDocument = XDocument.Parse(result).Descendants("result").First();
            }

            //using (var client = new HttpClient())
            //{

            //    string seachurl = "https://maps.googleapis.com/maps/api/geocode/xml?address=" + addressPoint + "&key=" + ApiKey;
            //    //string[] geocodeInfo = client.DownloadString(seachurl).Split(',');
            //    //return (Convert.ToDouble(geocodeInfo[2]));
            //    var result = client.GetStringAsync(seachurl).Result;
            //    //var result = client.DownloadString(seachurl);
            //    resultDocument = XDocument.Parse(result).Descendants("result").First();
            //}

        }
        public GoogleGeoService(double lat, double longi)
        {
            using (var client = new WebClient())
            {
                string seachurl = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat.ToString() + "," + longi.ToString() + "&key=" + ApiKey;
                //string[] geocodeInfo = client.DownloadString(seachurl).Split(',');
                //return (Convert.ToDouble(geocodeInfo[2]));
                var result = client.DownloadString(seachurl);
                resultDocument = XDocument.Parse(result).Descendants("result").First();
            }
        }
        /// <summary>
        /// returns latitude 
        /// </summary>
        /// <param name="addresspoint"></param>
        /// <returns></returns>
        public double? GetCoordinatesLat()
        {
            var value = resultDocument.Descendants("location").First().Element("lat").Value;
            return (Convert.ToDouble(value));
        }
        /// <summary>
        /// returns longitude 
        /// </summary>
        /// <param name="addresspoint"></param>
        /// <returns></returns>
        public double? GetCoordinatesLng()
        {
            var value = resultDocument.Descendants("location").First().Element("lng").Value;
            return (Convert.ToDouble(value));
        }
        public string GetCountry()
        {

            var value = resultDocument
                .Descendants("address_component").FirstOrDefault(ad => ad.Elements("type").Any(t => t.Value == "country"));
            if (value != null)
            {
                return value.Element("long_name").Value;
            }
            else
            {
                return "";
            }
        }
        public string GetCity()
        {
            var value = resultDocument
                .Descendants("address_component").FirstOrDefault(ad => ad.Elements("type").Any(t => t.Value == "locality")
                    && ad.Elements("type").Any(t => t.Value == "political"));
            if (value != null)
            {
                return value.Element("long_name").Value;
            }
            else
            {
                return "";
            }
        }
        public string GetState()
        {
            var value = resultDocument
                .Descendants("address_component").FirstOrDefault(ad => ad.Elements("type").Any(t => t.Value == "administrative_area_level_1")
                    && ad.Elements("type").Any(t => t.Value == "political"));
            if (value != null)
            {
                return value.Element("long_name").Value;
            }
            else
            {
                return "";
            }
        }
        public string GetPostcode()
        {
            var value = resultDocument
                .Descendants("address_component").FirstOrDefault(ad => ad.Elements("type").Any(t => t.Value == "postal_code"));
            if (value != null)
            {
                return value.Element("long_name").Value;
            }
            else
            {
                return "";
            }
        }
        public string GetPlaceId()
        {
            var value = resultDocument.Descendants("place_id").FirstOrDefault();
            if (value != null)
            {
                return value.Value;
            }
            else
            {
                return "";
            }
        }
        public string GetFormattedAddress()
        {
            var value = resultDocument.Descendants("formatted_address").FirstOrDefault();
            if (value != null)
            {
                return value.Value;
            }
            else
            {
                return "";
            }
        }
        public string GetStreetName()
        {
            var value = resultDocument
                .Descendants("address_component").FirstOrDefault(ad => ad.Elements("type").Any(t => t.Value == "route"));
            if (value != null)
            {
                return value.Element("long_name").Value;
            }
            else
            {
                return "";
            }
        }
        public string GetStreetNumber()
        {
            var value = resultDocument
                .Descendants("address_component").FirstOrDefault(ad => ad.Elements("type").Any(t => t.Value == "street_number"));
            if (value != null)
            {
                return value.Element("long_name").Value;
            }
            else
            {
                return "";
            }
        }
        public string GetUnitNumber()
        {
            var value = resultDocument
                .Descendants("address_component").FirstOrDefault(ad => ad.Elements("type").Any(t => t.Value == "subpremise"));
            if (value != null)
            {
                return value.Element("long_name").Value;
            }
            else
            {
                return "";
            }
        }

        static public double DistanceBetween(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }

    }
}
