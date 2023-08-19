using c971.Models;
using c971.Services;
using c971.ViewModels;
using Plugin.LocalNotifications;
//using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace c971.Views
{
    public partial class Dashboard : ContentPage
    {
        public AcademicTermViewModel ViewModel { get; set; }

        bool firstRun = true;

        public Dashboard()
        {
            InitializeComponent();
            ViewModel = new AcademicTermViewModel();
            BindingContext = ViewModel;
            //TotalReset();
        }

        private void CheckNotifications()
        {
            var courseList = AdoNetDatabaseService.GetAllCourses();
            var assessmentList = AdoNetDatabaseService.GetAllAssessments();
            var notifyRandom = new Random();
            var notifyId = notifyRandom.Next(1000);
            foreach (Course course in courseList)
            {
                if (course.GetNotified == true)
                {
                    if (course.StartDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{ course.Name} begins today!", notifyId);
                    }
                    else if (course.EndDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{ course.Name} ends today.", notifyId);
                    }
                }
            }

            foreach (Assessment assessment in assessmentList)
            {
                if (assessment.EndDate == DateTime.Today)
                {
                    CrossLocalNotifications.Current.Show("Notice", $"{assessment.Name} is due today.", notifyId);
                }
            }
        }


        private async void OnAddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTerm());
        }

        private async void OnTermTapped(object sender, EventArgs e)
        {
            if (e is TappedEventArgs tappedEventArgs && tappedEventArgs.Parameter is AcademicTerm selectedTerm)
            {
                var termDetailsPage = new TermDetails(selectedTerm);
                await Navigation.PushAsync(termDetailsPage);
            }
        }

        protected override void OnAppearing()
        {
            var newTermList = AdoNetDatabaseService.GetAcademicTermsTableAsList();
            ViewModel.AcademicTerms.Clear();
            foreach (AcademicTerm term  in newTermList)
            {
                ViewModel.AcademicTerms.Add(term);
                Console.WriteLine("Added Term " + term.Name);
            }

            if (scroller != null)
            {
                scroller.ScrollToAsync(0, 0, false);
            }

            base.OnAppearing();
            if (firstRun)
            {
                CheckNotifications();
                firstRun = false;
            }
        }
    }
}
