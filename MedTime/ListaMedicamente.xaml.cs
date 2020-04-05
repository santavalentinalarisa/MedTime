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
            listView.ItemsSource = await App.Database.GetMedicationsAsync();
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
