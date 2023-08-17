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
    public partial class CourseDetails : ContentPage
    {
        public CourseViewModel ViewModel { get; set; }
        public CourseDetails(Course course)
        {
            InitializeComponent();
            ViewModel = new CourseViewModel(course);
            BindingContext = ViewModel;
        }

        private async void OnAssessmentTapped(object sender, EventArgs e)
        {
            if (e is TappedEventArgs tappedEventArgs && tappedEventArgs.Parameter is Assessment selectedAssessment)
            {
                var assessmentDetailsPage = new AssessmentDetails(selectedAssessment);
                await Navigation.PushAsync(assessmentDetailsPage);
            }
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            EditCourseViewModel editTermViewModel = new EditCourseViewModel
            {
                SelectedCourse = ViewModel.SelectedCourse,
                EditedCourseName = ViewModel.CourseName,
                EditedStartDate = ViewModel.StartDate,
                EditedEndDate = ViewModel.EndDate,
                EditedStatus = ViewModel.Status,
                EditedInstructorName = ViewModel.InstructorName,
                EditedInstructorPhone = ViewModel.InstructorPhone,
                EditedInstructorEmail = ViewModel.InstructorEmail
            };
            
            await Navigation.PushAsync(new EditCourse(editTermViewModel));
        }

        private async void OnAddAssessmentClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssessment(ViewModel.SelectedCourse));
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool courseTerm = await DisplayAlert("Delete Course", "Are you sure you want to delete this Course?", "Yes", "No");
            if (!courseTerm)
            {
                //do nothing
            }
            else
            {
                // Navigate back to the Dashboard page
                AdoNetDatabaseService.RemoveCourse(ViewModel.SelectedCourse);
                await Navigation.PopAsync();
            }
        }

        protected override void OnAppearing()
        {
            var newAssessmentList = AdoNetDatabaseService.GetAssessmentTableAsListForCourse(ViewModel.SelectedCourse);
            ViewModel.Assessments.Clear();
            foreach (Assessment assessment in newAssessmentList)
            {
                ViewModel.Assessments.Add(assessment);
            }

            if (scroller != null)
            {
                scroller.ScrollToAsync(0, 0, false);
            }


            base.OnAppearing();
        }
    }
}