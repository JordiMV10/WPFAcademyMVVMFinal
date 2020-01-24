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
            GetStudentExamsBySubjectCommand = new RouteCommand(GetStudentExamsBySubjectAndExam);
            AvgMarkSVMCommand = new RouteCommand(AvgMarkSVM);
            MaxMarkSVMCommand = new RouteCommand(MaxMarkSVM);
            MinMarkSVMCommand = new RouteCommand(MinMarkSVM);
        }


        public ICommand LoadDataByNameCommand { get; set; }
        public ICommand GetSubjectByStudentsEVCommand { get; set; }
        public ICommand GetStudentExamsBySubjectCommand { get; set; }
        public ICommand AvgMarkSVMCommand { get; set; }
        public ICommand MaxMarkSVMCommand { get; set; }
        public ICommand MinMarkSVMCommand { get; set; }




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

        List<StudentExam> _studentExamsBySubjectList;
        public List<StudentExam> StudentExamsBySubjectList
        {
            get
            {
                return _studentExamsBySubjectList;
            }
            set
            {
                _studentExamsBySubjectList = value;
                OnPropertyChanged();
            }

        }


        List<StudentExam> _studentExamsList;
        public List<StudentExam> StudentExamsList
        {
            get
            {
                return _studentExamsList;
            }
            set
            {
                _studentExamsList = value;
                OnPropertyChanged();
            }

        }





        public void GetSubjectsEV()  
        {
            var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
            SubjectsListEV = repo.QueryAll().ToList();
        }


        public List<string> GetSubjectsByNameEV()  
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


        public void LoadDataByNameEV()  
        {
            SubjectsNameListEV = GetSubjectsByNameEV();
            ExamsNameListEV = GetExamsByNameSVM();

        }

        public void GetSubjectByStudents()
        {
            ErrorsSVM = "";
            if (CurrentSubjectNameEVM != null)
            {
                GetSubjectsEV();
                CurrentSubjectEVM = SubjectsListEV.FirstOrDefault(x => x.Name == CurrentSubjectNameEVM);
                var repo = Subject.DepCon.Resolve<IRepository<StudentSubject>>();
                SubjectByStudentsList = repo.QueryAll().Where(x => x.SubjectId == CurrentSubjectEVM.Id).ToList();

            }
            else
            {
                ErrorsSVM = "No has seleccionado ninguna asignatura";
            }
        }


        public void GetExamsSVM()  
        {
            var repoExams = Subject.DepCon.Resolve<IRepository<Exam>>();
            if (CurrentSubjectNameEVM == null)   
                ExamsListEV = repoExams.QueryAll().ToList();

            
            else
            {
                GetSubjectsEV();
                CurrentSubjectEVM = SubjectsListEV.FirstOrDefault(x => x.Name == CurrentSubjectNameEVM);

                ExamsListEV = repoExams.QueryAll().Where(x => x.SubjectId == CurrentSubjectEVM.Id).ToList();
            }
        }


        public List<string> GetExamsByNameSVM()  
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


        public void GetStudentExamsBySubjectAndExam()
        {
            ErrorsSVM = "";
            var repo = Subject.DepCon.Resolve<IRepository<StudentExam>>();
            List<StudentExam> StudentExamsList = new List<StudentExam>();
            StudentExamsList = repo.QueryAll().ToList();
            GetExamsSVM();

            if (CurrentSubjectNameEVM !=null )
            {
                GetSubjectsEV();
                CurrentSubjectEVM = SubjectsListEV.FirstOrDefault(x => x.Name == CurrentSubjectNameEVM);

                if (CurrentExamNameEVM != null)
                {
                    CurrentExamEV = ExamsListEV.FirstOrDefault(x => x.Title == CurrentExamNameEVM);

                    StudentExamsBySubjectList = StudentExamsList.FindAll(x => x.ExamId == CurrentExamEV.Id).ToList();

                }

                else
                {
                    StudentExamsBySubjectList = StudentExamsList.FindAll(x => x.Exam.SubjectId == CurrentSubjectEVM.Id);
                }

            }

            else
                ErrorsSVM = "No hay ningúna Asignatura seleccionada";
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
                StudentExamsBySubjectList.Clear();
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
                StudentExamsBySubjectList = StudentExamsBySubjectList.FindAll(x => x.Mark == MarkSVM).ToList();

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

                StudentExamsBySubjectList = StudentExamsBySubjectList.FindAll(x => x.Mark == MarkSVM).ToList();
            }
        }


        public List<double> MarksListSVM()
        {
            ErrorsSVM = "";
            GetStudentExamsBySubjectAndExam();  


            if (CurrentExamNameEVM != null)
            {
                var marksList = new List<double>();

                foreach (StudentExam stuEx in StudentExamsBySubjectList)
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

                foreach (StudentExam stuEx in StudentExamsBySubjectList) 
                {
                    marksList.Add(stuEx.Mark);
                }

                return marksList;
            }

        }
        #endregion



    }
}
