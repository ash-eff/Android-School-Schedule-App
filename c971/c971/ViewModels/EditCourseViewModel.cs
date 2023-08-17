using c971.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace c971.ViewModels
{
    public class EditCourseViewModel
    {
        public Course SelectedCourse { get; set; }
        public string EditedCourseName { get; set; }
        public DateTime EditedStartDate { get; set; }
        public DateTime EditedEndDate { get; set; }
        public string EditedStatus { get; set; }
        public string EditedInstructorName { get; set; }
        public string EditedInstructorPhone { get; set; }
        public string EditedInstructorEmail { get; set; }
    }
}
