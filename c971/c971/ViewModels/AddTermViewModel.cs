using c971.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace c971.ViewModels
{
    public class AddTermViewModel : BaseViewModel
    {
        private ObservableCollection<Course> _selectedCourses;
        public ObservableCollection<Course> SelectedCourses
        {
            get { return _selectedCourses; }
            set
            {
                _selectedCourses = value;
                OnPropertyChanged(nameof(SelectedCourses));
            }
        }

        public AddTermViewModel()
        {
            // Initialize the SelectedCourses collection
            SelectedCourses = new ObservableCollection<Course>();
        }

    }
}
