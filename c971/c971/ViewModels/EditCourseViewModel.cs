using c971.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace c971.ViewModels
{
    public class EditCourseViewModel
    {
        public EditCourseViewModel()
        {

        }
        public EditCourseViewModel(Course course)
        {
            SelectedCourse = course;
            EditedCourseName = course.Name;
            EditedStartDate = course.StartDate;
            EditedEndDate = course.EndDate;
            EditedStatus = course.CourseStatus;
            EditedInstructorName = course.InstructorName;
            EditedInstructorPhone = course.InstructorPhone;
            EditedInstructorEmail = course.InstructorEmail;
            EditedNotes = course.Notes;
        }
        public Course SelectedCourse { get; set; }
        public string EditedCourseName { get; set; }
        public DateTime EditedStartDate { get; set; }
        public DateTime EditedEndDate { get; set; }
        public string EditedStatus { get; set; }
        public string EditedInstructorName { get; set; }
        public string EditedInstructorPhone { get; set; }
        public string EditedInstructorEmail { get; set; }
        public string EditedNotes { get; set; }

        public Course UpdateCourse()
        {
            SelectedCourse.Name = EditedCourseName;
            SelectedCourse.StartDate = EditedStartDate;
            SelectedCourse.EndDate = EditedEndDate;
            SelectedCourse.CourseStatus = EditedStatus;
            SelectedCourse.InstructorName = EditedInstructorName;
            SelectedCourse.InstructorPhone = EditedInstructorPhone;
            SelectedCourse.InstructorEmail = EditedInstructorEmail;
            SelectedCourse.Notes = EditedNotes;
            return SelectedCourse;
        }

        public List<string> CourseStatusOptions { get; } = new List<string>
        {
            "Not Started",
            "In Progress",
            "Completed"
        };
    }
}
