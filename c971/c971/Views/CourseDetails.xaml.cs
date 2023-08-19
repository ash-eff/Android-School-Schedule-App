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
        private Course workingCourse;
        private int workingCourseId;
        public CourseDetails(Course course)
        {
            InitializeComponent();
            workingCourseId = course.Id;
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
            await Navigation.PushAsync(new EditCourse(ViewModel.SelectedCourse));
        }

        private async void OnAddPerformanceAssessmentClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPerformanceAssessment(ViewModel.SelectedCourse));
        }
        private async void OnAddObjectiveAssessmentClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddObjectiveAssessment(ViewModel.SelectedCourse));
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
                AdoNetDatabaseService.RemoveCourse(ViewModel.SelectedCourse);
                await Navigation.PopAsync();
            }
        }

        private void OnShareNotesClicked(object sender, EventArgs e)
        {
            string courseNotes = ViewModel.CourseNotes;
            MessagingCenter.Send(Application.Current, "ShareCourseNotes", courseNotes);
        }


        private void OnToggleAssessmentsClicked(object sender, EventArgs e)
        {
            CloseAllHiddenSections();
            assessmentSection.IsVisible = !assessmentSection.IsVisible;
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            CloseAllHiddenSections();
            addAssessmentMenu.IsVisible = !addAssessmentMenu.IsVisible;
        }

        private void OnViewNotesClicked(object sender, EventArgs e)
        {
            CloseAllHiddenSections();
            notesInfoStackLayout.IsVisible = !notesInfoStackLayout.IsVisible;
        }

        private void OnViewContactClicked(object sender, EventArgs e)
        {
            CloseAllHiddenSections();
            contactInfoStackLayout.IsVisible = !contactInfoStackLayout.IsVisible;
        }

        private void CloseAllHiddenSections()
        {
            assessmentSection.IsVisible = false;
            addAssessmentMenu.IsVisible = false;
            notesInfoStackLayout.IsVisible = false;
            contactInfoStackLayout.IsVisible = false;
        }



        protected override void OnAppearing()
        {
            // these lines need to happen in on appearing so that when a course is update and the page pops back to this one, the updated course information will be added to the view model so the page can update
            workingCourse = AdoNetDatabaseService.GetCourseById(workingCourseId);
            ViewModel = new CourseViewModel(workingCourse);
            BindingContext = ViewModel;

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