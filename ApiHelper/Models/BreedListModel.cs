using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace ApiHelper.Models
{
    public class BreedListModel
    {
        [JsonProperty("message")]
        public Message Message { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Message
    {
        IList<string> affenpinscher { get; set; }
    }
}
