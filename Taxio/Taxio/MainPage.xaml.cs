using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using SQLite;
using Taxio.Models;
using System.IO;
using System.Xml.Linq;
namespace Taxio
{
    public partial class MainPage : ContentPage
    {
        //DB CONNECTION
        private static DB database;

        public static DB Database
        {
            get
            {
                if(database == null)
                {
                    database = new DB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DataPlace.db3"));
                }
                return database;
                
            }

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var u = await MainPage.Database.GetUsers();
            USER_ID.Text = Convert.ToString("ID: " + u.FirstOrDefault().User_id);
            USERNAME.Text = Convert.ToString(u.FirstOrDefault().User_name);
        }

        //ADRESS
        private readonly List<string> _Adress = new List<string>
        {
            "Фрунзецкая,Д.12","М.Жукова,22","Васильева,4"
        };
        private void PosRN_Search_OnSearchButtonPressed(object sender, EventArgs e)
        {
            string keyword = PosRN_Search_AdressTo.Text;

            IEnumerable<string> searchResult =  _Adress.Where(adress => adress.ToLower().Contains(keyword.ToLower()));
        }
        private void PosRN_Search_OnSearchButtonPressed2(object sender, EventArgs e)
        {
            string keyword = PosRN_Search_AdressToGo.Text;

            IEnumerable<string> searchResult = _Adress.Where(adress => adress.ToLower().Contains(keyword.ToLower()));
        }
        //MENU
        private void FlyoutMenu_Click(object sender, EventArgs e)
        {
            menuLayout.IsVisible = !menuLayout.IsVisible;
        }
        public MainPage()
        {
            InitializeComponent();

        }
        //CALCULATIONS
        private double DistanceTimeMath(double distance)
        {
            double distanceto = distance;
            double kmtime = 1.50;
            double TimeToPass = distanceto * kmtime;
            return TimeToPass;
        }
        private double PriceCalculation(double distance, int km, double ratemultipler)
        {
            double distanceto = distance;
            double priceratemultipler = ratemultipler;
            int kmprice = km;
            double PriceMath = distanceto * kmprice * priceratemultipler;
            return PriceMath;
        }
        private async void RatePicker(int rate)
        {
            var c = await MainPage.Database.GetCarsV2();
            int ratematch = c.FindIndex(car => car.Car_Stat == Convert.ToString(rate));
            switch (rate)
            {
                case 1:
                    DriverNamenCar.Text = Convert.ToString(c.ElementAt(ratematch).Driver_name + " / " + c.ElementAt(ratematch).car_vendors);
                    Car_GosNum.Text = Convert.ToString(c.ElementAt(ratematch).Car_Gos);
                    break;
                case 2:
                    DriverNamenCar.Text = Convert.ToString(c.ElementAt(ratematch).Driver_name + " / " + c.ElementAt(ratematch).car_vendors);
                    Car_GosNum.Text = Convert.ToString(c.ElementAt(ratematch).Car_Gos);
                    break;
                case 3:
                    DriverNamenCar.Text = Convert.ToString(c.ElementAt(ratematch).Driver_name + " / " + c.ElementAt(ratematch).car_vendors);
                    Car_GosNum.Text = Convert.ToString(c.ElementAt(ratematch).Car_Gos);
                    break;
            }
            Console.WriteLine(ratematch);
        }
        //LOGIC
        private void EconomButton_Click(object sender, EventArgs e)
        {
            int rate = 1;
            RatePicker(rate);
            OrderGoButton.BackgroundColor = Color.DarkGreen;
            int km = 24;
            double ratemultipler = 1.22;
            double distance = 25;
            double PriceMath = PriceCalculation(distance, km, ratemultipler);
            double TimeToPass =  DistanceTimeMath(distance);
            TimeToTravel.Text = "";
            TimeToTravel.Text = Convert.ToString("В пути: " + distance + " Км" + " / " + TimeToPass + " Минут");
            OrderPrice.Text = "";
            OrderPrice.Text = Convert.ToString("Цена: " + PriceMath + " ₽");
            
        }
        private void BusinessButton_Click(object sender, EventArgs e)
        {
            int rate = 3;
            RatePicker(rate);
            OrderGoButton.BackgroundColor = Color.FromHex("#2D2DDF");
            int km = 66;
            double ratemultipler = 2.33;
            double distance = 35;
            double PriceMath = PriceCalculation(distance, km, ratemultipler);
            double TimeToPass = DistanceTimeMath(distance);
            TimeToTravel.Text = "";
            TimeToTravel.Text = Convert.ToString("В пути: " + distance + " Км" + " / " + TimeToPass + " Минут");
            OrderPrice.Text = "";
            OrderPrice.Text = Convert.ToString("Цена: " + PriceMath + " ₽");
        }
        private void ComfortButton_Click(object sender, EventArgs e)
        {
            int rate = 2;
            RatePicker(rate);          
            OrderGoButton.BackgroundColor = Color.FromHex("#FFBE8F56");
            int km = 32;
            double ratemultipler = 1.87;
            double distance = 42;
            double PriceMath = PriceCalculation(distance, km, ratemultipler);
            double TimeToPass = DistanceTimeMath(distance);
            TimeToTravel.Text = "";
            TimeToTravel.Text = Convert.ToString("В пути: " + distance + " Км" + " / " + TimeToPass + " Минут");
            OrderPrice.Text = "";
            OrderPrice.Text = Convert.ToString("Цена: " + PriceMath + " ₽");
        }
    }
}
