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
        public Assessment SelectedAssessment;

        public AssessmentDetails(Assessment assessment)
        {
            InitializeComponent();
            SelectedAssessment = assessment;
            ViewModel = new AssessmentViewModel(assessment);
            BindingContext = ViewModel;
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
    }
}