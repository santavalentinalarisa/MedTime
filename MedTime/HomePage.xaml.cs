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

            // var urmatorul_medicament = medicaments.OrderBy(medicament => medicament.DataStart).ThenBy(medicament => medicament.Ora).First();

            var diferenta_ore_min = TimeSpan.MaxValue;
            TimeSpan urmatoarea_ora;
            foreach (Medicament medicament in medicaments)
            {
                var ora_medicament = medicament.Ora;
                if (medicament.Ora < DateTime.Now.TimeOfDay)
                {
                    ora_medicament += TimeSpan.FromHours(24);
                }

                var diferenta_ore = ora_medicament - DateTime.Now.TimeOfDay;

                if (diferenta_ore < diferenta_ore_min)
                {
                    diferenta_ore_min = diferenta_ore;
                    urmatoarea_ora = ora_medicament;
                }
            }

            if (diferenta_ore_min != TimeSpan.MaxValue)
            {
                Device.StartTimer(new TimeSpan(0, 0, 1), () =>
                {
                  
                    urmator.Text = (urmatoarea_ora - DateTime.Now.TimeOfDay).ToString().Split('.')[0]; //stergere milisecunde
                    var timp_ramas = urmatoarea_ora - DateTime.Now.TimeOfDay;
                    if (timp_ramas.Hours == 0 && timp_ramas.Minutes == 0 && timp_ramas.Seconds == 1) // reapelare functie cand cronometrul ajunge la 0
                    {
                        UrmatorulMedicament();
                    }
                    urmator.FontSize = 24;
                    text.Text = "Următoarea alarmă: ";
                    return true;
                });
            }
            else
            {
                urmator.Text = "Nu aveți nici un medicament de administrat!";
                
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
