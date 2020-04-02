using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HotelMVVM2020.Common;
using HotelMVVM2020.Perstistency;
using HotelMVVM2020.ViewModel;
using ModelLibrary;
using Newtonsoft.Json;

namespace HotelMVVM2020.Handler
{
    public class HotelHandler
    {
        public HotelViewModel HotelViewModel { get; set; }

        public HotelHandler(HotelViewModel hotelViewModel)
        {
            HotelViewModel = hotelViewModel;
        }


        public async void CreateHotel()
        {
            int hotelNo = HotelViewModel.NewHotel.Id;
            string hotelName = HotelViewModel.NewHotel.Name;
            string hotelAdress = HotelViewModel.NewHotel.Address;
            Hotel aHotel = new Hotel(hotelNo, hotelName, hotelAdress);
            PersistenceFacade facade = new PersistenceFacade();
            bool ok = await facade.PostHotelAsync(aHotel);

            if (!ok)
            {

                MessageDialogHelper.Show("Der skete en fejl", "Hotel blev ikke oprettet");

            }

            else
            {
                MessageDialogHelper.Show("Alt gik godt", $"Hotellet {aHotel.Name} blev oprettet");
                HotelViewModel.HotelCatalogSingleton.Hotels.Clear();
                HotelViewModel.HotelCatalogSingleton.LoadHotels();
            }
        }

        public async void DeleteHotel()
        {
            int hotelNo = HotelViewModel.NewHotel.Id;
            PersistenceFacade facade = new PersistenceFacade();
            bool ok = await facade.DeleteHotelAsync(hotelNo);

            if (!ok)
            {

                MessageDialogHelper.Show("Der skete en fejl", "Hotel blev ikke slettet");

            }

            else
            {
                MessageDialogHelper.Show("Alt gik godt", $"Hotellet {hotelNo} blev slettet");
                HotelViewModel.HotelCatalogSingleton.Hotels.Clear();
                HotelViewModel.HotelCatalogSingleton.LoadHotels();
            }
        }

        public async void UpdateHotel()
        {
            int hotelNo = HotelViewModel.NewHotel.Id;
            PersistenceFacade facade = new PersistenceFacade();
            bool ok = await facade.PutHotelAsync (hotelNo, HotelViewModel.NewHotel);

            if (!ok)
            {

                MessageDialogHelper.Show("Der skete en fejl", "Hotel blev ikke opdateret");

            }

            else
            {
                MessageDialogHelper.Show("Alt gik godt", $"Hotellet {hotelNo} blev Updateret");
                HotelViewModel.HotelCatalogSingleton.Hotels.Clear();
                HotelViewModel.HotelCatalogSingleton.LoadHotels();
            }
        }

    }

   
}
