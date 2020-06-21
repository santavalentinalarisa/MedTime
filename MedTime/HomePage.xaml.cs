using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Threading.Tasks;
using MedTime.Models;

namespace MedTime
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            UrmatorulMedicament();
        }
        async void UrmatorulMedicament()
        {
            var medicaments = await App.Database.GetMedicationsAsync();
            var urmatoarea_ora = TimeSpan.MaxValue;
            foreach (Medicament medicament in medicaments)
            {
                if (medicament.DataStart <= DateTime.Today && medicament.Ora >= DateTime.Now.TimeOfDay)
                {
                    if (medicament.Ora < urmatoarea_ora)
                    {
                        urmatoarea_ora = medicament.Ora;
                    } 
                }
            }

            if (urmatoarea_ora != TimeSpan.MaxValue)
            {
                Device.StartTimer(new TimeSpan(0, 0, 1), () =>
                {   
                    urmator.Text = (urmatoarea_ora - DateTime.Now.TimeOfDay).ToString().Split('.')[0]; //stergere milisecunde
                    var timp_ramas = urmatoarea_ora - DateTime.Now.TimeOfDay;
                    if (timp_ramas.Hours == 0 && timp_ramas.Minutes == 0 && timp_ramas.Seconds == 0) // oprire countdown cand timpul ajunge la 0
                    {
                        return false;
                    }
                    urmator.FontSize = 24;
                    text.Text = "Următoarea administrare: ";
                    return true;
                });
            }
            else
            {
                urmator.Text = "Azi nu aveți nici un medicament de administrat!";
            }
        }
        async void AdaugareMedicament(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CRUDMedicamente
            {
                BindingContext = new Medicament()
            });
        }
    }
}
