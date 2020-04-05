using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using MedTime.Models;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedTime
{
    public partial class CRUDMedicamente : ContentPage
    {
        public CRUDMedicamente(){
        
               InitializeComponent();
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var medicament = (Medicament)BindingContext;
            //medicament.Ora = tp.Time;
            //medicament.MinimumDate = DateTime.Today;
            //medicament.DataStart =dps.Date;
            await App.Database.SaveMedicationAsync(medicament);
            await Navigation.PopAsync();

            
          
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var medicament = (Medicament)BindingContext;
            await App.Database.DeleteMedicationAsync(medicament);
            await Navigation.PopAsync();
        }
    }

}
