using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestSharpAutomation.Model
{
    public class SingleResourceResponse
    {
        [JsonPropertyName("data")]
        public Resource? Data { get; set; }
    }
}
