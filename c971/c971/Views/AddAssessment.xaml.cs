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
    public partial class AddAssessment : ContentPage
    {
        private Course Course { get; set; }
        public AddAssessment(Course course)
        {
            InitializeComponent();
            Course = course;
            BindingContext = this;
        }

        private async void OnSaveAssessmentClicked(object sender, EventArgs e)
        {
            Assessment newAssessment = new Assessment
            {
                Name = assessmentNameEntry.Text,
                StartDate = assessmentStartDatePicker.Date,
                EndDate = assessmentEndDatePicker.Date,
                CourseId = Course.Id
            };

            AdoNetDatabaseService.SaveAssessment(newAssessment);

            await DisplayAlert("Success", $"{newAssessment.Name} has been added to {Course.Name}.", "Ok");

            await Navigation.PopAsync();

        }
    }
}