using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeemSDK.Model
{
    public class Address
    {
        [JsonProperty("addressbook")]
        public string Addressbook { get; set; }

        [JsonProperty("contacts_count")]
        public long ContactsCount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

    }
}
