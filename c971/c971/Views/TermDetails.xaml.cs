using c971.Models;
using c971.Services;
using c971.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace c971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDetails : ContentPage
    {
        public AcademicTerm SelectedTerm { get; set; }

        public ObservableCollection<Course> coursesList { get; set; } = new ObservableCollection<Course>();

        public TermDetails(AcademicTerm term)
        {
            InitializeComponent();
            SelectedTerm = term;
            BindingContext = this;
        }

        private async void OnCourseTapped(object sender, EventArgs e)
        {
            if (e is TappedEventArgs tappedEventArgs && tappedEventArgs.Parameter is Course selectedCourse)
            {
                Console.WriteLine("selected course name is " + selectedCourse.Name);
                CourseDetails courseDetailsPage = new CourseDetails(selectedCourse);

                await Navigation.PushAsync(courseDetailsPage);
            }
        }
        private void OnToggleCourseClicked(object sender, EventArgs e)
        {
            CloseAllHiddenSections();
            courseSection.IsVisible = !courseSection.IsVisible;
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            CloseAllHiddenSections();
            addCourseMenu.IsVisible = !addCourseMenu.IsVisible;
        }
        private void CloseAllHiddenSections()
        {
            courseSection.IsVisible = false;
            addCourseMenu.IsVisible = false;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool deleteTerm = await DisplayAlert("Delete Term", "Are you sure you want to delete this Term?", "Yes", "No");
            if (!deleteTerm)
            {
                //do nothing
            }
            else
            {
                // Navigate back to the Dashboard page
                AdoNetDatabaseService.RemoveAcademicTerm(SelectedTerm);
                await Navigation.PopAsync();
            }
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            EditTermViewModel editTermViewModel = new EditTermViewModel
            {
                SelectedTerm = SelectedTerm,
                EditedTermName = SelectedTerm.Name,
                EditedStartDate = SelectedTerm.StartDate,
                EditedEndDate = SelectedTerm.EndDate
            };

            await Navigation.PushAsync(new EditTerm(editTermViewModel));
        }

        private async void OnAddCourseClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCourse(SelectedTerm));
        }

        protected override void OnAppearing()
        {
            coursesList.Clear(); // Clear the previous data
            var newCoursesList = AdoNetDatabaseService.GetCourseTableAsListForTermId(SelectedTerm.Id);
            foreach (var course in newCoursesList)
            {
                coursesList.Add(course);
            }

            if (scroller != null)
            {
                scroller.ScrollToAsync(0, 0, false);
            }

            base.OnAppearing();
        }
    }
}