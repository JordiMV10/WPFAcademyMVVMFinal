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



        }




        public ICommand GetExamsEVCommand { get; set; }
        public ICommand EditExamEVCommand { get; set; }
        public ICommand ClearSelEVCommand { get; set; }






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


        }



        bool isEdit = false;

        public void EditExamEV()
        {
            //Exam exam = new Exam();
            Subject subject = new Subject();

            var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
            var subjectsList = new List<Subject>();
            subjectsList = repo.QueryAll().ToList();

            subject = subjectsList.FirstOrDefault(x => x.Id == CurrentExamEV.SubjectId);


            //exam = CurrentExamEV;


            TitleEVM = CurrentExamEV.Title;
            SubjectNameEVM = subject.Name;

            GetStudentExamsEV();

            isEdit = true;

            
        }

        public void ClearSelEV()
        {
            TitleEVM = "";
            SubjectNameEVM = "";
            CurrentExamEV = null;
            GetStudentExamsEV();
            StudentExamsListEV.Clear();
        }
          //Seguir aqui !! : Pdte desarrollar max min y avg !!
    }
}
