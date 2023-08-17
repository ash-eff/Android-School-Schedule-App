using c971.Views;
using c971.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using c971.ViewModels;
using c971.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace c971
{
    public partial class App : Application
    {
        //public static AdoNetDatabaseService Database { get; private set; }
        public static string dbPath;
        public static Color DeleteColor = Color.FromHex("#a0615f");
        public static Color EditColor = Color.FromHex("#5F9EA0");
        public static Color AddColor = Color.FromHex("#3a7f98");
        public static Color Background = Color.FromHex("#ece6df");
        public static Color ContainerColor = Color.FromHex("#e2d9cf");


        public App(string completePath)
        {
            InitializeComponent();

            var dashBoard = new Dashboard();
            var navPage = new NavigationPage(dashBoard);
            MainPage = navPage;
            dbPath = completePath;

            //AdoNetDatabaseService.InitializeDatabase();
        }
    }
}
