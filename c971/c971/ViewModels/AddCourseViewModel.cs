using c971.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace c971.ViewModels
{
    public class AddCourseViewModel : BaseViewModel
    {
        public AddCourseViewModel()
        {
            // Set the default value for AddedStatus
            AddedStatus = "Not Started";
        }

        public string AddedName { get; set; }
        public string AddedCourseName { get; set; }
        public DateTime AddedStartDate { get; set; }
        public DateTime AddedEndDate { get; set; }
        public string AddedStatus { get; set; }
        public string AddedInstructorName { get; set; }
        public string AddedInstructorPhone { get; set; }
        public string AddedInstructorEmail { get; set; }
        public int AddedTermId { get; set; }
        public string AddedNotes { get; set; }
        public bool AddedGetNotified { get; set; }

        public List<string> CourseStatusOptions { get; } = new List<string>
        {
            "Not Started",
            "In Progress",
            "Completed"
        };

        public Course CreateCourse()
        {
            Course newCourse = new Course
            {
                Name = AddedName,
                StartDate = AddedStartDate,
                EndDate = AddedEndDate,
                CourseStatus = AddedStatus,
                InstructorName = AddedInstructorName,
                InstructorPhone = AddedInstructorPhone,
                InstructorEmail = AddedInstructorEmail,
                TermId = AddedTermId,
                Notes = AddedNotes,
                GetNotified = AddedGetNotified
            };

            return newCourse;
        }
    }
}
