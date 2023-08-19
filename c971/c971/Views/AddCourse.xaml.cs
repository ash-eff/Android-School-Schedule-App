using c971.Models;
using c971.Services;
using c971.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace c971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCourse : ContentPage
    {
        private AcademicTerm Term { get; set; }
        public AddCourseViewModel ViewModel { get; set; }

        public AddCourse(AcademicTerm term)
        {
            InitializeComponent();
            ViewModel = new AddCourseViewModel();
            Term = term;
            BindingContext = ViewModel;
        }

        private async void OnSaveCourseClicked(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }

            AddCourseViewModel newCourseViewModel = new AddCourseViewModel
            {
                AddedName = courseNameEntry.Text,
                AddedStartDate = courseStartDatePicker.Date,
                AddedEndDate = courseEndDatePicker.Date,
                AddedStatus = courseStatusPicker.SelectedItem.ToString(),
                AddedInstructorName = instructorNameEntry.Text,
                AddedInstructorPhone = instructorPhoneEntry.Text,
                AddedInstructorEmail = instructorEmailEntry.Text,
                AddedTermId = Term.Id,
                AddedNotes = notesEditor.Text,
                AddedGetNotified = notificationSwitch.IsToggled
            };

            Course newCourse = newCourseViewModel.CreateCourse();

            AdoNetDatabaseService.SaveCourse(newCourse);
            
            await DisplayAlert("Success", $"{courseNameEntry.Text} has been added to {Term.Name}.", "Ok");
            
            await Navigation.PopAsync();
            
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(courseNameEntry.Text))
            {
                DisplayAlert("Error", "Course name cannot be empty.", "OK");
                return false;
            }

            AcademicTerm workingTerm = Term;
            if (courseStartDatePicker.Date < workingTerm.StartDate.Date)
            {
                DisplayAlert("Error", "Course start date cannot be before the term start date.", "OK");
                return false;
            }

            if (courseEndDatePicker.Date > workingTerm.EndDate.Date)
            {
                DisplayAlert("Error", "Course end date cannot be after the term end date.", "OK");
                return false;
            }

            if (courseEndDatePicker.Date <= courseStartDatePicker.Date)
            {
                DisplayAlert("Error", "End date cannot be before or on the same day as the start date.", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(instructorNameEntry.Text))
            {
                DisplayAlert("Error", "Instructor name cannot be empty.", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(instructorPhoneEntry.Text))
            {
                DisplayAlert("Error", "Phone number cannot be empty.", "OK");
                return false;
            }

            string phoneNumberPattern = @"^\d{3}-\d{3}-\d{4}$";
            if (!Regex.IsMatch(instructorPhoneEntry.Text, phoneNumberPattern))
            {
                DisplayAlert("Error", "Invalid phone number format. Please use the format '###-###-####'.", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(instructorEmailEntry.Text))
            {
                DisplayAlert("Error", "Email cannot be empty.", "OK");
                return false;
            }

            return true;
        }
    }
}
