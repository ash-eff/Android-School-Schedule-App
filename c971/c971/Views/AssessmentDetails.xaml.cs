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
    public partial class AssessmentDetails : ContentPage
    {
        public AssessmentViewModel ViewModel { get; set; }
        private Assessment workingAssessment;
        private int workingAssessmentId;

        public AssessmentDetails(Assessment assessment)
        {
            InitializeComponent();
            workingAssessmentId = assessment.Id;
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditAssessment(ViewModel.Assessment));
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool courseTerm = await DisplayAlert("Delete Assessment", "Are you sure you want to delete this Assessment?", "Yes", "No");
            if (!courseTerm)
            {
                //do nothing
            }
            else
            {
                AdoNetDatabaseService.RemoveAssessment(ViewModel.Assessment);
                await Navigation.PopAsync();
            }
        }


        protected override void OnAppearing()
        {
            // these lines need to happen in on appearing so that when a course is update and the page pops back to this one, the updated assessment information will be added to the view model so the page can update
            workingAssessment = AdoNetDatabaseService.GetAssessmentById(workingAssessmentId);
            ViewModel = new AssessmentViewModel(workingAssessment);
            BindingContext = ViewModel;

            //if (scroller != null)
            //{
            //    scroller.ScrollToAsync(0, 0, false);
            //}


            base.OnAppearing();
        }
    }
}