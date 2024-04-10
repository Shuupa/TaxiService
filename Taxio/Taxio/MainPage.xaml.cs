using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.AppleSignInAuthenticator;

[assembly: ExportFont("Impact-Regular.ttf", Alias = "Impact")]
[assembly: ExportFont("Consola-Reguar.ttf", Alias = "Consola")]
namespace Taxio
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public double DistanceTimeMath(double distance)
        {
            double distanceto = distance;
            double kmtime = 1.50;
            double TimeToPass = distanceto * kmtime;
            return TimeToPass;
        }
        public void EconomButton_Click(object sender, EventArgs e)
        {
            OrderGoButton.BackgroundColor = Color.DarkGreen;
            int km = 24;
            double ratemultipler = 1.22;
            double distance = 25;
            string PriceMath = distance * km * ratemultipler + " ₽";
            double TimeToPass =  DistanceTimeMath(distance);
            TimeToTravel.Text = Convert.ToString("В пути: " + distance + " Км" + " / " + TimeToPass + " Минут");
            OrderPrice.Text = "";
            OrderPrice.Text = PriceMath; 
        }
        public void BusinessButton_Click(object sender, EventArgs e)
        {
            OrderGoButton.BackgroundColor = Color.DarkBlue;
            int km = 66;
            double ratemultipler = 2.33;
            double distance = 35;
            string PriceMath = distance * km * ratemultipler + " ₽";
            double TimeToPass = DistanceTimeMath(distance);
            TimeToTravel.Text = Convert.ToString("В пути: " + distance + " Км" + " / " + TimeToPass + " Минут");
            OrderPrice.Text = "";
            OrderPrice.Text = PriceMath;
        }
        public void ComfortButton_Click(object sender, EventArgs e)
        {
            OrderGoButton.BackgroundColor = Color.FromHex("#FFBE8F56");
            int km = 32;
            double ratemultipler = 1.87;
            double distance = 42;
            string PriceMath = distance * km * ratemultipler + " ₽";
            double TimeToPass = DistanceTimeMath(distance);
            TimeToTravel.Text = Convert.ToString("В пути: " + distance + " Км" + " / " + TimeToPass + " Минут");
            OrderPrice.Text = "";
            OrderPrice.Text = PriceMath;
        }
    }
}
