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
    public class StatisticsByExamViewModel : ViewModelBase
    {
        public StatisticsByExamViewModel()
        {
            GetExamsEVCommand = new RouteCommand(GetExamsEV);
            EditExamEVCommand = new RouteCommand(EditExamEV);
            ClearSelEVCommand = new RouteCommand(ClearSelEV);
            AvgMarkSVMCommand = new RouteCommand(AvgMarkSVM);
            MaxMarkSVMCommand = new RouteCommand(MaxMarkSVM);
            MinMarkSVMCommand = new RouteCommand(MinMarkSVM);



        }


        public ICommand GetExamsEVCommand { get; set; }
        public ICommand EditExamEVCommand { get; set; }
        public ICommand ClearSelEVCommand { get; set; }
        public ICommand AvgMarkSVMCommand { get; set; }
        public ICommand MaxMarkSVMCommand { get; set; }
        public ICommand MinMarkSVMCommand { get; set; }





        private string _subjectNameEVM;

        public string SubjectNameEVM
        {
            get
            {
                return _subjectNameEVM;
            }
            set
            {
                _subjectNameEVM = value;
                OnPropertyChanged();
            }
        }




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


        List<StudentExam> _studentExamsListEV;
        public List<StudentExam> StudentExamsListEV
        {
            get
            {
                return _studentExamsListEV;
            }
            set
            {
                _studentExamsListEV = value;
                OnPropertyChanged();
            }
        }




        public void GetExamsEV()
        {
            Exam exam = new Exam();
            var repo = Student.DepCon.Resolve<IRepository<Exam>>();
            ExamsListEV = repo.QueryAll().ToList();
        }


        public void GetStudentExamsEV()
        {
            ErrorsSVM = "";
            var repo = Student.DepCon.Resolve<IRepository<StudentExam>>();

            if (CurrentExamEV != null)
            {
                StudentExamsListEV = repo.QueryAll().ToList();
                StudentExamsListEV = StudentExamsListEV.FindAll(x => x.ExamId == CurrentExamEV.Id);
            }
            else
            {
                StudentExamsListEV = repo.QueryAll().ToList();

            }

            if (StudentExamsListEV.Count == 0)
            {
                ErrorsSVM = "No hay Exámenes para mostrar";
                TitleEVM = "";
                SubjectNameEVM = "";
                CurrentExamEV = null;
            }

        }



        bool isEdit = false;

        public void EditExamEV()
        {
            ErrorsSVM = "";


            if (CurrentExamEV != null)
            {
                Subject subject = new Subject();

                var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
                var subjectsList = new List<Subject>();
                subjectsList = repo.QueryAll().ToList();

                subject = subjectsList.FirstOrDefault(x => x.Id == CurrentExamEV.SubjectId);


                TitleEVM = CurrentExamEV.Title;
                SubjectNameEVM = subject.Name;

                GetStudentExamsEV();

            }

            else
                ErrorsSVM = "No nas seleccionado ningún Examen";

            isEdit = true;

            
        }

        public void ClearSelEV()
        {
            TitleEVM = "";
            SubjectNameEVM = "";
            CurrentExamEV = null;
            GetStudentExamsEV();
            StudentExamsListEV.Clear();
            MarkSVM = 0;
        }



        #region  Statistics


        public void AvgMarkSVM()
        {
            MarkSVM = 0;
            var marksList = new List<double>();
            marksList = MarksListSVM();

            if (marksList == null) { }

            else
            {
                MarkSVM = marksList.Average();
                StudentExamsListEV.Clear();
            }
        }

        public void MaxMarkSVM()
        {
            MarkSVM = 0;

            var marksList = new List<double>();
            marksList = MarksListSVM();

            if (marksList == null) { }

            else
            {
                MarkSVM = marksList.Max();
                StudentExamsListEV = StudentExamsListEV.FindAll(x => x.Mark == MarkSVM).ToList();

            }

        }


        public void MinMarkSVM()
        {
            MarkSVM = 0;

            var marksList = new List<double>();
            marksList = MarksListSVM();

            if (marksList == null) { }

            else
            {
                MarkSVM = marksList.Min();

                StudentExamsListEV = StudentExamsListEV.FindAll(x => x.Mark == MarkSVM).ToList();
            }
        }


        public List<double> MarksListSVM()
        {
            ErrorsSVM = "";
            GetStudentExamsEV();  


            if (CurrentExamEV != null)
            {


                var marksList = new List<double>();

                foreach (StudentExam stuEx in StudentExamsListEV)
                {
                    marksList.Add(stuEx.Mark);
                }

                return marksList;
            }


            else
            {

                var repo = Subject.DepCon.Resolve<IRepository<StudentExam>>();
                List<StudentExam> StudentExamsList = new List<StudentExam>();
                StudentExamsList = repo.QueryAll().ToList();


                var marksList = new List<double>();

                foreach (StudentExam stuEx in StudentExamsListEV)
                {
                    marksList.Add(stuEx.Mark);
                }

                return marksList;
            }

        }
        #endregion

    }
}
