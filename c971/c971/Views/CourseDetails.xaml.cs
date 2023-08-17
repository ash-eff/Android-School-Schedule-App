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
        public List<Assessment> assessmentList = new List<Assessment>();

        public Course SelectedCourse { get; set; }
        public CourseDetails(Course course)
        {
            InitializeComponent();
            SelectedCourse = course;
            BindingContext = this;
        }

        private async void OnAssessmentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Assessment selectedAssessment && selectedAssessment != null)
            {
                Console.WriteLine("selected assessment name is " + selectedAssessment.Name);
                var assessmentDetailsPage = new AssessmentDetails(selectedAssessment);

                await Navigation.PushAsync(assessmentDetailsPage);
            }
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            EditCourseViewModel editTermViewModel = new EditCourseViewModel
            {
                SelectedCourse = SelectedCourse,
                EditedCourseName = SelectedCourse.Name,
                EditedStartDate = SelectedCourse.StartDate,
                EditedEndDate = SelectedCourse.EndDate,
                EditedStatus = SelectedCourse.CourseStatus,
                EditedInstructorName = SelectedCourse.InstructorName,
                EditedInstructorPhone = SelectedCourse.InstructorPhone,
                EditedInstructorEmail = SelectedCourse.InstructorEmail
            };

            await Navigation.PushAsync(new EditCourse(editTermViewModel));
        }

        private async void OnAddAssessmentClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssessment(SelectedCourse));
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
                AdoNetDatabaseService.RemoveCourse(SelectedCourse);
                await Navigation.PopAsync();
            }
        }

        protected override void OnAppearing()
        {
            //AdoNetDatabaseService.TableInformation();
            assessmentList = AdoNetDatabaseService.GetCourseTableAsListForCourse(SelectedCourse);
            listView.ItemsSource = assessmentList;
            base.OnAppearing();
        }
    }
}