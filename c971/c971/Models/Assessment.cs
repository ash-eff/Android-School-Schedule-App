using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace c971.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CourseId { get; set; }

        public AssessmentType Type { get; set; }
        public bool GetNotified { get; set; }

        public enum AssessmentType
        {
            Objective,
            Performance
        }
    }
}
