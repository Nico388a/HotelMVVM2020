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
    
    public class HotelViewModel: INotifyPropertyChanged
    {
        
    public HotelCatalogSingleton HotelCatalogSingleton { get; set; }

    //Property af HotelHandler
    public HotelHandler HotelHandler { get; set; }
        

        /// <summary>
        /// Constructor af Hotel og initialiserer Commandoer
        /// </summary>
    public HotelViewModel()
    {
        HotelCatalogSingleton = HotelCatalogSingleton.Instance;
        HotelHandler = new HotelHandler(this);
        _newHotel = new Hotel();
        //Create
        CreateHotelCommand = new RelayCommand(HotelHandler.CreateHotel);
        //Delete
        _deleteHotelCommand= new RelayCommand(HotelHandler.DeleteHotel, SelectedIndexIsSet);
        //Update
        _updateHotelCommand = new RelayCommand(HotelHandler.UpdateHotel, SelectedIndexIsSet);
    }


     //Instancefields og properties


     //NewHotel
     private Hotel _newHotel;

    public Hotel NewHotel
    {
        get { return _newHotel; }
        set
        {
            _newHotel = value;
            OnPropertyChanged();
        }
    }

       
    //Func

    /// <summary>
    /// Viser når metoden forandre sig
    /// </summary>
    private int _selectedIndex;
    public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
                ((RelayCommand)_deleteHotelCommand).RaiseCanExecuteChanged();
            }
        }

        private Hotel _selectedHotel;

        public Hotel SelectedHotel
        {
            get { return _selectedHotel; }
            set {  _selectedHotel = value;
                if (value == null)
                    _selectedHotel = new Hotel();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Instance af: Sletter Hoteller
        /// </summary>
        private ICommand _deleteHotelCommand;

        /// <summary>
        /// Instance af: Skaber HotelObejkter
        /// </summary>
        public ICommand CreateHotelCommand { get; }

        /// <summary>
        /// Property Command der Sletter Hoteller
        /// </summary>
        public ICommand DeleteHotelCommand 
        {
            get { return _deleteHotelCommand; }
            set { _deleteHotelCommand = value; }
        }

        private ICommand _updateHotelCommand;
        /// <summary>
        /// Property Commando der opdaterer hoteller
        /// </summary>
        public  ICommand UpdateHotelCommand 
        { 
          get { return _updateHotelCommand;} 
          set {_updateHotelCommand = value ;} 
        
        }


        /// <summary>
        /// Metode der sørger for man ikke sletter et hotel, før man har klikket på det valgte object.
        /// </summary>
        /// <returns>Slet Hotel</returns>
        public bool SelectedIndexIsSet()
        {
            return SelectedIndex != -1;
        }



        //Denne metode notificerer properties når de ændres.
        public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    }
}
