using c971.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace c971.ViewModels
{
    public class EditAssessmentViewModel
    {
        public EditAssessmentViewModel()
        {

        }
        public EditAssessmentViewModel(Assessment assessment)
        {
            SelectedAssessment = assessment;
            EditedAssessmentName = assessment.Name;
            EditedStartDate = assessment.StartDate;
            EditedEndDate = assessment.EndDate;
            EditedGetNotifications = assessment.GetNotified;
        }
        public Assessment SelectedAssessment { get; set; }
        public string EditedAssessmentName { get; set; }
        public DateTime EditedStartDate { get; set; }
        public DateTime EditedEndDate { get; set; }
        public bool EditedGetNotifications { get; set; }

        public Assessment UpdateAssessment()
        {
            SelectedAssessment.Name = EditedAssessmentName;
            SelectedAssessment.StartDate = EditedStartDate;
            SelectedAssessment.EndDate = EditedEndDate;
            SelectedAssessment.GetNotified = EditedGetNotifications;
            return SelectedAssessment;
        }

 
    }
}
