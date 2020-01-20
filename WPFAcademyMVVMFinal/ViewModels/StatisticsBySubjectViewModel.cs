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
    public class StatisticsBySubjectViewModel : ViewModelBase
    {
        public StatisticsBySubjectViewModel()
        {
            LoadDataByNameCommand = new RouteCommand(LoadDataByNameEV);
            GetSubjectByStudentsEVCommand = new RouteCommand(GetSubjectByStudents);


        }


        public ICommand LoadDataByNameCommand { get; set; }
        public ICommand GetSubjectByStudentsEVCommand { get; set; }



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
        public Subject CurrentSubjectEVM  
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

        private string _currentExamNameEVM;
        public string CurrentExamNameEVM
        {
            get
            {
                return _currentExamNameEVM;
            }
            set
            {
                _currentExamNameEVM = value;
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


        List<StudentSubject> _subjectByStudentsList;
        public List<StudentSubject> SubjectByStudentsList
        {
            get
            {
                return _subjectByStudentsList;
            }
            set
            {
                _subjectByStudentsList = value;
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

        List<string> _examsNameListEV;
        public List<string> ExamsNameListEV
        {
            get
            {
                return _examsNameListEV;
            }
            set
            {
                _examsNameListEV = value;
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


        public void LoadDataByNameEV()  //OK Funciona No tocar !!!!
        {
            SubjectsNameListEV = GetSubjectsByNameEV();
            ExamsNameListEV = GetExamsByNameSVM();

        }

        public void GetSubjectByStudents()
        {
            GetSubjectsEV();
            CurrentSubjectEVM = SubjectsListEV.FirstOrDefault(x => x.Name == CurrentSubjectNameEVM);
            var repo = Subject.DepCon.Resolve<IRepository<StudentSubject>>();
            SubjectByStudentsList = repo.QueryAll().Where (x=> x.SubjectId==CurrentSubjectEVM.Id).ToList();
        }


        public void GetExamsSVM()  //NO TOCAR
        {
            var repo = Subject.DepCon.Resolve<IRepository<Exam>>();
            ExamsListEV = repo.QueryAll().ToList();
        }


        public List<string> GetExamsByNameSVM()  //NO TOCAR
        {
            GetExamsSVM();
            List<string> examsNameListEV = new List<string>();
            foreach (Exam ex in ExamsListEV)
            {
                var title = ex.Title;
                examsNameListEV.Add(title);
            }
            return examsNameListEV;
        }

        //public void GetExamsNameSVM()  //OK Funciona No tocar !!!!
        //{
        //    ExamsNameListEV = GetExamsByNameSVM();
        //}



    }
}
