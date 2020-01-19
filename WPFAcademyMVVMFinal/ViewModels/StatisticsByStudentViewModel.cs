using Academy.Lib.Models;
using Common.Lib.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WPFAcademyMVVMFinal.Lib.UI;

namespace WPFAcademyMVVMFinal.ViewModels
{
    public class StatisticsByStudentViewModel : ViewModelBase
    {

        public StatisticsByStudentViewModel()
        {
            FindStudentSVMCommand = new RouteCommand(FindStudentSVM);
            GetStudentExamsSVMCommand = new RouteCommand(GetStudentExamsSVM);
            GetAllStudentExamsSVMCommand = new RouteCommand(GetAllStudentExamsSVM);
            AvgMarkSVMCommand = new RouteCommand(AvgMarkSVM);
            MaxMarkSVMCommand = new RouteCommand(MaxMarkSVM);
            MinMarkSVMCommand = new RouteCommand(MinMarkSVM);

        }


        public ICommand FindStudentSVMCommand { get; set; }
        public ICommand GetStudentExamsSVMCommand { get; set; }
        public ICommand GetAllStudentExamsSVMCommand { get; set; }
        public ICommand AvgMarkSVMCommand { get; set; }
        public ICommand MaxMarkSVMCommand { get; set; }
        public ICommand MinMarkSVMCommand { get; set; }


        #region Propierties

        private string _dniSVM;
        public string DniSVM
        {
            get { return _dniSVM; }
            set
            {
                _dniSVM = value;
                OnPropertyChanged();
            }
        }

        private string _nameSVM;
        public string NameSVM
        {
            get { return _nameSVM; }
            set
            {
                _nameSVM = value;
                OnPropertyChanged();
            }
        }


        private Student _currentStudentSVM;
        public Student CurrentStudentSVM  
        {
            get { return _currentStudentSVM; }
            set
            {
                _currentStudentSVM = value;
                OnPropertyChanged();
            }
        }

        private string _currentSubjectNameSVM;
        public string CurrentSubjectNameSVM
        {
            get
            {
                return _currentSubjectNameSVM;
            }
            set
            {
                _currentSubjectNameSVM = value;
                OnPropertyChanged();
            }
        }


        private string _errorsSVM;
        public string ErrorsSVM
        {
            get
            {
                return _errorsSVM;
            }
            set
            {
                _errorsSVM = value;
                OnPropertyChanged();
            }
        }


        private Subject _currentSubjectSVM;
        public Subject CurrentSubjectSVM  
        {
            get { return _currentSubjectSVM; }
            set
            {
                _currentSubjectSVM = value;
                OnPropertyChanged();
            }
        }

        private double _markSVM;
        public double MarkSVM
        {
            get
            {
                return _markSVM;
            }
            set
            {
                _markSVM = value;
                OnPropertyChanged();
            }

        }


        List<string> _subjectsNameListSVM;
        public List<string> SubjectsNameListSVM
        {
            get
            {
                return _subjectsNameListSVM;
            }
            set
            {
                _subjectsNameListSVM = value;
                OnPropertyChanged();
            }

        }

        List<StudentSubject> _subjectsByStudentList;
        public List<StudentSubject> SubjectsByStudentList   
        {
            get
            {
                return _subjectsByStudentList;
            }
            set
            {
                _subjectsByStudentList = value;

                OnPropertyChanged();
            }
        }

        List<StudentExam> _studentExamsListSVM;
        public List<StudentExam> StudentExamsListSVM
        {
            get
            {
                return _studentExamsListSVM;
            }
            set
            {
                _studentExamsListSVM = value;
                OnPropertyChanged();
            }
        }

