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
    public partial class EditAssessment : ContentPage
    {
        public EditAssessmentViewModel ViewModel { get; set; }

        public EditAssessment(Assessment assessment)
        {
            InitializeComponent();
            ViewModel = new EditAssessmentViewModel(assessment);
            BindingContext = ViewModel;
        }



        private async void OnSaveClicked(object sender, EventArgs e)
        {
            ViewModel.EditedAssessmentName = editedNameEntry.Text;
            ViewModel.EditedStartDate = datePickerStartDate.Date;
            ViewModel.EditedEndDate = datePickerEndDate.Date;
            ViewModel.EditedGetNotifications = editedNotificationSwitch.IsToggled;

            Assessment updatedAssessment = ViewModel.UpdateAssessment();

            AdoNetDatabaseService.SaveAssessment(updatedAssessment);

            await DisplayAlert("Success", "Assessment saved successfully!", "OK");

            await Navigation.PopAsync();
        }
    }
}