using c971.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace c971.ViewModels
{
    public class AssessmentViewModel
    {
        public Assessment Assessment;
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CourseId { get; set; }
        public AssessmentType Type { get; set; }
        public bool GetNotified { get; set; }

        public AssessmentViewModel()
        {
            //Assessments = new ObservableCollection<Assessment>();
        }

        public AssessmentViewModel(Assessment assessment)
        {
            Assessment = assessment;
            Name = assessment.Name;
            StartDate = assessment.StartDate;
            EndDate = assessment.EndDate;
            CourseId = assessment.CourseId;
            Type = (AssessmentType)assessment.Type;
            GetNotified = assessment.GetNotified;
        }

        public Assessment CreateAssessment()
        {
            Assessment newAssessment = new Assessment
            {
                Name = Name,
                StartDate = StartDate,
                EndDate = EndDate,
                CourseId = CourseId,
                Type = (Assessment.AssessmentType)Type,
                GetNotified = GetNotified
            };

            return newAssessment;
        }
        public enum AssessmentType
        {
            Objective,
            Performance
        }
    }
}
