using c971.Models;
using c971.Services;
using c971.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace c971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTerm : ContentPage
    {
        private AcademicTerm NewTerm { get; set; }
        public AddTerm()
        {
            InitializeComponent();
            NewTerm = new AcademicTerm();
            BindingContext = NewTerm;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }
            
            AcademicTerm newTerm = new AcademicTerm
            {
                Name = termNameEntry.Text,
                StartDate = startTimePicker.Date,
                EndDate = endTimePicker.Date
            };
            
            AdoNetDatabaseService.SaveAcademicTerm(newTerm);

            await DisplayAlert("Success", $"{newTerm.Name} has been added.", "Ok");

            await Navigation.PopAsync();
            
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(termNameEntry.Text))
            {
                DisplayAlert("Error", "Term name cannot be empty.", "OK");
                return false;
            }

            if (endTimePicker.Date <= startTimePicker.Date)
            {
                DisplayAlert("Error", "End date cannot be before or on the same day as the start date.", "OK");
                return false;
            }

            return true;
        }
    }
}