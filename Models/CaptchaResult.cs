using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace online_education_site.Models
{
    public class CaptchaResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}