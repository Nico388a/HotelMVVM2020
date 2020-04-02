using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelMVVM2020.Perstistency;
using ModelLibrary;

namespace HotelMVVM2020.Model
{
    /// <summary>
    /// Singleton klasse af Hotel som kun HotelCatalogSingleton har adgang til
    /// </summary>
    public class HotelCatalogSingleton
    {
        private static HotelCatalogSingleton _instance = new HotelCatalogSingleton();//eager = ivrig

        //Constructor
        public static HotelCatalogSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                  _instance = new HotelCatalogSingleton(); //Lazy  
                }
                return _instance;
            }
        }
        /// <summary>
        /// Observerer når Hotel collections ændre sig og bliver added
        /// </summary>
        public ObservableCollection<Hotel> Hotels { get; set; }

        private HotelCatalogSingleton()
        {
           Hotels = new ObservableCollection<Hotel>();
           Hotel h1 = new Hotel(456, "Hotel 1", "Vej 466");
           Hotel h2 = new Hotel(123, "Hotel 2", "Vej 123");
           Hotel h3 = new Hotel(321, "Hotel 3", "Vej 321");
           Hotels.Add(h1);
           Hotels.Add(h2);
           Hotels.Add(h3);
            LoadHotels();
        }

        public async void LoadHotels()
        {
            PersistenceFacade facade = new PersistenceFacade();
            List<Hotel> hotels = await facade.GetAllHotelsAsync();
            foreach (Hotel hotel in hotels)
            {
                Hotels.Add(hotel);
            }
        }


    }
}
