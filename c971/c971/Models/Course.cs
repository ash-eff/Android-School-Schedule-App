using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace c971.Models
{
    public class Course
    {
        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public List<Assessment> Assessments { get; set; }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CourseStatus { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }

        [ForeignKey(typeof(AcademicTerm))]
        public int TermId { get; set; }
        public string Notes { get; set; }
        public bool GetNotified { get; set; }
        public List<string> CourseStatusOptions { get; } = new List<string>
        {
            "Not Started",
            "In Progress",
            "Completed"
        };
    }
}
