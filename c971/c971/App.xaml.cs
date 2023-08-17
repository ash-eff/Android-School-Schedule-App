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
