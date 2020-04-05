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
