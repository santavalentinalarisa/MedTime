using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MedTime.MenuItems;


namespace MedTime
{
   
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }
        public MainPage()
        {
            InitializeComponent();
            menuList = new List<MasterPageItem>();
            menuList.Add(new MasterPageItem() { Title = "Acasă", Icon = "home.png", TargetType = typeof(HomePage) });
            menuList.Add(new MasterPageItem() { Title = "Listă medicamente", Icon = "icon1.png", TargetType = typeof(ListaMedicamente) });
            menuList.Add(new MasterPageItem() { Title = "Găsește farmacii", Icon = "icon2.png", TargetType = typeof(GasesteFarmacii) });
        
          
            navigationDrawerList.ItemsSource = menuList;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
         
        }
     
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}
