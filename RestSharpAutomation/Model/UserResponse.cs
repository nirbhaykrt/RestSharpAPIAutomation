using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestSharpAutomation.Model
{
    public class UserData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";
        [JsonPropertyName("first_name")]
        public string First_name { get; set; } = "";
        [JsonPropertyName("last_name")]
        public string Last_name { get; set; } = "";
        [JsonPropertyName("avatar")]
        public string Avatar { get; set; } = "";
    }

    public class UserResponse
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("per_page")]
        public int Per_page { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
        [JsonPropertyName("total_pages")]
        public int Total_pages { get; set; }
        [JsonPropertyName("data")]
        public List<UserData>? Data { get; set; }
    }
}
