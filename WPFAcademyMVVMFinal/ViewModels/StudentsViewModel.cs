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
    public class StudentsViewModel : ViewModelBase
    {
        private string _dniVM;

        public string DniVM
        {
            get { return _dniVM; }
            set
            {
                _dniVM = value;
                OnPropertyChanged();
            }
        }

        private string _nameVM;

        public string NameVM
        {
            get { return _nameVM; }
            set
            {
                _nameVM = value;
                OnPropertyChanged();
            }
        }

        private int _chairNumberVM;

        public int ChairNumberVM
        {
            get { return _chairNumberVM; }
            set
            {
                _chairNumberVM = value;
                OnPropertyChanged();
            }
        }


        public List<StudentsViewModel> Students
        {
            get
            {
                return _students;
            }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }
        List<StudentsViewModel> _students;


        public StudentsViewModel()
        {

            SaveStudentCommand = new RouteCommand(SaveStudent);
            GetStudentsCommand = new RouteCommand(GetStudents);
            DelStudentCommand = new RouteCommand(DelStudent);
            EditStudentCommand = new RouteCommand(EditStudent);
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

        bool isEdit = false;

        public void SaveStudent()
        {
            Student student = new Student()
            {
                Dni = DniVM,
                Name = NameVM,
                ChairNumber = ChairNumberVM,

            };

            if (isEdit == false)
                CurrentStudent = null;

            if (CurrentStudent != null)
                student.Id = CurrentStudent.Id;

            student.Save();  


            ErrorsList = student.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();
            GetStudents();
            CurrentStudent = null;
            DniVM = "";
            NameVM = "";
            ChairNumberVM = 0;

            isEdit = false;
        }

        public void GetStudents()
        {

            var student = new Student();
            var repo = Student.DepCon.Resolve<IRepository<Student>>();
            StudentsListNou = repo.QueryAll().ToList();

        }





        List<Student> _studentsListNou;
        public List<Student> StudentsListNou
        {
            get
            {
                return _studentsListNou;
            }
            set
            {
                _studentsListNou = value;
                OnPropertyChanged();
            }
        }



        #region Commands
        public ICommand SaveStudentCommand { get; set; }
        public ICommand GetStudentsCommand { get; set; }

        public ICommand DelStudentCommand { get; set; } //Meu funciona OK
        public ICommand EditStudentCommand { get; set; }

        #endregion


        private Student _currentStudent;
        public Student CurrentStudent  //Meu ok funciona !!
        {
            get { return _currentStudent; }
            set
            {
                _currentStudent = value;
                OnPropertyChanged("CurrentStudent");
                OnPropertyChanged("CanShowInfo");
            }
        }

        public void DelStudent()    //Meu, verificado funciona OK
        {

            Student student = new Student();

            student = CurrentStudent;

            student.Delete();   

            ErrorsList = student.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();


            GetStudents();

            DniVM = "";
            NameVM = "";
            ChairNumberVM = 0;
        }

        private bool CanShowInfo
        {
            get
            {
                return CurrentStudent != null;
            }
        }


        public void EditStudent()   //Meu : Funciona ok. 
        {

            var student = new Student();

            student = CurrentStudent;

            DniVM = CurrentStudent.Dni;
            NameVM = CurrentStudent.Name;
            ChairNumberVM = CurrentStudent.ChairNumber;

            isEdit = true;
        }


    }

}
