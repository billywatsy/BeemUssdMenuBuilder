using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeemSDK.Model
{
    public class Pagination
    {
        [JsonProperty("totalItems")]
        public long TotalItems { get; set; }

        [JsonProperty("currentPage")]
        public long CurrentPage { get; set; }

        [JsonProperty("pageSize")]
        public long PageSize { get; set; }

        [JsonProperty("totalPages")]
        public long TotalPages { get; set; }

        [JsonProperty("startPage")]
        public long StartPage { get; set; }

        [JsonProperty("endPage")]
        public long EndPage { get; set; }

        [JsonProperty("startIndex")]
        public long StartIndex { get; set; }

        [JsonProperty("endIndex")]
        public long EndIndex { get; set; }

        [JsonProperty("pages")]
        public long[] Pages { get; set; }
    }
}
