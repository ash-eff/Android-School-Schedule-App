using c971.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace c971.ViewModels
{
    public class CourseViewModel : BaseViewModel
    {
        private Course _course;

        public Course Course
        {
            get => _course;
            set
            {
                _course = value;
                OnPropertyChanged(); // Notify UI of property change
            }
        }

        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }

        public CourseViewModel()
        {
            Course = new Course(); // Initialize with a new course
            SaveCommand = new Command(SaveCourse);
            DeleteCommand = new Command(DeleteCourse);
        }

        private void SaveCourse()
        {
            // Logic to save the course using the data service
        }

        private void DeleteCourse()
        {
            // Logic to delete the course using the data service
        }
    }

}
