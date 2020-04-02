using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelMVVM2020.Common;
using HotelMVVM2020.Model;
using HotelMVVM2020.Perstistency;
using HotelMVVM2020.ViewModel;
using ModelLibrary;

namespace HotelMVVM2020.Handler
{
    public class GuestHandler
    {
        private GuestViewModel guestViewModel;

        public GuestHandler(GuestViewModel guestViewModel)
        {
            this.guestViewModel = guestViewModel;
            
        }


        private Guest CopyGuest(Guest originalGuest)
        {
            Guest copy = new Guest();
            copy.Id = originalGuest.Id;
            copy.Name = originalGuest.Name;
            copy.Address = originalGuest.Address;
            return copy;
        }
        public async void CreateGuest()
        {
            Guest copyGuest = CopyGuest(guestViewModel.NewGuest);
            guestViewModel.GuestCatalogSingleton.Add(copyGuest);

            PersistenceGuest createGuest = new PersistenceGuest();
            await createGuest.PostGuestAsync(copyGuest);

        }


        public async void DeleteGuest()
        {
            var gæster = guestViewModel.GuestCatalogSingleton.Guests;
            for (int i = 0; i < gæster.Count; i++)
            {
                if (gæster[i].Id == guestViewModel.NewGuest.Id)
                {
                    Guest guest = guestViewModel.NewGuest;
                    guestViewModel.GuestCatalogSingleton.Delete(i);
                    PersistenceGuest deleteGuest = new PersistenceGuest();
                    await deleteGuest.DeleteGuestAsync(guest.Id);
                    return;
                }
            }

        }

        public async void UpdateGuest()
        {
            var gæster = guestViewModel.GuestCatalogSingleton.Guests;
            for (int i = 0; i < gæster.Count; i++)
            {
                if (gæster[i].Id == guestViewModel.NewGuest.Id)
                {
                    Guest copyGuest = CopyGuest(guestViewModel.NewGuest);
                    guestViewModel.GuestCatalogSingleton.Delete(i);
                    guestViewModel.GuestCatalogSingleton.Add(copyGuest);
                    PersistenceGuest deleteGuest = new PersistenceGuest();
                    await deleteGuest.PutGuestAsync(copyGuest.Id, copyGuest);
                    return;
                }
            }
        }

        public void ClearGuest()
        {
            guestViewModel.NewGuest = new Guest();


        }

    }
}
