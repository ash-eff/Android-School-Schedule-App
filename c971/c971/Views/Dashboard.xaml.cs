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
        public List<Assessment> assessmentsList = new List<Assessment>();
        public AcademicTermViewModel ViewModel { get; set; }

        bool firstRun = true;

        public Dashboard()
        {
            InitializeComponent();
            ViewModel = new AcademicTermViewModel();
            BindingContext = ViewModel;
            
            //TotalReset();
            
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


        }

        //private void runAlerts()
        //{
        //    foreach (AcademicTerm term in termsList)
        //    {
        //        using (SQLiteConnection con = new SQLiteConnection(App.dbPath))
        //        {
        //            var courses = con.Query<Course>($"SELECT * FROM Courses WHERE Term = '{term.Id}'");
        //            foreach (Course course in courses)
        //            {
        //                // Check for courses starting within 3 days
        //                if ((course.StartDate - DateTime.Now).TotalDays < 3)
        //                {
        //                    CrossLocalNotifications.Current.Show("Course Starting Soon", $"{course.Name} is starting on {course.StartDate.Date.ToString()}");
        //                }
        //                // Check for courses ending within 7 days
        //                if ((course.EndDate - DateTime.Now).TotalDays < 7)
        //                {
        //                    CrossLocalNotifications.Current.Show("Course Ending Soon", $"{course.Name} is ending on {course.EndDate.Date.ToString()}");
        //                }
        //
        //                //Check for assessments that are coming up within 3 days
        //                var assessments = con.Query<Assessment>($"SELECT * FROM Assessments WHERE Course = '{course.Id}'");
        //                foreach (Assessment assessment in assessments)
        //                {
        //                    if ((assessment.End - DateTime.Now).TotalDays < 3 && assessment.GetNotified == 1)
        //                    {
        //                        CrossLocalNotifications.Current.Show("Assessment Due Soon", $"{assessment.AssessmentName} is starting on {assessment.End.Date.ToString()}");
        //                    }
        //                }
        //
        //            }
        //        }
        //    }
        //
        //
        //}

        //private void CreateEvaluationData(int termNumber)
        //{
        //    Term newTerm = new Term();
        //    newTerm.TermName = "Term " + termNumber.ToString();
        //    newTerm.Start = new DateTime(2021, 06, 01);
        //    newTerm.End = new DateTime(2021, 11, 30);
        //    using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
        //    {
        //        con.Insert(newTerm);
        //    }
        //    ////       ----SAMPLE COURSE----
        //    Course newCourse = new Course();
        //    newCourse.Term = newTerm.Id;
        //    newCourse.CourseName = "Intro To Philosophy";
        //    newCourse.CourseStatus = "Plan To Take";
        //    newCourse.Start = new DateTime(2021, 08, 12);
        //    newCourse.End = new DateTime(2021, 10, 12);
        //    newCourse.InstructorName = "Joseph Kennedy";
        //    newCourse.InstructorEmail = "jken252@wgu.edu";
        //    newCourse.InstructorPhone = "620-412-6906";
        //    newCourse.Notes = "Welcome to class";
        //    newCourse.GetNotified = 1;
        //    using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
        //    {
        //        con.Insert(newCourse);
        //    }
        //    ////       ----SAMPLE OBJECTIVE ASSESSMENT----
        //    Assessment newObjectiveAssessment = new Assessment();
        //    newObjectiveAssessment.AssessmentName = "BOP1";
        //    newObjectiveAssessment.Start = new DateTime(2021, 09, 10);
        //    newObjectiveAssessment.End = new DateTime(2021, 09, 10);
        //    newObjectiveAssessment.AssessType = "Objective";
        //    newObjectiveAssessment.Course = newCourse.Id;
        //    newObjectiveAssessment.GetNotified = 0;
        //    using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
        //    {
        //        con.Insert(newObjectiveAssessment);
        //    }
        //    ////       ----SAMPLE PERFORMANCE ASSESSMENT----
        //    Assessment newPerformanceAssessment = new Assessment();
        //    newPerformanceAssessment.AssessmentName = "LAG1";
        //    newPerformanceAssessment.Start = new DateTime(2021, 10, 13);
        //    newPerformanceAssessment.End = new DateTime(2021, 10, 13);
        //    newPerformanceAssessment.AssessType = "Performance";
        //    newPerformanceAssessment.Course = newCourse.Id;
        //    newPerformanceAssessment.GetNotified = 0;
        //    using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
        //    {
        //        con.Insert(newPerformanceAssessment);
        //    }
        //}
    }
}
