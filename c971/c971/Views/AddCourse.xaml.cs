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
    }
}
