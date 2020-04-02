using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using Newtonsoft.Json;

namespace HotelMVVM2020.Perstistency
{
    public class PersistenceGuest
    {
        private const string URI = "http://localhost:50328/API/Guest";
        public async Task<List<Guest>> GetGuestsAsync()
        {
            List<Guest> gæster = new List<Guest>();
            using (HttpClient client = new HttpClient())
            {
                string stringAsync = await client.GetStringAsync(URI);
                gæster = JsonConvert.DeserializeObject<List<Guest>>(stringAsync);
            }

            return gæster;
        }


        public async Task<bool> PostGuestAsync(Guest guest)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(guest);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage postAsync = await client.PostAsync(URI, content);
                if (postAsync.IsSuccessStatusCode)
                {
                    string jsonStr = postAsync.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }
            }
            return ok;
        }

        public async Task<bool> DeleteGuestAsync(int id)
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

        public async Task<bool> PutGuestAsync(int id, Guest guest)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(guest);
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
