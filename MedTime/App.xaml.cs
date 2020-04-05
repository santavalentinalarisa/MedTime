using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MedTime.Data;
using System.IO;

namespace MedTime
{
    public partial class App : Application
    {
        static DataMedicament database;
        public static DataMedicament Database
        {
            get
            {
                if (database == null)
                {
                    database = new
                   DataMedicament(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
                   LocalApplicationData), "Medication.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
