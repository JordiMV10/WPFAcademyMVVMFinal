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
    public class SubjectsViewModel : ViewModelBase
    {

        public SubjectsViewModel()
        {

            SaveSubjectCommand = new RouteCommand(SaveSubject);
            GetSubjectsCommand = new RouteCommand(GetSubjects);
            DelSubjectCommand = new RouteCommand(DelSubject);
            EditSubjectCommand = new RouteCommand(EditSubject);
        }


        #region Commands
        public ICommand SaveSubjectCommand { get; set; }
        public ICommand GetSubjectsCommand { get; set; }
        public ICommand DelSubjectCommand { get; set; } //Meu funciona OK
        public ICommand EditSubjectCommand { get; set; }

        #endregion


        private string _subjectNameVM;

        public string SubjectNameVM
        {
            get { return _subjectNameVM; }
            set
            {
                _subjectNameVM = value;
                OnPropertyChanged();
            }
        }


        private string _subjectTeacherVM;
        public string SubjectTeacherVM
        {
            get { return _subjectTeacherVM; }
            set
            {
                _subjectTeacherVM = value;
                OnPropertyChanged();
            }
        }


        private Subject _currentSubject;
        public Subject CurrentSubject  //Meu ok funciona !!
        {
            get { return _currentSubject; }
            set
            {
                _currentSubject = value;
                OnPropertyChanged();
            }
        }



        List<Subject> _subjectsList;
        public List<Subject> SubjectList  //Meu : OK funciona
        {
            get
            {
                return _subjectsList;
            }
            set
            {
                _subjectsList = value;

                //if (value != null && value.Count > 0)  //Nou
                //{
                //    CurrentSubject = value[0];          //Nou
                //}                                       //Nou
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






        bool isEdit = false;

        public void SaveSubject()   //Meu : OK funciona
        {

            Subject subject = new Subject()
            {
                Name = SubjectNameVM,
                Teacher = SubjectTeacherVM

            };

            if (isEdit == false)
                CurrentSubject = null;

            if (CurrentSubject != null)
                subject.Id = CurrentSubject.Id;

            subject.Save();

            ErrorsList = subject.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();

            GetSubjects();
            CurrentSubject = null;
            SubjectNameVM = "";
            SubjectTeacherVM = "";
            isEdit = false;
        }

        public void GetSubjects()    //Meu : OK funciona
        {
            var subject = new Subject();
            var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
            SubjectList = repo.QueryAll().ToList();
        }


        public void EditSubject()   //Meu : Funciona ok. 
        {
            var subject = new Subject();

            if (CurrentSubject == null)
            {
                subject.Save();
                ErrorsList = subject.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();

            }

            else
            {
                ErrorsList = new List<ErrorMessage>();

                subject = CurrentSubject;

                SubjectNameVM = CurrentSubject.Name;
                SubjectTeacherVM = CurrentSubject.Teacher;


                isEdit = true;

            }

        }


        public void DelSubject()    //Meu, verificado funciona OK
        {
            Subject subject = new Subject();

            if (CurrentSubject == null)
            {
                subject.Delete();
                ErrorsList = subject.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();

            }

            else
            {
                ErrorsList = new List<ErrorMessage>();               
                subject = CurrentSubject;

                subject.Delete();

                ErrorsList = subject.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();

                GetSubjects();

                SubjectNameVM = "";
                SubjectTeacherVM = "";

            }
        }
    }
}


