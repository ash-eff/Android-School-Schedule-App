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
    public partial class AddPerformanceAssessment : ContentPage
    {
        private Course Course { get; set; }

        public AddPerformanceAssessment(Course course)
        {
            InitializeComponent();
            Course = course;
            BindingContext = this;
        }

        private async void OnSaveAssessmentClicked(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }

            AssessmentViewModel newAssessmentViewModel = new AssessmentViewModel
            {
                Name = assessmentNameEntry.Text,
                StartDate = assessmentStartDatePicker.Date,
                EndDate = assessmentEndDatePicker.Date,
                GetNotified = notificationSwitch.IsToggled,
                CourseId = Course.Id,
                Type = AssessmentViewModel.AssessmentType.Performance
            };

            Assessment newAssessment = newAssessmentViewModel.CreateAssessment();

            AdoNetDatabaseService.SaveAssessment(newAssessment);

            await DisplayAlert("Success", $"{newAssessment.Name} has been added to {Course.Name}.", "Ok");

            await Navigation.PopAsync();

        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(assessmentNameEntry.Text))
            {
                DisplayAlert("Error", "Assessment name cannot be empty.", "OK");
                return false;
            }

            Course workingCourse = Course;
            if (assessmentStartDatePicker.Date < workingCourse.StartDate.Date)
            {
                DisplayAlert("Error", "Assessment start date cannot be before the course start date.", "OK");
                return false;
            }

            if (assessmentEndDatePicker.Date > workingCourse.EndDate.Date)
            {
                DisplayAlert("Error", "Course end date cannot be after the term end date.", "OK");
                return false;
            }

            if (assessmentEndDatePicker.Date <= assessmentStartDatePicker.Date)
            {
                DisplayAlert("Error", "End date cannot be before or on the same day as the start date.", "OK");
                return false;
            }

            return true;
        }
    }
}