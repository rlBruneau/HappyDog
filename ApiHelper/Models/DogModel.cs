using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace ApiHelper.Models
{
    public class DogModel
    {
        [JsonProperty("message")]
        public IList<string> Message { get; set; }
    }
}
