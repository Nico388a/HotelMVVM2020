using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HotelMVVM2020.Annotations;
using HotelMVVM2020.Common;
using HotelMVVM2020.Handler;
using HotelMVVM2020.Model;
using ModelLibrary;

namespace HotelMVVM2020.ViewModel
{
    /// <summary>
    /// en connection mellem model-Guest og Guestpage, hvor man bruger modellen til viewmodellen, og viser metoderne gennem pagen
    /// </summary>
    public class GuestViewModel: INotifyPropertyChanged
    {
        public GuestCatalogSingleton GuestCatalogSingleton { get; set; }
        
        //Constructor
        public GuestViewModel()
        {
            GuestCatalogSingleton = GuestCatalogSingleton.Instance;
            _newGuest = new Guest();
            GuestHandler = new GuestHandler(this);
            _createCommand = new RelayCommand(GuestHandler.CreateGuest);
            _deleteCommand = new RelayCommand(GuestHandler.DeleteGuest, SelectedIndexIsNotSet);
            _updateCommand = new RelayCommand(GuestHandler.UpdateGuest, SelectedIndexIsNotSet);
            _clearCommand = new RelayCommand(GuestHandler.ClearGuest);
        }

        //Property
        public Guest SelectedGuest
        {
            set
            {
                if (value != null)
                {
                    _newGuest.Id = value.Id;
                    _newGuest.Name = value.Name;
                    _newGuest.Address = value.Address;
                    OnPropertyChanged(nameof(NewGuest));
                }
                else
                {
                    NewGuest = new Guest();
                }
               
            }
        }

        private Guest _newGuest;
        public Guest NewGuest
        {
            get { return _newGuest; }
            set
            {
                _newGuest = value; 
                OnPropertyChanged();
            }
        }


        public GuestHandler GuestHandler { get; set; }


        //Add
        private RelayCommand _createCommand;

        public ICommand CreateCommand
        {
            get { return _createCommand; }
        }

        //Delete
        private RelayCommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
        }

        //Update
        private RelayCommand _updateCommand;

        public ICommand UpdateCommand
        {
            get { return _updateCommand; }
        }

        //Clear
        private RelayCommand _clearCommand;

        public ICommand ClearCommand
        {
            get { return _clearCommand; }
        }

        /// <summary>
        /// Faiter Delete knappen når den ikke har klikket på nogen gæst
        /// </summary>
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
                ((RelayCommand)_deleteCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Metode der sørger for man ikke sletter et hotel, før man har klikket på det valgte object.
        /// </summary>
        /// <returns>Slet Guest</returns>
        public bool SelectedIndexIsNotSet()
        {
            return SelectedIndex != -1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
