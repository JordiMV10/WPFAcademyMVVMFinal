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

        private string _managementErrorMGVM;
        public string ManagementErrorMGVM
        {
            get { return _managementErrorMGVM; }
            set
            {
                _managementErrorMGVM = value;
                OnPropertyChanged();
            }
        }


        private Student _currentStudentMVM;
        public Student CurrentStudentMVM  //Meu ok funciona !!
        {
            get { return _currentStudentMVM; }
            set
            {
                _currentStudentMVM = value;
                OnPropertyChanged("CurrentStudentMVM");
                OnPropertyChanged("CanShowInfo");
            }
        }

        private Subject _currentSubjectMVM;
        public Subject CurrentSubjectMVM  //Meu ok funciona !!
        {
            get { return _currentSubjectMVM; }
            set
            {
                _currentSubjectMVM = value;
                OnPropertyChanged("CurrentSubjectMVM");
                OnPropertyChanged("CanShowInfo");
            }
        }

        private StudentSubject _currentStudentSubjectMVM;
        public StudentSubject CurrentStudentSubjectMVM  //Meu ok funciona !!
        {
            get { return _currentStudentSubjectMVM; }
            set
            {
                _currentStudentSubjectMVM = value;
                OnPropertyChanged();
            }
        }


        List<Subject> _subjectsListMGVM;
        public List<Subject> SubjectListMGVM  //Meu : OK funciona
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
        public List<Subject> SubjectsByNameList  //Meu 
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
        public List<StudentSubject> SubjectsByStudentList  //Meu 
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



        public void AddSubjectToListVM()
        {
            Subject subject = new Subject();
            Student student = new Student();
            StudentSubject studentSubjectMVM = new StudentSubject();


            subject = CurrentSubjectMVM;
            student = CurrentStudentMVM;
            studentSubjectMVM.StudentId = student.Id;
            studentSubjectMVM.SubjectId = subject.Id;
            ManagementErrorMGVM = "";

            SubjectsByStudentList = studentSubjectMVM.StudentBySubjects(studentSubjectMVM.StudentId);
            if (SubjectsByStudentList != null && SubjectsByStudentList.Any(x => x.SubjectId == subject.Id))
            {
                ManagementErrorMGVM = "El alumno ya tiene la asignatura";
            }

            else
            {
                var error = studentSubjectMVM.Save();

                if (error.IsSuccess == false)
                {
                    ManagementErrorMGVM = error.ToString();
                }
                else
                {
                    var repo = StudentSubject.DepCon.Resolve<IRepository<StudentSubject>>();
                    SubjectsByStudentList = studentSubjectMVM.StudentBySubjects(studentSubjectMVM.StudentId);
                }
            }

        }


        public void DelSubjectToListVM()   //MEU OK Funciona
        {
            StudentSubject studentSubjectMVM = new StudentSubject();

            studentSubjectMVM = CurrentStudentSubjectMVM;

            studentSubjectMVM.Delete();

            SubjectsByStudentList = studentSubjectMVM.StudentBySubjects(studentSubjectMVM.StudentId);
            GetSubjectsToStudent();
        }


        public void GetSubjectsToStudent()  //MEU OK Funciona 
        {
            Student student = new Student();

            StudentSubject studentSubjectMVM = new StudentSubject();

            student = CurrentStudentMVM;
            studentSubjectMVM.StudentId = student.Id;

            SubjectsByStudentList = studentSubjectMVM.StudentBySubjects(studentSubjectMVM.StudentId);

        }

        private void FindStudent()   //Meu : Funciona OK
        {
            var studentsVM = new StudentsViewModel();
            studentsVM.GetStudents();

            CurrentStudentMVM = studentsVM.StudentsListNou.FirstOrDefault(x => x.Dni == DniMGVM);

            if (CurrentStudentMVM != null)
            {
                DniMGVM = CurrentStudentMVM.Dni;
                NameMGVM = CurrentStudentMVM.Name;
                ManagementErrorMGVM = "";
            }

            else
            {
                ManagementErrorMGVM = "Student no Existe";
                DniMGVM = "";
                NameMGVM = "";

            }

            GetSubjectsToStudent();
        }


        public void GetSubjectsMGVM()    //Meu : OK funciona
        {
            var subject = new Subject();
            var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
            SubjectListMGVM = repo.QueryAll().ToList();
        }


    }

}

