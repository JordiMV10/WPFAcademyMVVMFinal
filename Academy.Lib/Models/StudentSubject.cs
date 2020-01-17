using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Academy.Lib.Models
{
    public class StudentSubject : Entity
    {
            
        public Guid StudentId { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject
        {
            get
            {
                var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
                var subject = repo.Find(SubjectId);
                return subject;
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

        public override ValidationResult Validate()
        {
            var output = base.Validate();

            ValidateName(output);
            //ValidateTeacher(output);

            return output;
        }



        public SaveResult<StudentSubject> Save()   //OK funciona bien
        {
            var saveResult = base.Save<StudentSubject>();

            return saveResult;
        }


        public DeleteResult<StudentSubject> Delete()  //OK funciona bien
        {
            var deleteResult = base.Delete<StudentSubject>();

            return deleteResult;
        }

        public  List<StudentSubject> StudentBySubjects(Guid idStudent)   //Meu Funciona OK, torna llista dels IDs dels students i les ID de les seves Subjects.
        {
            var repo = DepCon.Resolve<IRepository<StudentSubject>>();
            var entityId = repo.QueryAll().Where(e => e.StudentId == idStudent).ToList();
            return entityId;
        }

        public ValidationResult<string> ValidateName(Guid studentId, Guid subjectId, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

          
            var studentBySubjects = new List<StudentSubject>();
            studentBySubjects = StudentBySubjects(studentId); 

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
                    if (subjectId == default)
                    {
                        output.IsSuccess = false;
                        output.Errors.Add("la Asignatura no puede estar vacío");

                    }


                    else
                    {
                        var repo = DepCon.Resolve<IRepository<StudentSubject>>();

                        if (studentBySubjects != null && studentBySubjects.Any(x => x.SubjectId == subjectId))
                        {
                            output.IsSuccess = false;
                            output.Errors.Add("Ya está asignada esta Asignatura");
                        }

                    }
                }
                

            }

            return output;
        }


        public void ValidateName(ValidationResult validationResult)
        {
            var vr = ValidateName(this.StudentId, this.SubjectId, this.Id);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }

        }





        public StudentSubject Clone()
        {
            return Clone<StudentSubject>();
        }


    }



}
