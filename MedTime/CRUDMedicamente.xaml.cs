using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using MedTime.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;
using System.ComponentModel;

namespace MedTime
{
    public partial class CRUDMedicamente : ContentPage
    {
    
        DateTime _time;
        public CRUDMedicamente(){
            InitializeComponent();
           
        }
        
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var medicament = (Medicament)BindingContext;
               
            if (medicament.DataStart < DateTime.Today) 
            {
                medicament.DataStart = DateTime.Today;
            }
            if (medicament.DataFinal < DateTime.Today)
            {
                medicament.DataFinal = DateTime.Today;
            }

            SetTriggerTime();
            Device.StartTimer(TimeSpan.FromSeconds(1), () => OnTimerTick(medicament.Nume, medicament.Doza, medicament.DataStart, medicament.DataFinal));
            
            await App.Database.SaveMedicationAsync(medicament);
            await Navigation.PushAsync(new ListaMedicamente()); // navigare spre pagina listaMedicamente

        } 
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var medicament = (Medicament)BindingContext;
            await App.Database.DeleteMedicationAsync(medicament);
            await Navigation.PopAsync();
        }
        bool OnTimerTick(String nume, String doza, DateTime dataStart, DateTime dataFinal)
        {
            if (_switch.IsToggled && DateTime.Now >= _time && dataStart <= DateTime.Today && DateTime.Today <= dataFinal)
            {
                _time += TimeSpan.FromDays(1);
                var titlu = "MedTime";
                var mesaj = "E timpul să administrezi " + nume + ", " + doza + "!";

                CrossLocalNotifications.Current.Show(titlu, mesaj);
            }

            return true;
        }

        void TimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {

            if (args.PropertyName == "Time")
            {
                SetTriggerTime();

            }

        }

        void SetTriggerTime()
        {
            _time = DateTime.Today + tp.Time;

            if (_time < DateTime.Now)
            {
                _time += TimeSpan.FromDays(1);
            }
        }
       
        
    }
}


