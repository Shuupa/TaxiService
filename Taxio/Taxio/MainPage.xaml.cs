using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Maps;
using System.Drawing;
namespace Taxio
{
    public partial class MainPage : ContentPage
    {
        //Theme
        //DB CONNECTION
        private static DB database;

        public static DB Database
        {
            get
            {
                if (database == null)
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
            var order = await MainPage.Database.GetOrderDAT();
            RightsPolicy.Text = "Copyright ©. All rights reserved";
        }

        //ADRESS
        private readonly List<string> _Adress = new List<string>
        {
            "Фрунзецкая,Д.12","М.Жукова,22","Васильева,4"
        };
        private void PosRN_Search_OnSearchButtonPressed(object sender, EventArgs e)
        {
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
            Menuframe.IsVisible = !Menuframe.IsVisible;
        }

        public MainPage()
        {
            InitializeComponent();
        }
        //CALCULATIONS

        // This method contains 2 adress and calculate distance for A to B
        private double GetDistanceATOB()
        {
            double distance = 25;
            return distance;
        }
        // This calculate time to travel
        private double DistanceTimeMath()
        {
            double distance = GetDistanceATOB();
            double kmtime = 1.50;
            double TimeToPass = distance * kmtime;
            return TimeToPass;
        }
        // This shit calculate price XD
        private double PriceCalculation(int rate)
        {
            double distance = GetDistanceATOB();
            double ratemultipler = 0;
            double km = 0;
            switch (rate)
            {
                case 1:
                    km = 24;
                    ratemultipler = 1.22;
                    break;
                case 2:
                    km = 32;
                    ratemultipler = 1.87;
                    break;
                case 3:
                    km = 66;
                    ratemultipler = 2.33;
                    break;
            }
            double PriceMath = distance * km * ratemultipler;
            return PriceMath;
        }
        // Method returns Driver name, car name and gos num
        private async void _RatePicker(int rate)
        {
            var c = await MainPage.Database.GetCarsV3();
            int carindex = 0;
            var list_indexstore = new List<int>();
            int len = list_indexstore.Count();
            var indexfinder = c.Where(car => car.Car_Stat == Convert.ToString(rate)).ToList();
            foreach (var index in indexfinder)
            {
                int _INDEX = Convert.ToInt32(index.ID);
                list_indexstore.Add(_INDEX);
            }
            Random rand = new Random();
            int randomIndex = rand.Next(list_indexstore.Min(), list_indexstore.Max());
            Console.WriteLine(randomIndex);
            carindex = randomIndex;
            list_indexstore = null;
            DriverNamenCar.Text = Convert.ToString(c.ElementAt(carindex).Driver_name + " / " + c.ElementAt(carindex).car_vendors);
            Car_GosNum.Text = Convert.ToString(c.ElementAt(carindex).Car_Gos);
            carindex = 0;
            indexfinder = null;   
        }

        // Method take several methods to write distance, time to travel and price
        private void _ORDER_OPTION(int rate)
        {
            TimeToTravel.Text = "";
            TimeToTravel.Text = Convert.ToString("В пути: " + GetDistanceATOB() + " Км" + " / " + DistanceTimeMath() + " Минут");
            OrderPrice.Text = "";
            OrderPrice.Text = Convert.ToString("Цена: " + PriceCalculation(rate) + " ₽");
        }
        //LOGIC
        private int OrderClass;
        private void EconomButton_Click(object sender, EventArgs e)
        {
            int rate = 1;
            _RatePicker(rate);
            _ORDER_OPTION(rate);
            OrderClass = 1;
        }
        private void BusinessButton_Click(object sender, EventArgs e)
        {
            int rate = 3;
            _RatePicker(rate);
            _ORDER_OPTION(rate);
            OrderClass = 1;
        }
        private void ComfortButton_Click(object sender, EventArgs e)
        {
            int rate = 2;
            _RatePicker(rate);
            _ORDER_OPTION(rate);
            OrderClass = 1;
        }
        private async void OrderConfirm_Click(object sender, EventArgs e)
        {
            double distance = GetDistanceATOB();
            string OrderClassTXT = "";
            var order = await MainPage.database.GetOrderDAT();
            string Adress_A = PosRN_Search_AdressTo.Text + ".";
            string Adress_B = PosRN_Search_AdressTo.Text + ".";
            int Id = Convert.ToInt32(order.Last().ID) + 1;
            switch (OrderClass)
            {
                case 1:
                    OrderClassTXT = "Эконом";
                    break;
                case 2:
                    OrderClassTXT = "Комфорт";
                    break;
                case 3:
                    OrderClassTXT = "Бизнес";
                    break;
            }
            if (Adress_A.Length < 2 && Adress_B.Length < 2)
            {
                await DisplayAlert("Ошибка", "Не все данные введены.", "Продолжить");
            }
            else
            {    
                new OrderData {ID = Id, Adress_A_B = $"{Adress_A} / {Adress_B}", OrderClass = OrderClassTXT, Distance = TimeToTravel.Text, Price = OrderPrice.Text, Gos_Car = Car_GosNum.Text, Driver_Name_Car = DriverNamenCar.Text};
                
            }
        }
    }
}
