using Academy.Lib.DAL.Repositories;
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
    public class StudentExamsViewModel : ViewModelBase
    {

        public StudentExamsViewModel()
        {
            FindStudentSEVMCommand = new RouteCommand(FindStudentSEVM);
            GetExamsSEVMCommand = new RouteCommand(GetExamsSEVM);
            SelExamSEVMCommand = new RouteCommand(SelExamSEVM);
            EditStudentExamsSEVMCommand = new RouteCommand(EditStudentExamsSEVM);
            DelStudentExamsSEVMCommand = new RouteCommand(DelStudentExamsSEVM);
            SaveStudentExamsSEVMCommand = new RouteCommand(SaveStudentExamsSEVM);


        }




        public ICommand FindStudentSEVMCommand { get; set; }
        public ICommand GetExamsSEVMCommand { get; set; }
        public ICommand SelExamSEVMCommand { get; set; }
        public ICommand EditStudentExamsSEVMCommand { get; set; }
        public ICommand DelStudentExamsSEVMCommand { get; set; }
        public ICommand SaveStudentExamsSEVMCommand { get; set; }







        private string _dniSEVM;
        public string DniSEVM
        {
            get { return _dniSEVM; }
            set
            {
                _dniSEVM = value;
                OnPropertyChanged();
            }
        }

        private string _nameSEVM;
        public string NameSEVM
        {
            get { return _nameSEVM; }
            set
            {
                _nameSEVM = value;
                OnPropertyChanged();
            }
        }

        private string _titleSEVM;

        public string TitleSEVM
        {
            get
            {
                return _titleSEVM;
            }
            set
            {
                _titleSEVM = value;
                OnPropertyChanged();
            }
        }


        private DateTime _dateSEVM;
        public DateTime DateSEVM
        {
            get
            {
                return _dateSEVM;
            }
            set
            {
                _dateSEVM = value;
                OnPropertyChanged();
            }
        }


        private double _markSEVM;
        public double MarkSEVM
        {
            get
            {
                return _markSEVM;
            }
            set
            {
                _markSEVM = value;
                OnPropertyChanged();
            }

        }


        private bool _hasCheatedSEVM;
        public bool HasCheatedSEVM
        {
            get
            {
                return _hasCheatedSEVM;
            }
            set
            {
                _hasCheatedSEVM = value;
                OnPropertyChanged();
            }

        }


        private string _subjectNameSEVM;
        public string SubjectNameSEVM
        {
            get
            {
                return _subjectNameSEVM;
            }
            set
            {
                _subjectNameSEVM = value;
                OnPropertyChanged();
            }
        }


        private Student _currentStudentSEVM;
        public Student CurrentStudentSEVM  
        {
            get { return _currentStudentSEVM; }
            set
            {
                _currentStudentSEVM = value;
                OnPropertyChanged();
            }
        }


        private Exam _currentExamSEVM;
        public Exam CurrentExamSEVM
        {
            get { return _currentExamSEVM; }
            set
            {
                _currentExamSEVM = value;
                OnPropertyChanged();
            }
        }


        private StudentExam _currentStudentExamSEVM;
        public StudentExam CurrentStudentExamSEVM
        {
            get { return _currentStudentExamSEVM; }
            set
            {
                _currentStudentExamSEVM = value;
                OnPropertyChanged();
            }
        }




        List<ErrorMessage> _errorsList;  //Nou
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


        List<Exam> _examsListSEVM;
        public List<Exam> ExamsListSEVM
        {
            get
            {
                return _examsListSEVM;
            }
            set
            {
                _examsListSEVM = value;
                OnPropertyChanged();
            }
        }

        List<StudentExam> _studentExamsListSEVM;
        public List<StudentExam> StudentExamsListSEVM
        {
            get
            {
                return _studentExamsListSEVM;
            }
            set
            {
                _studentExamsListSEVM = value;
                OnPropertyChanged();
            }
        }


        bool isEdit = false;

        public void SaveStudentExamsSEVM()   //Pdte probar funcionamiento
        {

            StudentExam studentExamsSEVM = new StudentExam();
            //{
            //    Mark = MarkSEVM,
            //    HasCheated = HasCheatedSEVM
            //};

            Exam exam = new Exam();
            Student student = new Student();
            ErrorsList = new List<ErrorMessage>();


            exam = CurrentExamSEVM;
            student = CurrentStudentSEVM;
            if (CurrentStudentExamSEVM != null)
            {
                studentExamsSEVM = CurrentStudentExamSEVM;
            }
                studentExamsSEVM.Mark = MarkSEVM;
                studentExamsSEVM.HasCheated = HasCheatedSEVM;

            //}

            if (CurrentStudentSEVM != null)
            {
                studentExamsSEVM.StudentId = student.Id;
                

                if (CurrentExamSEVM != null)
                {

                    studentExamsSEVM.ExamId = exam.Id;
                }
            }

            studentExamsSEVM.Save();

            ErrorsList = studentExamsSEVM.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();  //Nou

            if (CurrentStudentSEVM != null || CurrentStudentExamSEVM != null)
            {
                GetStudentExamsSEVM();
            }

            CurrentStudentSEVM = null;
            _currentExamSEVM = null;
            CurrentStudentExamSEVM = null;
            DniSEVM = "";
            NameSEVM = "";
            TitleSEVM = "";
            SubjectNameSEVM = "";
            DateSEVM = default;
            MarkSEVM = 0;
            HasCheatedSEVM = false;

            isEdit = false;
        }








        public void EditStudentExamsSEVM()  //Pdte. Repasar y probar mejor. parece funciona OK
        {
            StudentExam studentExam = new StudentExam();

            DniSEVM = CurrentStudentExamSEVM.Student.Dni;
            NameSEVM = CurrentStudentExamSEVM.Student.Name;
            TitleSEVM = CurrentStudentExamSEVM.Exam.Text;
            SubjectNameSEVM = CurrentStudentExamSEVM.Exam.Subject.Name;
            DateSEVM = CurrentStudentExamSEVM.Exam.Date;
            MarkSEVM = CurrentStudentExamSEVM.Mark;
            HasCheatedSEVM = CurrentStudentExamSEVM.HasCheated;

            CurrentExamSEVM = CurrentStudentExamSEVM.Exam;

        }


        public void DelStudentExamsSEVM()    //Funciona OK
        {
            StudentExam studentExam = new StudentExam();

            if (CurrentStudentExamSEVM == null)
            {
                studentExam.Delete();
                ErrorsList = studentExam.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();

            }

            else
            {
                ErrorsList = new List<ErrorMessage>();

                studentExam = CurrentStudentExamSEVM;
                studentExam.Delete();
                ErrorsList = studentExam.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();

                GetStudentExamsSEVM();

                //DniSEVM = "";
                //NameSEVM = "";
                //TitleSEVM = "";
                //SubjectNameSEVM = "";
                //DateSEVM = default;
                //MarkSEVM = 0;
                //HasCheatedSEVM = false;

            }
        }



        private void FindStudentSEVM()   //Funciona OK  
        {
            var studentsVM = new StudentsViewModel();
            StudentSubject studentSubjectMVM = new StudentSubject();

            studentsVM.GetStudents();

            CurrentStudentSEVM = studentsVM.StudentsListNou.FirstOrDefault(x => x.Dni == DniSEVM);

            if (CurrentStudentSEVM != null)
            {
                DniSEVM = CurrentStudentSEVM.Dni;
                NameSEVM = CurrentStudentSEVM.Name;
                ErrorsList = new List<ErrorMessage>();
                GetStudentExamsSEVM();
            }

            else
            {
                NameSEVM = "Student no Existe";
                Student student = new Student();
                CurrentStudentSEVM = student;
                ErrorsList = new List<ErrorMessage>();
                StudentExamsListSEVM = new List<StudentExam>();

                DniSEVM = "";

            }
        }


        public void GetExamsSEVM()  //OK Funciona bien
        {
            Exam exam = new Exam();
            var repo = Student.DepCon.Resolve<IRepository<Exam>>();
            ExamsListSEVM = repo.QueryAll().ToList();
        }

        public void SelExamSEVM()  //OK Funciona bien
        {
            Exam exam = new Exam();

            exam = CurrentExamSEVM;

            TitleSEVM = CurrentExamSEVM.Title;
            SubjectNameSEVM = CurrentExamSEVM.Subject.Name;
            DateSEVM = CurrentExamSEVM.Date;
        }

        public void GetStudentExamsSEVM()  //  OK funciona perfecto !!.
        {
            Student student = new Student();
            Exam exam = new Exam();
            StudentExam studentExam = new StudentExam();

            if (CurrentStudentSEVM != null)
            {
                
                student = CurrentStudentSEVM;
                exam = CurrentExamSEVM;
                studentExam.StudentId = student.Id;

                StudentExamsListSEVM = studentExam.StudentByExams(studentExam.StudentId);
            }


        }


    }
}
