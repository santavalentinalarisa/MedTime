using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using MedTime.Models;

namespace MedTime
{
    public partial class ListaMedicamente : ContentPage
    {
        public ListaMedicamente()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasBackButton(this, false); //stergere buton back de la lista medicamente

            listView.ItemsSource = await App.Database.GetMedicationsAsync();
            
            foreach (Medicament medicament in listView.ItemsSource) //stergere medicament cu data final trecuta
            {
                if (medicament.DataFinal < DateTime.Now.Date)
                {
                    await App.Database.DeleteMedicationAsync(medicament);
                }
            }
        }
        async void AddMedication(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CRUDMedicamente
            {
                BindingContext = new Medicament()
            });
        }
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new CRUDMedicamente
                {
                    BindingContext = e.SelectedItem as Medicament
                });
            }
        }
    }
}
