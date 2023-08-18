using c971.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace c971.ViewModels
{
    public class CourseViewModel : BaseViewModel
    {
        public ObservableCollection<Assessment> Assessments { get; set; }
        private string _courseDetailsText;
        public CourseViewModel()
        {
            //Assessments = new ObservableCollection<Assessment>();
        }

        public CourseViewModel(Course course)
        {
            Assessments = new ObservableCollection<Assessment>();
            SelectedCourse = course;
            CourseName = course.Name;
            StartDate = course.StartDate;
            EndDate = course.EndDate;
            Status = course.CourseStatus;
            InstructorName = course.InstructorName;
            InstructorPhone = course.InstructorPhone;
            InstructorEmail = course.InstructorEmail;
            CourseNotes = course.Notes;
        }

        public string CourseDetailsText
        {
            get { return _courseDetailsText; }
            set
            {
                if (_courseDetailsText != value)
                {
                    _courseDetailsText = value;
                    OnPropertyChanged(nameof(CourseDetailsText));
                }
            }
        }

        public Course SelectedCourse { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string CourseNotes { get; set; }
    }
}
