using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeemSDK.Model
{
    public class Contact
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("mob_no")]
        public string MobNo { get; set; }

        [JsonProperty("mob_no2")]
        public string MobNo2 { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("fname")]
        public string Fname { get; set; }

        [JsonProperty("lname")]
        public string Lname { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birth_date")]
        public DateTimeOffset BirthDate { get; set; }

        [JsonProperty("area")]
        public string Area { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

    }
}
