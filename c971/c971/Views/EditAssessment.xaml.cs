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
            if (IsValid())
            {
                return;
            }

            ViewModel.EditedAssessmentName = editedNameEntry.Text;
            ViewModel.EditedStartDate = datePickerStartDate.Date;
            ViewModel.EditedEndDate = datePickerEndDate.Date;
            ViewModel.EditedGetNotifications = editedNotificationSwitch.IsToggled;

            Assessment updatedAssessment = ViewModel.UpdateAssessment();

            AdoNetDatabaseService.SaveAssessment(updatedAssessment);

            await DisplayAlert("Success", "Assessment saved successfully!", "OK");

            await Navigation.PopAsync();
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(editedNameEntry.Text))
            {
                DisplayAlert("Error", "Assessment name cannot be empty.", "OK");
                return false;
            }

            Course workingCourse = AdoNetDatabaseService.GetCourseById(ViewModel.SelectedAssessment.CourseId);
            if (datePickerStartDate.Date < workingCourse.StartDate.Date)
            {
                DisplayAlert("Error", "Assessment start date cannot be before the course start date.", "OK");
                return false;
            }

            if (datePickerEndDate.Date > workingCourse.EndDate.Date)
            {
                DisplayAlert("Error", "Course end date cannot be after the term end date.", "OK");
                return false;
            }

            if (datePickerEndDate.Date <= datePickerStartDate.Date)
            {
                DisplayAlert("Error", "End date cannot be before or on the same day as the start date.", "OK");
                return false;
            }

            return true;
        }
    }
}