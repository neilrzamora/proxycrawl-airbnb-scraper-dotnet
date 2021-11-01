using System;

using Newtonsoft.Json;

namespace ConsoleApp
{
    class Program
    {
        #region Inner Classes

        public class AirBnbScraperResult
        {
            public AirBnbResident[] Residents { get; set; }
        }

        public class AirBnbResident
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("superHost")]
            public bool? SuperHost { get; set; }

            [JsonProperty("residentType")]
            public string ResidentType { get; set; }

            [JsonProperty("location")]
            public string Location { get; set; }

            [JsonProperty("samplePhotoUrl")]
            public string SamplePhotoUrl { get; set; }

            [JsonProperty("rating")]
            public decimal? Rating { get; set; }

            [JsonProperty("personReviewed")]
            public int? PersonReviewed { get; set; }

            [JsonProperty("accommodation")]
            public AirBnbResidentAccommodation Accommodation { get; set; }

            [JsonProperty("amenities")]
            public string[] Amenities { get; set; }

            [JsonProperty("costs")]
            public AirBnbResidentCost Costs { get; set; }
        }

        public class AirBnbResidentAccommodation
        {
            [JsonProperty("guests")]
            public string Guests { get; set; }

            [JsonProperty("bedrooms")]
            public string Bedrooms { get; set; }

            [JsonProperty("beds")]
            public string Beds { get; set; }

            [JsonProperty("baths")]
            public string Baths { get; set; }
        }

        public class AirBnbResidentCost
        {
            [JsonProperty("priceCurrency")]
            public string PriceCurrency { get; set; }

            [JsonProperty("pricePerNight")]
            public string PricePerNight { get; set; }
        }

        #endregion

        static void Main(string[] args)
        {
            var api = new ProxyCrawl.ScraperAPI("YOUR_PROXYCRAWL_TOKEN_HERE");
            api.Get("https://www.airbnb.com/s/Beirut/homes");

            AirBnbScraperResult results = JsonConvert.DeserializeObject<AirBnbScraperResult>(api.Body);
            foreach (var resident in results.Residents)
            {
                if (resident.SuperHost.HasValue && resident.SuperHost.Value)
                {
                    Console.WriteLine("{0} <SuperHost>", resident.Title);
                }
                else
                {
                    Console.WriteLine(resident.Title);
                }
                Console.WriteLine("Type: {0}", resident.ResidentType);
                Console.WriteLine("Location: {0}", resident.Location);
                Console.WriteLine("Photo: {0}", resident.SamplePhotoUrl);
                Console.WriteLine("Rating: {0}", resident.Rating);
                Console.WriteLine("Reviewers count: {0}", resident.PersonReviewed);
                if (resident.Amenities != null && resident.Amenities.Length > 0)
                {
                    Console.WriteLine("Amenities: {0}", string.Join(", ", resident.Amenities));
                }
                if (resident.Accommodation != null)
                {
                    Console.WriteLine("Amenities");
                    Console.WriteLine("  * Bedrooms: {0}", resident.Accommodation.Bedrooms);
                    Console.WriteLine("  *     Beds: {0}", resident.Accommodation.Beds);
                    Console.WriteLine("  *    Baths: {0}", resident.Accommodation.Baths);
                    Console.WriteLine("  *   Guests: {0}", resident.Accommodation.Guests);
                }
                if (resident.Costs != null)
                {
                    Console.WriteLine("Costs");
                    Console.WriteLine("  * Currency: {0}", resident.Costs.PriceCurrency);
                    Console.WriteLine("  *Per Night: {0}", resident.Costs.PricePerNight);
                }

                Console.WriteLine();
            }
        }
    }
}
