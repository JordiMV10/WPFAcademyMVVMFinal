using Common.Lib.Core;
using System;
using System.Collections.Generic;
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
                var subject = new Subject(); //BORRAR
                //var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
                //var subject = repo.Find(SubjectId);
                return subject;
            }
        }

        public Student Student
        {
            get
            {
                var student = new Student();   //BORRAR
                //var repo = Student.DepCon.Resolve<IRepository<Student>>();
                //var student = repo.Find(StudentId);
                return student;
            }
        }


    }
}
