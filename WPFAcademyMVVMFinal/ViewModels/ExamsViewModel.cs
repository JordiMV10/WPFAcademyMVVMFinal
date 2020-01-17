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
    public class ExamsViewModel : ViewModelBase
    {
        public ExamsViewModel()
        {
            GetSubjectsEVCommand = new RouteCommand(GetSubjectsEV);
            GetSubjectsNameEVCommand = new RouteCommand(GetSubjectsNameEV);
            SaveExamEVCommand = new RouteCommand(SaveExamEV);
            GetExamsEVCommand = new RouteCommand(GetExamsEV);
            DelExamEVCommand = new RouteCommand(DelExamEV);
            EditExamEVCommand = new RouteCommand(EditExamEV);

            DateEVM = DateTime.Now;

        }

        public ICommand GetSubjectsEVCommand { get; set; }
        public ICommand GetSubjectsNameEVCommand { get; set; }
        public ICommand SaveExamEVCommand { get; set; }
        public ICommand GetExamsEVCommand { get; set; }
        public ICommand DelExamEVCommand { get; set; }
        public ICommand EditExamEVCommand { get; set; }



        private string _titleEVM;

        public string TitleEVM
        {
            get
            {
                return _titleEVM;
            }
            set
            {
                _titleEVM = value;
                OnPropertyChanged();
            }
        }

        private string _textEVM;

        public string TextEVM
        {
            get
            {
                return _textEVM;
            }
            set
            {
                _textEVM = value;
                OnPropertyChanged();
            }
        }


        private DateTime _dateEVM;
        public DateTime DateEVM
        {
            get
            {
                return _dateEVM;
            }
            set
            {
                _dateEVM = value;
                OnPropertyChanged();
            }
        }


        private string _currentSubjectNameEVM;
        public string CurrentSubjectNameEVM
        {
            get
            {
                return _currentSubjectNameEVM;
            }
            set
            {
                _currentSubjectNameEVM = value;
                OnPropertyChanged();
            }
        }




        private Subject _currentSubjectEVM;
        public Subject CurrentSubjectEVM  //Meu ok funciona !!
        {
            get { return _currentSubjectEVM; }
            set
            {
                _currentSubjectEVM = value;
                OnPropertyChanged();
            }
        }

        private Exam _currentExamEV;
        public Exam CurrentExamEV
        {
            get { return _currentExamEV; }
            set
            {
                _currentExamEV = value;
                OnPropertyChanged();
            }
        }


        List<Subject> _subjectsListEV;
        public List<Subject> SubjectsListEV
        {
            get
            {
                return _subjectsListEV;
            }
            set
            {
                _subjectsListEV = value;
                OnPropertyChanged();
            }

        }


        List<string> _subjectsNameListEV;
        public List<string> SubjectsNameListEV
        {
            get
            {
                return _subjectsNameListEV;
            }
            set
            {
                _subjectsNameListEV = value;
                OnPropertyChanged();
            }

        }


        List<Exam> _examsListEV;
        public List<Exam> ExamsListEV
        {
            get
            {
                return _examsListEV;
            }
            set
            {
                _examsListEV = value;
                OnPropertyChanged();
            }
        }



        List<ErrorMessage> _errorsListEV;

        public List<ErrorMessage> ErrorsListEV
        {
            get
            {
                return _errorsListEV;
            }
            set
            {
                _errorsListEV = value;
                OnPropertyChanged();
            }
        }



        public void GetSubjectsEV()  //NO TOCAR
        {
            var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
            SubjectsListEV = repo.QueryAll().ToList();
        }


        public List<string> GetSubjectsByNameEV()  //NO TOCAR
        {
            GetSubjectsEV();
            List<string> SubjectsNameListEV = new List<string>();
            foreach (Subject subj in SubjectsListEV)
            {
                var name = subj.Name;
                SubjectsNameListEV.Add(name);
            }
            return SubjectsNameListEV;
        }


        public void GetSubjectsNameEV()  //OK Funciona No tocar !!!!
        {
            SubjectsNameListEV = GetSubjectsByNameEV();
        }

        bool isEdit = false;
        public void SaveExamEV()  //OK Funciona bien !!
        {
            Exam exam = new Exam();
            Subject subject = new Subject();

            exam = SaveExamNameEV(CurrentSubjectNameEVM);

            if (isEdit == false)
                CurrentExamEV = null;

            if (CurrentExamEV != null)
                exam.Id = CurrentExamEV.Id;

            exam.Save();

            ErrorsListEV = exam.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();
            GetExamsEV();
            CurrentExamEV = null;
            TitleEVM = "";
            TextEVM = "";
            DateEVM = DateTime.Now;
            subject = null;

            isEdit = false;

        }

        public void GetExamsEV()  //OK Funciona bien
        {
            Exam exam = new Exam();
            var repo = Student.DepCon.Resolve<IRepository<Exam>>();
            ExamsListEV = repo.QueryAll().ToList();
        }


        public Exam SaveExamNameEV(string name)   //Meu : Funciona OK !! Me devuelve exam completo!! .
        {
            Subject subject = new Subject();
            var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
            subject = repo.QueryAll().FirstOrDefault(s => s.Name == name);   //Funciona bien, recupero bien el subject

            Exam exam = new Exam();
            exam.SubjectId = subject.Id;
            exam.Title = TitleEVM;
            exam.Text = TextEVM;
            exam.Date = DateEVM;

            return exam;

        }

        public void DelExamEV()    //Meu : Funciona OK !!
        {
            Exam exam = new Exam();

            if (CurrentExamEV == null)
            {
                exam.Save();
                ErrorsListEV = exam.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();

            }

            else
            {
                ErrorsListEV = new List<ErrorMessage>();

                exam = CurrentExamEV;

                exam.Delete();

                //ErrorsListEV = exam.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();

                GetExamsEV();

                TitleEVM = "";
                TextEVM = "";
                DateEVM = DateTime.Now;
            }

        }

        public void EditExamEV()   //Meu : Funciona ok. 
        {

            Exam exam = new Exam();
            if (CurrentExamEV == null)
            {
                exam.Save();
                ErrorsListEV = exam.CurrentValidation.Errors.Select(x => new ErrorMessage() { Message = x }).ToList();

            }

            else
            {
                exam = CurrentExamEV;

                Subject subject = new Subject();

                TitleEVM = CurrentExamEV.Title;
                TextEVM = CurrentExamEV.Text;
                DateEVM = CurrentExamEV.Date;
                CurrentSubjectNameEVM = CurrentExamEV.Subject.Name;

                isEdit = true;

            }
        }
    }
}
