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
    public partial class EditTerm : ContentPage
    {
        AcademicTerm SelectedTerm { get; set; }

        public EditTerm(EditTermViewModel viewModel)
        {
            InitializeComponent();
            SelectedTerm = viewModel.SelectedTerm;

            editedNameEntry.Text = SelectedTerm.Name;
            datePickerStartDate.Date = SelectedTerm.StartDate;
            datePickerEndDate.Date = SelectedTerm.EndDate;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }

            SelectedTerm.Name = editedNameEntry.Text;
            SelectedTerm.StartDate = datePickerStartDate.Date;
            SelectedTerm.EndDate = datePickerEndDate.Date;

            AdoNetDatabaseService.SaveAcademicTerm(SelectedTerm);

            await DisplayAlert("Success", "Term saved successfully!", "OK");

            await Navigation.PopToRootAsync();
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(editedNameEntry.Text))
            {
                DisplayAlert("Error", "Term name cannot be empty.", "OK");
                return false;
            }

            if (datePickerEndDate.Date <= datePickerStartDate.Date)
            {
                DisplayAlert("Error", "End date cannot be before or on the same day as the start date.", "OK");
                return false;
            }

            return true;
        }
    }
}