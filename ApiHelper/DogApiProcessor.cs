using ApiHelper.Models;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiHelper
{
    public class DogApiProcessor
    {

        public static async Task<List<string>> LoadBreedList()
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://dog.ceo/api/breeds/list/all");
            string res = await response.Content.ReadAsStringAsync();
            JObject jobj = JObject.Parse(res);
            JObject message = (JObject)jobj["message"];
            List<string> breedList = message.Properties().Select(p=>p.Name).ToList();

            return breedList;
        }

        public static async Task<IList<string>> GetImageUrl(string breed)
        {
            /// TODO : GetImageUrl()
            /// TODO : Compléter le modèle manquant

            string endPoint = $"https://dog.ceo/api/breed/{breed}/images";
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(endPoint);

            string res = await response.Content.ReadAsStringAsync();

            DogModel dogs = JsonConvert.DeserializeObject<DogModel>(res);

            return dogs.Message;
        }
    }
}
