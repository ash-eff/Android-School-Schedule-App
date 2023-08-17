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
        Course SelectedCourse { get; set; }

        public EditCourse(EditCourseViewModel viewModel)
        {
            InitializeComponent();
            SelectedCourse = viewModel.SelectedCourse;
            PopulateFields();
        }

        private void PopulateFields()
        {
            editedNameEntry.Text = SelectedCourse.Name;
            datePickerStartDate.Date = SelectedCourse.StartDate;
            datePickerEndDate.Date = SelectedCourse.EndDate;
            courseStatusPicker.SelectedItem = SelectedCourse.CourseStatus;
            editedInstructorNameEntry.Text = SelectedCourse.InstructorName;
            editedPhoneEntry.Text = SelectedCourse.InstructorPhone;
            editedEmailEntry.Text = SelectedCourse.InstructorEmail;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            SelectedCourse.Name = editedNameEntry.Text;
            SelectedCourse.StartDate = datePickerStartDate.Date;
            SelectedCourse.EndDate = datePickerEndDate.Date;
            SelectedCourse.CourseStatus = courseStatusPicker.SelectedItem.ToString();
            SelectedCourse.InstructorName = editedInstructorNameEntry.Text;
            SelectedCourse.InstructorPhone = editedPhoneEntry.Text;
            SelectedCourse.InstructorEmail = editedEmailEntry.Text;

            AdoNetDatabaseService.SaveCourse(SelectedCourse);

            await DisplayAlert("Success", "Course saved successfully!", "OK");

            await Navigation.PopAsync();
        }
    }
}