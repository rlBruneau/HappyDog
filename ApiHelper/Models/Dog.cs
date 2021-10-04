using System;
using System.Collections.Generic;
using System.Text;

namespace ApiHelper.Models
{
    public class Dog
    {
        public string url { get; set; }

        public Dog(string url)
        {
            this.url = url;
        }
    }
}
