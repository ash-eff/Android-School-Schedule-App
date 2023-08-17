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

        public List<Course> coursesList = new List<Course>();

        public TermDetails(AcademicTerm term)
        {
            InitializeComponent();
            SelectedTerm = term;
            BindingContext = this;
        }

        private async void OnCourseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Course selectedCourse && selectedCourse != null)
            {
                Console.WriteLine("selected course name is " + selectedCourse.Name);
                var courseDetailsPage = new CourseDetails(selectedCourse);

                await Navigation.PushAsync(courseDetailsPage);
            }
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
            //AdoNetDatabaseService.TableInformation();
            coursesList = AdoNetDatabaseService.GetCourseTableAsListForTerm(SelectedTerm);
            listView.ItemsSource = coursesList;
            base.OnAppearing();
        }
    }
}