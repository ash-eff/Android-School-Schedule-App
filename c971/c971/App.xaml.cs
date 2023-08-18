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

namespace c971
{
    public partial class App : Application
    {
        //public static AdoNetDatabaseService Database { get; private set; }
        public static string dbPath;
        public static Color DeleteColor = Color.FromHex("#a0615f");
        public static Color EditColor = Color.FromHex("#5F9EA0");
        public static Color AddColor = Color.FromHex("#3a7f98");
        public static Color Background = Color.FromHex("#ece6df");
        public static Color ContainerColor = Color.FromHex("#e2d9cf");


        public App(string completePath)
        {
            InitializeComponent();

            var dashBoard = new Dashboard();
            var navPage = new NavigationPage(dashBoard);
            MainPage = navPage;
            dbPath = completePath;

            //AdoNetDatabaseService.InitializeDatabase();
            //PopulateDataBase(6);
        }

        private void PopulateDataBase(int numberOfTerms)
        {
            int numberOfCourses = 6;
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
                        InstructorPhone = "555-555-555",
                        InstructorEmail = "afisc38@wgu.edu",
                        GetNotified = true,
                        TermId = term.Id,
                    };

                    courseInitial += courseOffset;

                    AdoNetDatabaseService.SaveCourse(course);

                    var objectiveAssessment = new Assessment
                    {
                        Name = $"Objective Assessment {j}",
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
                        Name = $"Performance Assessment {j}",
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
