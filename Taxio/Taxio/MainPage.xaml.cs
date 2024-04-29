using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Maps;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using SQLite;
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
            LastOrderWriter();
        }

        //ADRESS
        private readonly List<string> _Adress = new List<string>
        {
            "Ленина,Д.1","Ленина,Д.2","Ленина,Д.3","Ленина,Д.4","Ленина,Д.5","Ленина,Д.6","Ленина,Д.7","Ленина,Д.8","Ленина,Д.9","Ленина,Д.10","Дом советов,10"
        };
        private string Adresss1;
        private string Adresss2;
        private double distance;
        private void AdressDistance()
        {
            int[,] distances = new int[,]
            {
                {0,100,200,300,400,500,600,700,800,900,1000}, // Расстояние от Ленина.Д.1 до всех адресов
                {100,0,100,200,300,400,500,600,700,800,900 }, // Расстояние от Ленина.Д.2 до всех адресов
                {200,100,0,100,200,300,400,500,600,700,800 }, // Расстояние от Ленина.Д.3 до всех адресов
                {300,200,100,0,100,200,300,400,500,600,700}, // Расстояние от Ленина.Д.4 до всех адресов
                {400,300,200,100,0,100,200,300,400,500,600}, // Расстояние от Ленина.Д.5 до всех адресов
                {500,400,300,200,100,0,100,200,300,400,500}, // Расстояние от Ленина.Д.6 до всех адресов
                {600,500,400,300,200,100,0,100,200,300,400 }, // Расстояние от Ленина.Д.7 до всех адресов
                {700,600,500,400,300,200,100,0,100,200,300 }, // Расстояние от Ленина.Д.8 до всех адресов
                {800,700,600,500,400,300,200,100,0,100,200 }, // Расстояние от Ленина.Д.9 до всех адресов
                {900,800,700,600,500,400,300,200,100,0,100}, // Расстояние от Ленина.Д.10 до всех адресов
                {1000,900,800,700,600,500,400,300,200,100,0}, // Расстояние от Дом советов.10 до всех адресов
            };
            int index1 = _Adress.IndexOf(Adresss1);
            int index2 = _Adress.IndexOf(Adresss2);
            if (index1 != -1 && index2 != -1)
            {
                // Получаем расстояние между выбранными адресами из массива distances
                distance = distances[index1, index2];

                // Выводим расстояние в консоль
                Console.WriteLine($"Расстояние между адресами {Adresss1} и {Adresss2}: {distance} м.");
                checkerSost();
            }
            else
            {
                checkerSost();
                Console.WriteLine("Ошибка: Один из выбранных адресов отсутствует в массиве расстояний.");
            }
        }
        private string getadress1(string selectedAdress)
        {
            Adresss1 = selectedAdress;
            return Adresss1;
        }
        private string getadress2(string selectedAdress)
        {
            Adresss2 = selectedAdress;
            return Adresss2;
        }
        private void checkerSost()
        {
            if (Adresss1 != null && Adresss2 != null)
            {
                FlyoutMenu.IsEnabled = true;
                FrameAdress.HeightRequest = 400;
                Grid.SetRow(AdressLayout, 1);
                Grid.SetRow(PosRN_Search_AdressToGo, 3);
                Grid.SetRow(SecondadressFrame, 3);
                Grid.SetRow(Entry2, 2);
                Grid.SetRowSpan(Entry1,1);
                Grid.SetRowSpan(FirstadressFrame, 1);
                Grid.SetRowSpan(PosRN_Search_AdressTo, 1);
                Grid.SetRowSpan(Entry2, 1);
                Grid.SetRowSpan(SecondadressFrame, 1);
                Grid.SetRowSpan(PosRN_Search_AdressToGo, 1);
                TimeToTravel.IsVisible = true;
                TimeToTravel.IsEnabled = true;
                DriverNamenCar.IsVisible = true;
                DriverNamenCar.IsEnabled = true;
                OrderPrice.IsVisible = true;
                OrderPrice.IsEnabled = true;
                GosNumFrame.IsVisible = true;
                GosNumFrame.IsEnabled = true;
                Car_GosNum.IsVisible = true;
                Car_GosNum.IsEnabled = true;
                GoMenuLayout.IsVisible = true;
                GoMenuLayout.IsEnabled = true;
                TimeToTravel.Text = $"В пути: {DistanceTimeMath()} мин. | Путь: {Convert.ToString(distance)} м.";
                DriverNamenCar.Text = "";
                Car_GosNum.Text = "";
                OrderPrice.Text = "";
            }
            else
            {
                FlyoutMenu.IsEnabled = false;
                FrameAdress.HeightRequest = 200;
                Grid.SetRow(AdressLayout, 5);
                Grid.SetRow(PosRN_Search_AdressToGo, 5);
                Grid.SetRow(SecondadressFrame, 5);
                Grid.SetRow(Entry2, 4);
                Grid.SetRowSpan(Entry1, 2);
                Grid.SetRowSpan(FirstadressFrame, 2);
                Grid.SetRowSpan(PosRN_Search_AdressTo, 2);
                Grid.SetRowSpan(Entry2, 2);
                Grid.SetRowSpan(SecondadressFrame, 2);
                Grid.SetRowSpan(PosRN_Search_AdressToGo, 2);
                TimeToTravel.IsVisible = false;
                TimeToTravel.IsEnabled = false;
                DriverNamenCar.IsVisible = false;
                DriverNamenCar.IsEnabled = false;
                OrderPrice.IsVisible = false;
                OrderPrice.IsEnabled = false;
                GosNumFrame.IsVisible = false;
                GosNumFrame.IsEnabled = false;
                Car_GosNum.IsVisible = false;
                Car_GosNum.IsEnabled = false;
                GoMenuLayout.IsVisible = false;
                GoMenuLayout.IsEnabled = false;
                TimeToTravel.Text = "";
                DriverNamenCar.Text = "";
                Car_GosNum.Text = "";
                OrderPrice.Text = "";
                OrderConfirm.IsEnabled = false;
            }
        }
        private async void OnSearchBarFocused(object sender, FocusEventArgs e)
        {
            var searchBar = (SearchBar)sender;
            var selectedAddress = await DisplayActionSheet("Выберите адрес", "Отмена", null, _Adress.ToArray());
            if (selectedAddress != "Отмена") // пользователь выбрал адрес
            {
                searchBar.Text = selectedAddress;
                getadress1(selectedAddress);
                AdressDistance();
            }
            if (PosRN_Search_AdressTo.Text == null)
            {
                DriverNamenCar.Text = "";
                Car_GosNum.Text = "";
                OrderPrice.Text = "";
                TimeToTravel.Text = "";
                AdressDistance();
            }
            searchBar.Unfocus(); // снятие фокуса с SearchBar для закрытия клавиатуры
        }
        private async void OnSearchBarFocused2(object sender, FocusEventArgs e)
        {
            var searchBar = (SearchBar)sender;
            var selectedAddress = await DisplayActionSheet("Выберите адрес", "Отмена", null, _Adress.ToArray());
            if (selectedAddress != "Отмена") // пользователь выбрал адрес
            {
                searchBar.Text = selectedAddress;
                getadress2(selectedAddress);
                AdressDistance();
            }
            if (PosRN_Search_AdressToGo.Text == null)
            {
                PosRN_Search_AdressToGo.Text = "";
                DriverNamenCar.Text = "";
                Car_GosNum.Text = "";
                OrderPrice.Text = "";
                TimeToTravel.Text = "";
                AdressDistance();
            }
            searchBar.Unfocus(); // снятие фокуса с SearchBar для закрытия клавиатуры
        }
        private void OnSearchBarUnFocused(object sender, EventArgs e)
        {
            PriceWithoutClass();
        }
        private void OnSearchBarUnFocused2(object sender, EventArgs e)
        {
            PriceWithoutClass();
        }
        private void  PosRN_Search_OnSearchButtonPressed(object sender, EventArgs e)
        {
        }
        private void PosRN_Search_OnSearchButtonPressed2(object sender, EventArgs e)
        {
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
            checkerSost();
        }
        //CALCULATIONS
        private void PriceWithoutClass ()
        {
            int[,] rate = new int[,]{ { 1, 2, 3 } };
            Eco_PickerPrice.Text = PriceCalculation(rate[0, 0]).ToString() + " ₽";
            Comf_PickerPrice.Text = PriceCalculation(rate[0, 1]).ToString() + " ₽";
            Bui_PickerPrice.Text = PriceCalculation(rate[0, 2]).ToString() + " ₽";
        }
        private void TimeToGo()
        {
            int minutes = 1;
            int minutedate = DateTime.Now.Minute + minutes;
            TimeToGO.Text = $"Водитель прибудет в:  {DateTime.Now.Hour} : {(DateTime.Now.Minute + minutes).ToString("00")}";
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                if (DateTime.Now.Hour == DateTime.Now.Hour && minutedate == DateTime.Now.Minute)
                {
                    Device.BeginInvokeOnMainThread(() => TimeToGO.Text = "Водитель на месте!");
                    DependencyService.Get<INotificationService>().ShowNotification("Водитель на месте!", "Водитель на месте!");
                    return false; // Останавливаем таймер после изменения значения
                }
                else
                {
                    return true; // Продолжаем запускать таймер
                }
            });
        }
        public interface INotificationService
        {
            Task ShowNotification(string title, string message);
        }
        // This calculate time to travel
        private double DistanceTimeMath()
        {
            double mtime = 0.0010;
            double TimeToPass = distance * mtime;
            return TimeToPass;
        }
        // This shit calculate price XD
        private double PriceCalculation(int rate)
        {
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
            double PriceMath = Math.Round(distance * km * ratemultipler / 1000);
            return PriceMath;
        }
        // Method returns Driver name, car name and gos num
        private async void _RatePicker(int rate)
        {
            var c = await MainPage.Database.GetCarsV4();
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
            OrderPrice.Text = "";
            OrderPrice.Text = Convert.ToString("Цена: " + PriceCalculation(rate) + " ₽");
            OrderConfirm.IsEnabled = true;
        }
        //LOGIC
        private static System.Timers.Timer timer;
        private int OrderClass;
        private int rate;
        private void EconomButton_Click(object sender, EventArgs e)
        {
            rate = 1;
            _ORDER_OPTION(rate);
            OrderClass = 1;
        }
        private void BusinessButton_Click(object sender, EventArgs e)
        {
            rate = 3;
            _ORDER_OPTION(rate);
            OrderClass = 3;
        }
        private void ComfortButton_Click(object sender, EventArgs e)
        {
            rate = 2;
            _ORDER_OPTION(rate);
            OrderClass = 2;
        }
        private string OrderClassTXT;
        private async void OrderConfirm_Click(object sender, EventArgs e)
        {
            
            var order = await MainPage.database.GetOrderDAT();
            string Adress_A = PosRN_Search_AdressTo.Text + ".";
            string Adress_B = PosRN_Search_AdressToGo.Text + ".";
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
            if (Adress_A.Length < 2 || Adress_B.Length < 2 || OrderPrice.Text == "")
            {
                await DisplayAlert("Ошибка", "Не все данные введены.", "Продолжить");
            }
            else
            {
                _RatePicker(rate);
                TimeToGo();
                await Task.Delay(50);
                string result = "";
                string legthstopper = DriverNamenCar.Text;
                char delimeter = '/';
                int index = legthstopper.IndexOf(delimeter);
                if (index != -1)
                {
                    result = legthstopper.Substring(0, index);
                }
                await Task.Delay(100);
                var new_Order = new OrderDataV4
                {
                    Driver_Name_Car = result,
                    ID = Id,
                    Price = OrderPrice.Text,
                    OrderClass = OrderClassTXT,
                    Adress_A_B = $"{Adress_A} → {Adress_B}",
                    PaymentOption = paymentclass,
                };
                await database.AddNewOrder(new_Order);
                LastOrderWriter();

            }
        }
        private int paymentclass = 3;
        private void paymentoption()
        {
            switch(paymentclass)
            {
                case 1:
                    MirButton.IsEnabled = false;
                    MSButton.IsEnabled = true;
                    NalButton.IsEnabled = true;
                    MirButton.Margin = -5;
                    NalButton.Margin = 0;
                    MSButton.Margin = 0;
                    break;
                case 2:
                    MSButton.IsEnabled = false;
                    MirButton.IsEnabled = true;
                    NalButton.IsEnabled = true;
                    MSButton.Margin = -5;
                    MirButton.Margin = 0;
                    NalButton.Margin = 0;
                    break;
                case 3:
                    NalButton.IsEnabled = false;
                    MSButton.IsEnabled = true;
                    MirButton.IsEnabled = true;
                    NalButton.Margin = -5;
                    MirButton.Margin = 0;
                    MSButton.Margin = 0;
                    break;
            }
        }
        private void PayOptionBTN_Click(object sender, EventArgs e)
        {
            paymentoption();
            PayoptionLayout.IsVisible = !PayoptionLayout.IsVisible;
            PayoptionLayout.IsEnabled = !PayoptionLayout.IsEnabled;
        }
        private void MSButton_Click(object sender, EventArgs e)
        {
            paymentclass = 2;
            paymentoption();
            Payoption.Source = "MCCard100x100";
        }
        private void MIRButton_Click(object sender, EventArgs e)
        {
            paymentclass = 1;
            paymentoption();
            Payoption.Source = "MIRCard100x100";
        }
        private void NalButton_Click(object sender, EventArgs e)
        {
            paymentclass = 3;
            paymentoption();
            Payoption.Source = "Nalichka100x100";
        }
        private async void LastOrderWriter()
        {
            var order = await MainPage.database.GetOrderDAT();
            string orderclass = order.LastOrDefault().OrderClass.ToString();
            switch (orderclass)
            {
                case "Эконом":
                    OrderImage.Source = "Eco_car_ref.png";
                    break;
                case "Комфорт":
                    OrderImage.Source = "Comf_Car_Ref.png";
                    break;
                case "Бизнес":
                    OrderImage.Source = "Bui_Car_Ref.png";
                    break;
            }
            OrderClas2.Text = orderclass;
            CostLastOrder.Text = order.LastOrDefault().Price.ToString();
            LastOrderId.Text = $"ID: {order.LastOrDefault().ID}";
            int PaymentOptionorder = order.LastOrDefault().PaymentOption;
            switch (PaymentOptionorder)
            {
                case 1:
                    OrderPaymentOption.Text = "Карта МИР";
                    break;
                case 2:
                    OrderPaymentOption.Text = "Карта MasterCard";
                    break;
                case 3:
                    OrderPaymentOption.Text = "Наличные";
                    break;
            }
            AdressLastOrder.Text = order.LastOrDefault().Adress_A_B.ToString();
            LastNameOfOrder.Text = order.LastOrDefault().Driver_Name_Car.ToString();
            }
        }
    }

