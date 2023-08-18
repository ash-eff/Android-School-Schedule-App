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
    }
}