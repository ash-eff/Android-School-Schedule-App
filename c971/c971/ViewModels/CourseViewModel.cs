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

        public CourseViewModel()
        {
            Assessments = new ObservableCollection<Assessment>();
        }

        public CourseViewModel(Course course)
        {
            SelectedCourse = course;
            Assessments = new ObservableCollection<Assessment>();
        }

        public Course SelectedCourse { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
    }
}
