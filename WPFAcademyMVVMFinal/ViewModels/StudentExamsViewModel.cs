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


        }




        public ICommand FindStudentSEVMCommand { get; set; }
        public ICommand GetExamsSEVMCommand { get; set; }
        public ICommand SelExamSEVMCommand { get; set; }
        public ICommand EditStudentExamsSEVMCommand { get; set; }
        public ICommand DelStudentExamsSEVMCommand { get; set; }






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










        public void EditStudentExamsSEVM()  //Pdte. acabar ???  Repasar y probar
        {
            StudentExam studentExam = new StudentExam();

            DniSEVM = CurrentStudentExamSEVM.Student.Dni;
            NameSEVM = CurrentStudentExamSEVM.Student.Name;
            TitleSEVM = CurrentStudentExamSEVM.Exam.Text;
            SubjectNameSEVM = CurrentStudentExamSEVM.Exam.Subject.Name;
            DateSEVM = CurrentStudentExamSEVM.Exam.Date;
            MarkSEVM = CurrentStudentExamSEVM.Mark;
            HasCheatedSEVM = CurrentStudentExamSEVM.HasCheated;

            studentExam.Student.Dni = DniSEVM;
            studentExam.Student.Name = NameSEVM;
            studentExam.Exam.Title = TitleSEVM;
            studentExam.Exam.Subject.Name = SubjectNameSEVM;
            studentExam.Exam.Date = DateSEVM;
            studentExam.Mark = MarkSEVM;
            studentExam.HasCheated = HasCheatedSEVM;


        }


        public void DelStudentExamsSEVM()    // Pdte.acabar  ???? Y probar
        {
            StudentExam studentExam = new StudentExam();
            studentExam = CurrentStudentExamSEVM;
            studentExam.Delete();


        }



        private void FindStudentSEVM()   //Funciona OK  Pdte confirmar protecciones
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

        public void GetStudentExamsSEVM()  //  Pdte. probar funcionamiento.
        {
            Student student = new Student();
            StudentExam studentExam = new StudentExam();

            if (CurrentStudentSEVM != null)
            {
                student = CurrentStudentSEVM;
                studentExam.StudentId = student.Id;

                StudentExamsListSEVM = studentExam.StudentByExams(studentExam.StudentId);

            }


        }


    }
}
