using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using Newtonsoft.Json;

namespace HotelMVVM2020.Perstistency
{
    /// <summary>
    /// En klasse der Har forbindelse til programmet HotelRestAPI ved at bruge dens link,
    /// samt Indeholde den HotelRestAPIs metoder.
    /// </summary>
    public class PersistenceFacade
    {
        private const string URI = "http://localhost:50328/API/Hotels";


        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (HttpClient client = new HttpClient())
            {
                string jsonString = await client.GetStringAsync(URI);
                hoteller = JsonConvert.DeserializeObject<List<Hotel>>(jsonString);
            }

            return hoteller;
        }

        public async Task<Hotel> GetOneHotelsAsync(int id)
        {
            Hotel hotel = new Hotel();
            using (HttpClient client = new HttpClient())
            {
                string jsonString = await client.GetStringAsync(URI + "/" + id);
                hotel = JsonConvert.DeserializeObject<Hotel>(jsonString);
            }

            return hotel;
        }

        public async Task<bool> PostHotelAsync(Hotel hotel)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(hotel);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "Application/json");
                HttpResponseMessage postAsync = await client.PostAsync(URI, content);

                if (postAsync.IsSuccessStatusCode)
                {
                    string jsonstr = postAsync.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonstr);
                }
                else
                {
                    ok = false;
                }
            }

            return ok;
        }

        public async Task<bool> DeleteHotelAsync(int id)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage deleteAsync = await client.DeleteAsync(URI + "/" + id);
                if (deleteAsync.IsSuccessStatusCode)
                {
                    string jsonStr = deleteAsync.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }
            }
            return ok;
        }

        public async Task<bool> PutHotelAsync(int id, Hotel hotel)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(hotel);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "Application/json");
                HttpResponseMessage putAsync = await client.PutAsync(URI + "/" + id, content);

                if (putAsync.IsSuccessStatusCode)
                {
                    string jsonstr = putAsync.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonstr);
                }
                else
                {
                    ok = false;
                }
            }

            return ok;
        }
    }
}
