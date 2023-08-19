using c971.Views;
using c971.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using c971.ViewModels;
using c971.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace c971
{
    public partial class App : Application
    {
        //public static AdoNetDatabaseService Database { get; private set; }
        public static string dbPath;
        public static Color DeleteColor = Color.FromHex("#a0615f");
        public static Color EditColor = Color.FromHex("#5F9EA0");
        public static Color AddColor = Color.FromHex("#839bad");
        public static Color Background = Color.FromHex("#f4f5f5");
        public static Color ContainerColor = Color.FromHex("#e8ebea");


        public App(string completePath)
        {
            InitializeComponent();
            dbPath = completePath;

            if (!Application.Current.Properties.ContainsKey("IsFirstRun"))
            {
                AdoNetDatabaseService.InitializeDatabase();
                PopulateDataBase(6, 6);

                Application.Current.Properties["IsFirstRun"] = true;
                Application.Current.SavePropertiesAsync();
            }

            var dashBoard = new Dashboard();
            var navPage = new NavigationPage(dashBoard);
            MainPage = navPage;

            MessagingCenter.Subscribe<Application, string>(Current, "ShareCourseNotes", (sender, courseNotes) =>
            {
                Sms.ComposeAsync(new SmsMessage(courseNotes, ""));
            });
        }


        public static void PopulateDataBase(int numberOfTerms, int numberOfCourses)
        {
            int termInitial = 0;
            int termOffset = 90;
            int courseInitial = 0;
            int courseOffset = 15;
            int performanceInitial = 0;
            int objectiveInitial = 8;
            int assessmentOffset = 7;

            for (int i = 1; i < numberOfTerms + 1; i++)
            {
                var term = new AcademicTerm
                {
                    Name = $"Term {i}",
                    StartDate = DateTime.Today.AddDays(termInitial),
                    EndDate = DateTime.Today.AddDays(termInitial + termOffset)
                };

                AdoNetDatabaseService.SaveAcademicTerm(term);

                for(int j = 1; j < numberOfCourses + 1; j++)
                {
                    var course = new Course
                    {
                        Name = $"Course {j}",
                        StartDate = DateTime.Today.AddDays(courseInitial),
                        EndDate = DateTime.Today.AddDays(courseInitial + courseOffset),
                        InstructorName = "Ash Fischer",
                        InstructorPhone = "555-555-5555",
                        InstructorEmail = "afisc38@wgu.edu",
                        CourseStatus = "Not Started",
                        Notes = "These are placement course notes...",
                        GetNotified = true,
                        TermId = term.Id,
                    };

                    courseInitial += courseOffset;

                    AdoNetDatabaseService.SaveCourse(course);

                    var objectiveAssessment = new Assessment
                    {
                        Name = $"Assessment {j}",
                        StartDate = DateTime.Today.AddDays(objectiveInitial),
                        EndDate = DateTime.Today.AddDays(objectiveInitial + assessmentOffset),
                        Type = Assessment.AssessmentType.Objective,
                        GetNotified = true,
                        CourseId = course.Id
                    };

                    objectiveInitial += assessmentOffset;

                    AdoNetDatabaseService.SaveAssessment(objectiveAssessment);

                    var performaceAssessment = new Assessment
                    {
                        Name = $"Assessment {j + 1}",
                        StartDate = DateTime.Today.AddDays(performanceInitial),
                        EndDate = DateTime.Today.AddDays(performanceInitial + assessmentOffset),
                        Type = Assessment.AssessmentType.Performance,
                        GetNotified = true,
                        CourseId = course.Id
                    };

                    performanceInitial += assessmentOffset;

                    AdoNetDatabaseService.SaveAssessment(performaceAssessment);

                }

                termInitial += termOffset + 1;
                courseInitial = termInitial;
                performanceInitial = termInitial;
                objectiveInitial = termInitial;
            }
        }
    }
}
