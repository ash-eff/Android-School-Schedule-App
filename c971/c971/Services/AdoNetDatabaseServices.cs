using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using c971.Models;
using SQLite;

namespace c971.Services
{
    public class AdoNetDatabaseService
    {

        public static void TableInformation()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                string tableName = GetTableName<AcademicTerm>();
                List<string> columnNames = GetTableColumnNames(tableName, connection);

                if (columnNames.Count > 0)
                {
                    Console.WriteLine($"<----------------------------------Columns for table {tableName}:");
                    foreach (string columnName in columnNames)
                    {
                        Console.WriteLine("<----------------------------------Column name: " + columnName);
                    }
                }
                else
                {
                    Console.WriteLine($"<----------------------------------Table {tableName} does not exist or has no columns.");
                }
            }

        }

        public static void InitializeDatabase()
        {
            DropAndRecreateTables();
        }

        public static void SaveAcademicTerm(AcademicTerm term)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                if (term.Id == 0)
                {
                    connection.Insert(term);
                }
                else
                {
                    connection.Update(term);
                }
            }
        }

        public static void RemoveAcademicTerm(AcademicTerm term)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                if (term.Id != 0)
                {
                    connection.Delete(term);
                }
            }
        }

        public static void SaveCourse(Course course)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                if (course.Id == 0)
                {
                    connection.Insert(course);
                }
                else
                {
                    connection.Update(course);
                }
            }
        }

        public static void RemoveCourse(Course course)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                if (course.Id != 0)
                {
                    connection.Delete(course);
                }
            }
        }

        public static void SaveAssessment(Assessment assessment)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                if (assessment.Id == 0)
                {
                    connection.Insert(assessment);
                }
                else
                {
                    connection.Update(assessment);
                }
            }
        }

        public static void RemoveAssessment(Assessment assessment)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                if (assessment.Id != 0)
                {
                    connection.Delete(assessment);
                }
            }
        }

        public static List<AcademicTerm> GetAcademicTermsTableAsList()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                return connection.Table<AcademicTerm>().ToList();
            }
        }

        public static List<Course> GetCourseTableAsListForTerm(AcademicTerm term)
        {
            if (term == null)
            {
                Console.WriteLine("Term is null!");
                return new List<Course>();
            }

            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                var coursesForTerm = connection.Table<Course>()
                                              .Where(course => course.TermId == term.Id)
                                              .ToList();

                return coursesForTerm;
            }
        }

        public static Course GetCourseById(int courseId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                return connection.Table<Course>()
                                 .FirstOrDefault(course => course.Id == courseId);
            }
        }

        public static List<Assessment> GetAssessmentTableAsListForCourse(Course course)
        {
            if (course == null)
            {
                Console.WriteLine("Course is null!");
                return new List<Assessment>();
            }

            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                var assessmentsForCourse = connection.Table<Assessment>()
                                              .Where(assessment => assessment.CourseId == course.Id)
                                              .ToList();

                return assessmentsForCourse;
            }
        }

        public static string GetTableName<T>()
        {
            var connection = new SQLiteConnection(App.dbPath);
            TableMapping map = connection.GetMapping<T>();
            return map.TableName;
        }

        public static bool TableExists<T>(SQLiteConnection connection)
        {
            string tableName = typeof(T).Name;
            const string query = "SELECT name FROM sqlite_master WHERE type='table' AND name=?";
            var result = connection.ExecuteScalar<string>(query, tableName);
            return !string.IsNullOrEmpty(result);
        }

        public static List<string> GetTableColumnNames(string tableName, SQLiteConnection connection)
        {
            string query = $"PRAGMA table_info({tableName})";
            var result = connection.ExecuteScalar<string>(query);

            if (!string.IsNullOrEmpty(result))
            {
                var columnNames = result.Split(',').Select(name => name.Trim()).ToList();
                return columnNames;
            }
            else
            {
                return new List<string>();
            }
        }
        private static void DropAndRecreateTables()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                connection.DropTable<Assessment>();
                connection.DropTable<Course>();
                connection.DropTable<AcademicTerm>();

                connection.CreateTable<AcademicTerm>();
                connection.CreateTable<Course>();
                connection.CreateTable<Assessment>();
            }
        }
    }
}