        List<StudentExam> _studentExamsBySubjectListSVM;
        public List<StudentExam> StudentExamsBySubjectListSVM
        {
            get
            {
                return _studentExamsBySubjectListSVM;
            }
            set
            {
                _studentExamsBySubjectListSVM = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Métodos de Búsqueda
        private void FindStudentSVM()   
        {
            ErrorsSVM = "";
            var studentsVM = new StudentsViewModel();
            StudentSubject studentSubjectMVM = new StudentSubject();


            studentsVM.GetStudents();

            CurrentStudentSVM = studentsVM.StudentsListNou.FirstOrDefault(x => x.Dni == DniSVM);

            if (CurrentStudentSVM != null)
            {
                DniSVM = CurrentStudentSVM.Dni;
                NameSVM = CurrentStudentSVM.Name;

                GetSubjectsNameEV();
            }

            else
            {
                ErrorsSVM = "Student no Existe";
                Student student = new Student();
                CurrentStudentSVM = student;

                GetSubjectsNameEV();

                DniSVM = "";
                NameSVM = "";

            }
        }

        public void GetSubjectsToStudent()  
        {
            Student student = new Student();
            StudentSubject studentSubjectSVM = new StudentSubject();

            if (CurrentStudentSVM != null)
            {
                student = CurrentStudentSVM;
                studentSubjectSVM.StudentId = student.Id;

                SubjectsByStudentList = studentSubjectSVM.StudentBySubjects(studentSubjectSVM.StudentId);

            }
        }

        public List<string> GetSubjectsByNameEV()  
        {
            GetSubjectsToStudent();
            List<string> SubjectsNameListEV = new List<string>();
            foreach (StudentSubject subj in SubjectsByStudentList)
            {
                var name = subj.Subject.Name;
                SubjectsNameListEV.Add(name);
            }
            return SubjectsNameListEV;
        }


        public void GetSubjectsNameEV()  
        {
            SubjectsNameListSVM = GetSubjectsByNameEV();
        }


        bool exams = false;
        public void GetStudentExamsSVM()  
        {
            ErrorsSVM = "";

            Student student = new Student();
            Exam exam = new Exam();
            StudentExam studentExam = new StudentExam();

            if (CurrentStudentSVM != null)
            {

                student = CurrentStudentSVM;
                studentExam.StudentId = student.Id;

                StudentExamsListSVM = studentExam.StudentByExams(studentExam.StudentId);
                if (CurrentSubjectNameSVM != null)
                {
                    StudentExamsBySubjectListSVM = StudentExamsListSVM.FindAll(x => x.Exam.Subject.Name == CurrentSubjectNameSVM).ToList();
                    exams = true;
                }
                else
                {
                    StudentExamsBySubjectListSVM = StudentExamsListSVM;
                    exams = true;
                }

                if (StudentExamsBySubjectListSVM.Count == 0)
                {
                    ErrorsSVM = "El alumno no ha realizado exámenes de " + CurrentSubjectNameSVM;
                    exams = false;

                }
            }


        }

        public void GetAllStudentExamsSVM()
        {
            CurrentSubjectNameSVM = null;
            ErrorsSVM = "";
            GetStudentExamsSVM();
            exams = true;
        }

        #endregion


        #region  Statistics
        public void AvgMarkSVM()
        {
            MarkSVM = 0;
            var marksList = new List<double>();
            marksList = MarksListSVM();
            
            if (marksList ==null) { }
            
            else
                MarkSVM = marksList.Average();

        }

        public void MaxMarkSVM()
        {
            MarkSVM = 0;
            var marksList = new List<double>();
            marksList = MarksListSVM();

            if (marksList == null) { }

            else
                MarkSVM = marksList.Max() ;

        }
        

        public void MinMarkSVM()
        {
            MarkSVM = 0;
            var marksList = new List<double>();
            marksList = MarksListSVM();

            if (marksList == null) { }

            else
                MarkSVM = marksList.Min();

        }


        public List<double> MarksListSVM()
        {
            ErrorsSVM = "";
            if (exams)
            {

                if (CurrentStudentSVM != null)
                {
                    if (CurrentSubjectNameSVM != null)
                    {
                        var marksList = new List<double>();

                        foreach (StudentExam stuEx in StudentExamsBySubjectListSVM)
                        {
                            marksList.Add(stuEx.Mark);
                        }

                        return marksList;
                        
                    }

                    else
                    {
                        var marksList = new List<double>();

                        foreach (StudentExam stuEx in StudentExamsListSVM)
                        {
                            marksList.Add(stuEx.Mark);
                        }

                        return marksList;
                    }

                }
                else
                {
                    ErrorsSVM = "No hay ningún Student seleccionado";
                    return null;
                }
            }

            else
            {
                ErrorsSVM = "No hay exámenes para calcular";
                return null;
            }
        }
        #endregion
    }
}
