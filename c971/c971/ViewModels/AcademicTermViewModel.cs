using c971.Models;
using c971.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace c971.ViewModels
{
    public  class AcademicTermViewModel : BaseViewModel
    {
        public ObservableCollection<AcademicTerm> AcademicTerms { get; set; }

        public DateTime NewCourseStartDate { get; set; } = DateTime.Today;
        public DateTime NewCourseEndDate { get; set; } = DateTime.Today;

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

        public AcademicTermViewModel()
        {
            AcademicTerms = new ObservableCollection<AcademicTerm>();
        }

        public void AddTerm(AcademicTerm term)
        {
            AcademicTerms.Add(term);
        }

        //public void AddCourse(AcademicTerm term, Course course)
        //{
        //    course.TermId = term.Id;
        //    term.Courses.Add(course);
        //}

        //public void RemoveCourse(AcademicTerm term, Course course)
        //{
        //    term.Courses.Remove(course);
        //    // Update the database?
        //}

        public void DeleteTerm(AcademicTerm term)
        {
            AcademicTerms.Remove(term);
        }

        public void UpdateTerm(AcademicTerm termToUpdate)
        {
            int index = AcademicTerms.IndexOf(termToUpdate);

            if (index != -1)
            {
                AcademicTerms[index] = termToUpdate;
            }
        }

        public async Task LoadTerms()
        {
           // List<AcademicTerm> termsFromDatabase = await App.Database.GetAcademicTermsAsync();
           // AcademicTerms.Clear();
           // foreach (var term in termsFromDatabase)
           // {
           //     AcademicTerms.Add(term);
           // }
           // OnPropertyChanged(nameof(AcademicTerm));
        }

    }

}
