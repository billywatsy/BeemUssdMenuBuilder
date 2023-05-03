using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeemSDK.Model
{
    public class CommonResponse
    {
        [JsonProperty("data")]
        public CommonResponseMessage CommonResponseMessage { get; set; }
    }
}
