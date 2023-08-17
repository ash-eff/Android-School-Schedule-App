using c971.Models;
using c971.Services;
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
        public List<string> CourseStatusOptions { get; set; }
        public List<string> PrepopulatedCourseNames { get; set; }
        public AddCourse(AcademicTerm term)
        {
            InitializeComponent();
            Term = term;

            CourseStatusOptions = new List<string>
        {
            "Not Started",
            "In Progress",
            "Completed"
        };

            BindingContext = this;
        }

        private async void OnSaveCourseClicked(object sender, EventArgs e)
        {
            Course newCourse = new Course
            {
                Name = courseNameEntry.Text,
                StartDate = courseStartDatePicker.Date,
                EndDate = courseEndDatePicker.Date,
                CourseStatus = courseStatusPicker.SelectedItem.ToString(),
                InstructorName = instructorNameEntry.Text,
                InstructorPhone = instructorPhoneEntry.Text,
                InstructorEmail = instructorEmailEntry.Text,
                TermId = Term.Id
            };

            AdoNetDatabaseService.SaveCourse(newCourse);
            
            await DisplayAlert("Success", $"{courseNameEntry.Text} has been added to {Term.Name}.", "Ok");
            
            await Navigation.PopAsync();
            
        }
    }
}
