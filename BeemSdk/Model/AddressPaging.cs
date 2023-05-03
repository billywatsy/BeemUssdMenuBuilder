using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeemSDK.Model
{
    public class AddressPaging<T>
    {
        [JsonProperty("data")]
        public T[] Data { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}
