using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Threading.Tasks;
using MedTime.Models;
using FFImageLoading;

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
                    urmator.FontFamily = "Lobster-Regular";
                    urmator.FontSize = 24;
                    return true;
                });
            }
            else
            {
                urmator.Text = "Nu mai avem nici un medicament de luat";
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
