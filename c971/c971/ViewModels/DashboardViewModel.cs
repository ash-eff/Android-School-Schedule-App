using c971.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace c971.ViewModels
{
    class DashboardViewModel : BaseViewModel
    {
        public ObservableCollection<AcademicTerm> AcademicTerms { get; set; }
        //public AcademicTerm SelectedTerm { get; set; }

        private AcademicTerm _selectedTerm;
        public AcademicTerm SelectedTerm
        {
            get { return _selectedTerm; }
            set
            {
                _selectedTerm = value;
                OnPropertyChanged(nameof(SelectedTerm));
            }
        }

        public DashboardViewModel()
        {
            AcademicTerms = new ObservableCollection<AcademicTerm>();
        }

        public void AddTerm(AcademicTerm term)
        {
            AcademicTerms.Add(term);
        }

        public void DeleteTerm(AcademicTerm term)
        {
            AcademicTerms.Remove(term);
        }
    }
}
