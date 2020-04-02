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
    public class GuestCatalogSingleton
    {
        private static GuestCatalogSingleton _instance = new GuestCatalogSingleton();

        public static  GuestCatalogSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GuestCatalogSingleton();
                }
                return _instance;
            }
        }

        public ObservableCollection<Guest> Guests { get; set; }

        private GuestCatalogSingleton()
        {
            Guests = new ObservableCollection<Guest>();
            Guest g1 = new Guest(17, "Mathias Man", "Vej 110");
            Guest g2 = new Guest(12, "Lydia Dame", "Vej 111");
            Guest g3 = new Guest(14, "Poul Henriksen", "Vej 112");
            Guests.Add(g1);
            Guests.Add(g2);
            Guests.Add(g3);
            LoadGuest();

        }

        private async void LoadGuest()
        {
            PersistenceGuest facade = new PersistenceGuest();
            List<Guest> guests = await facade.GetGuestsAsync();
            foreach (Guest guest in guests)
            {
                Guests.Add(guest);
            }
        }

        //Delete
        public void Delete(int index)
        {
            Guests.RemoveAt(index);
        }

        //Add
        public void Add(Guest guest)
        {
            Guests.Add(guest);
        }

    }
}
