using c971.Models;
using c971.Services;
using c971.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using c971.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace c971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCourse : ContentPage
    {
        public EditCourseViewModel ViewModel { get; set; }

        public EditCourse(Course course)
        {
            InitializeComponent();
            ViewModel = new EditCourseViewModel(course);
            BindingContext = ViewModel;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }


            ViewModel.EditedCourseName = editedNameEntry.Text;
            ViewModel.EditedStartDate = datePickerStartDate.Date;
            ViewModel.EditedEndDate = datePickerEndDate.Date;
            ViewModel.EditedStatus = courseStatusPicker.SelectedItem.ToString();
            ViewModel.EditedInstructorName = editedInstructorNameEntry.Text;
            ViewModel.EditedInstructorPhone = editedPhoneEntry.Text;
            ViewModel.EditedInstructorEmail = editedEmailEntry.Text;
            ViewModel.EditedNotes = editedNotes.Text;
            ViewModel.EditedGetNotifications = editedNotificationSwitch.IsToggled;

            Course updatedCourse = ViewModel.UpdateCourse();

            AdoNetDatabaseService.SaveCourse(updatedCourse);

            await DisplayAlert("Success", "Course saved successfully!", "OK");

            await Navigation.PopAsync();
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(editedNameEntry.Text))
            {
                DisplayAlert("Error", "Course name cannot be empty.", "OK");
                return false;
            }

            AcademicTerm workingTerm = AdoNetDatabaseService.GetAcedemicTermById(ViewModel.SelectedCourse.TermId);
            if (datePickerStartDate.Date < workingTerm.StartDate.Date)
            {
                DisplayAlert("Error", "Course start date cannot be before the term start date.", "OK");
                return false;
            }

            if (datePickerEndDate.Date > workingTerm.EndDate.Date)
            {
                DisplayAlert("Error", "Course end date cannot be after the term end date.", "OK");
                return false;
            }

            if (datePickerEndDate.Date <= datePickerStartDate.Date)
            {
                DisplayAlert("Error", "End date cannot be before or on the same day as the start date.", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(editedInstructorNameEntry.Text))
            {
                DisplayAlert("Error", "Instructor name cannot be empty.", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(editedPhoneEntry.Text))
            {
                DisplayAlert("Error", "Phone number cannot be empty.", "OK");
                return false;
            }

            string phoneNumberPattern = @"^\d{3}-\d{3}-\d{4}$";
            if (!Regex.IsMatch(editedPhoneEntry.Text, phoneNumberPattern))
            {
                DisplayAlert("Error", "Invalid phone number format. Please use the format '###-###-####'.", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(editedEmailEntry.Text))
            {
                DisplayAlert("Error", "Email cannot be empty.", "OK");
                return false;
            }

            return true;
        }
    }
}