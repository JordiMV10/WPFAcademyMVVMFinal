using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Academy.Lib.Models
{
    public class StudentExam : Entity
    {

        public Exam Exam
        {
            get
            {
                var repo = Student.DepCon.Resolve<IRepository<Exam>>();
                var exam = repo.Find(ExamId);
                return exam;
            }
        }

        public Student Student
        {
            get
            {
                var repo = Student.DepCon.Resolve<IRepository<Student>>();
                var student = repo.Find(StudentId);
                return student;
            }
        }


        public Guid ExamId { get; set; }

        public Guid StudentId { get; set; }

        public double Mark { get; set; }

        public bool HasCheated { get; set; }

        public StudentExam()
        {

        }


        public SaveResult<StudentExam> Save() 
        {
            var saveResult = base.Save<StudentExam>();

            return saveResult;
        }


        public DeleteResult<StudentExam> Delete()  
        {
            var deleteResult = base.Delete<StudentExam>();

            return deleteResult;
        }

        public List<StudentExam> StudentByExams(Guid idStudent)   
        {
            var repo = DepCon.Resolve<IRepository<StudentExam>>();
            var entityId = repo.QueryAll().Where(e => e.StudentId == idStudent).ToList();
            return entityId;
        }

        public Exam FindExam(Guid idExam)
        {
            var repo = DepCon.Resolve<IRepository<Exam>>();
            var entity = repo.QueryAll().FirstOrDefault(x => x.Id == idExam);
            return entity;
        }

        public override ValidationResult Validate()
        {
            var output = base.Validate();

            ValidateStudentExam(output);
            ValidateStudentSubject(output);
            ValidateMark(output);

            return output;
        }

        public void ValidateMark(ValidationResult validationResult)
        {
            var vr = ValidateMark(this.Mark.ToString(), this.Id);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }

        }

        public void ValidateStudentExam(ValidationResult validationResult)
        {
            var vr = ValidateStudentExam(this.StudentId, this.ExamId, this.Id);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }

        }


        public void ValidateStudentSubject(ValidationResult validationResult)
        {
            var vr = ValidateStudentSubject(this.StudentId, this.ExamId, this.Id);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }

        }

        
        public ValidationResult<double> ValidateMark(string markText, Guid currentId = default)
        {
            var output = new ValidationResult<double>()
            {
                IsSuccess = true
            };

            var mark = 0.0;
            var isConversionOk = false;

            #region check null or empty
            if (string.IsNullOrEmpty(markText))
            {
                output.IsSuccess = false;
                output.Errors.Add("La Mark no puede estar vacío o nulo");
            }
            #endregion

            #region check format conversion

            isConversionOk = double.TryParse(markText.Replace(".", ","), out mark);

            if (!isConversionOk)
            {
                output.IsSuccess = false;
                output.Errors.Add($"no se puede convertir {markText} en número");
            }
            #endregion

            if (output.IsSuccess)
            {
                if (mark >= 0 && mark <= 10) 
                {
                    output.ValidatedResult = mark;
                }
                else
                {
                    output.IsSuccess = false;
                    output.Errors.Add("La nota no es correcta");

                }
            }
            return output;
        }


        public ValidationResult<string> ValidateStudentSubject(Guid studentId, Guid examId, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            var studentSubject = new StudentSubject();
            var studentBySubjects = new List<StudentSubject>();
            studentBySubjects = studentSubject.StudentBySubjects(studentId);
            var exam = new Exam();

            exam = FindExam(ExamId);

            if (exam ==null)
            {
                output.IsSuccess = false;
                output.Errors.Add("No has seleccionado ningún Examen");

            }

            else
            {
                studentSubject = studentBySubjects.FirstOrDefault(x => x.SubjectId == exam.SubjectId);


                //On Delete
                if (currentId != default)
                {
                    output.IsSuccess = true;
                }

                //On Create
                else
                {
                    if (studentId == default)
                    {
                        output.IsSuccess = false;
                        output.Errors.Add("el Student no puede estar vacío");
                    }

                    else
                    {
                        if (exam.SubjectId == default)
                        {
                            output.IsSuccess = false;
                            output.Errors.Add("la Asignatura no puede estar vacío");
                        }

                        else
                        {
                            if (studentSubject == null)
                            {
                                output.IsSuccess = false;
                                output.Errors.Add("El Student no está matriculado de la Asignatura");
                            }
                        }
                    }
                }
            }

            return output;
        }




        public ValidationResult<string> ValidateStudentExam(Guid studentId, Guid examId, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };


            var studentByExams = new List<StudentExam>();
            studentByExams = StudentByExams(studentId);

            //On Delete
            if (currentId != default)
            {
                output.IsSuccess = true;
            }

            //On Create
            else
            {
                if (studentId == default)
                {
                    output.IsSuccess = false;
                    output.Errors.Add("El Student no puede estar vacío");
                }

                else
                {
                    if (examId == default)
                    {
                        output.IsSuccess = false;
                        output.Errors.Add("El Examen no puede estar vacío");
                    }

                    else
                    {
                        var repo = DepCon.Resolve<IRepository<StudentSubject>>();

                        if (studentByExams != null && studentByExams.Any(x => x.ExamId == examId))
                        {
                            output.IsSuccess = false;
                            output.Errors.Add("Ya está registrado este Examen");
                        }
                    }
                }
            }

            return output;
        }




    }

}
