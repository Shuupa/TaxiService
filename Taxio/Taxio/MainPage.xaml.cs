using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Taxio
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ShowUsers();
        }

        private void ShowUsers()
        {
            UserCollection.ItemsSource = App.Db.GetUsers();
        }
        private void GetVendors()
        {
            VendorCollection.ItemsSource = App.Db.GetCar_Vendors();
        }


        private async void AddItemButton_User(object sender, EventArgs e)
        {
            string Namefield = NameField.Text.Trim();
            int UserId_Field = Convert.ToInt32(User_idField.Text.Trim());
            if (Namefield.Length < 4)
            {
                await DisplayAlert("Error", "Name < 4 digits", "Ok");
                return;
            }
            if (UserId_Field < 1)
            {
                await DisplayAlert("Error", "ID < 1", "Ok");
                return;
            }
            User user = new User
            {   
                User_id = UserId_Field,
                User_name = Namefield, 
            };
            App.Db.SaveUser(user);
            ShowUsers();
            Namefield = "";
            UserId_Field = 0;
        }
        private async void AddItemButton_Vendor(object sender, EventArgs e)
        {
            string VendorField = Car_VendorField.Text.Trim();
            if (VendorField.Length < 1)
            {
                await DisplayAlert("Error", "Car vendor error", "Ok");
            }
            Car_vendor car_Vendor = new Car_vendor
            {
                Car_Vendors = VendorField,
            };
            App.Db.SaveVendor(car_Vendor);
            GetVendors();
            VendorField = "";
        }
    }
}
