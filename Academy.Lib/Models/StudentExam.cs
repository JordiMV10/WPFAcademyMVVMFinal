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
        public Exam Exam { get; set; }
        public Guid ExamId { get; set; }

        public Student Student { get; set; }
        public Guid StudentId { get; set; }

        public double Mark { get; set; }

        public bool HasCheated { get; set; }

        public StudentExam()
        {

        }

        public DeleteResult<StudentExam> Delete()  //Pdte verificar si funciona
        {
            var deleteResult = base.Delete<StudentExam>();

            return deleteResult;
        }

        public List<StudentExam> StudentByExams(Guid idStudent)   //Pdte verificar si funciona
        {
            var repo = DepCon.Resolve<IRepository<StudentExam>>();
            var entityId = repo.QueryAll().Where(e => e.StudentId == idStudent).ToList();
            return entityId;
        }

    }

}
