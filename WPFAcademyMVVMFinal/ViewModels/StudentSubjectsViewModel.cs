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
    public class StudentSubjectsViewModel : ViewModelBase
    {
        public StudentSubjectsViewModel()
        {
            AddSubjectToListVMCommand = new RouteCommand(AddSubjectToListVM);
            FindStudentCommand = new RouteCommand(FindStudent);
            GetSubjectsMGVMCommand = new RouteCommand(GetSubjectsMGVM);
            DelSubjectToListVMCommand = new RouteCommand(DelSubjectToListVM);
            GetSubjectsToStudentVMCommand = new RouteCommand(GetSubjectsToStudent);
        }


        public ICommand AddSubjectToListVMCommand { get; set; }
        public ICommand FindStudentCommand { get; set; }
        public ICommand GetSubjectsMGVMCommand { get; set; }
        public ICommand DelSubjectToListVMCommand { get; set; }
        public ICommand GetSubjectsToStudentVMCommand { get; set; }



        private string _dniMGVM;
        public string DniMGVM
        {
            get { return _dniMGVM; }
            set
            {
                _dniMGVM = value;
                OnPropertyChanged();
            }
        }

        private string _nameMGVM;
        public string NameMGVM
        {
            get { return _nameMGVM; }
            set
            {
                _nameMGVM = value;
                OnPropertyChanged();
            }
        }

        private string _subjectNameMGVM;
        public string SubjectNameMGVM
        {
            get { return _subjectNameMGVM; }
            set
            {
                _subjectNameMGVM = value;
                OnPropertyChanged();
            }
        }



        private Student _currentStudentMVM;
        public Student CurrentStudentMVM  
        {
            get { return _currentStudentMVM; }
            set
            {
                _currentStudentMVM = value;
                OnPropertyChanged();
            }
        }

        private Subject _currentSubjectMVM;
        public Subject CurrentSubjectMVM  
        {
            get { return _currentSubjectMVM; }
            set
            {
                _currentSubjectMVM = value;
                OnPropertyChanged();
            }
        }

        private StudentSubject _currentStudentSubjectMVM;
        public StudentSubject CurrentStudentSubjectMVM  
        {
            get { return _currentStudentSubjectMVM; }
            set
            {
                _currentStudentSubjectMVM = value;
                OnPropertyChanged();
            }
        }


        List<Subject> _subjectsListMGVM;
        public List<Subject> SubjectListMGVM  
        {
            get
            {
                return _subjectsListMGVM;
            }
            set
            {
                _subjectsListMGVM = value;
                OnPropertyChanged();
            }
        }


        List<Subject> _subjectsByNameList;
        public List<Subject> SubjectsByNameList   
        {
            get
            {
                return _subjectsByNameList;
            }
            set
            {
                _subjectsByNameList = value;

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


        List<ErrorMessage> _errorsList;  
        public List<ErrorMessage> ErrorsList
        {
            get
            {
                return _errorsList;
            }
            set
            {
                _errorsList = value;
                OnPropertyChanged();
            }
        }




        public void AddSubjectToListVM()
        {
            Subject subject = new Subject();
            Student student = new Student();
            StudentSubject studentSubjectMVM = new StudentSubject();

            subject = CurrentSubjectMVM;
            student = CurrentStudentMVM;

            if (CurrentStudentMVM != null)
            {
                studentSubjectMVM.StudentId = student.Id;

                if (CurrentSubjectMVM != null)
                {
                    studentSubjectMVM.SubjectId = subject.Id;
                }
            }

            studentSubjectMVM.Save();

            ErrorsList = studentSubjectMVM.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();  //Nou

            if (CurrentStudentMVM != null)
            {
                GetSubjectsToStudent();
            }
        }


        public void DelSubjectToListVM()   
        {
            StudentSubject studentSubjectMVM = new StudentSubject();

            if (CurrentStudentSubjectMVM == null)
            {
                studentSubjectMVM.Delete();
                ErrorsList = studentSubjectMVM.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();

            }

            else
            {
                ErrorsList = new List<ErrorMessage>();
                studentSubjectMVM = CurrentStudentSubjectMVM;

                studentSubjectMVM.Delete();
                ErrorsList = studentSubjectMVM.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();

                SubjectsByStudentList = studentSubjectMVM.StudentBySubjects(studentSubjectMVM.StudentId);
                
                GetSubjectsToStudent();

            }
        }


        public void GetSubjectsToStudent()  
        {
            Student student = new Student();
            StudentSubject studentSubjectMVM = new StudentSubject();

            if (CurrentStudentMVM !=null)
            {
                student = CurrentStudentMVM;
                studentSubjectMVM.StudentId = student.Id;

                SubjectsByStudentList = studentSubjectMVM.StudentBySubjects(studentSubjectMVM.StudentId);

            }
        }

        private void FindStudent()   
        {
            var studentsVM = new StudentsViewModel();
            StudentSubject studentSubjectMVM = new StudentSubject();


            studentsVM.GetStudents();

            CurrentStudentMVM = studentsVM.StudentsListNou.FirstOrDefault(x => x.Dni == DniMGVM);

            if (CurrentStudentMVM != null)
            {
                DniMGVM = CurrentStudentMVM.Dni;
                NameMGVM = CurrentStudentMVM.Name;
                ErrorsList = new List<ErrorMessage>();

                GetSubjectsToStudent();
            }

            else
            {
                NameMGVM = "Student no Existe";
                Student student = new Student();
                CurrentStudentMVM = student;
                ErrorsList = new List<ErrorMessage>();

                GetSubjectsToStudent();
                DniMGVM = "";

            }
        }


        public void GetSubjectsMGVM()    
        {
            var subject = new Subject();
            var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
            SubjectListMGVM = repo.QueryAll().ToList();
        }


    }

}

