using System;
using System.Collections.Generic;
using System.Text;
using c971.Models;

namespace c971.ViewModels
{
    public class EditTermViewModel
    {
        public AcademicTerm SelectedTerm { get; set; }
        public string EditedTermName { get; set; }
        public DateTime EditedStartDate { get; set; }
        public DateTime EditedEndDate { get; set; }
    }
}