using Common.Lib.Core;
using System;
using System.Collections.Generic;
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
    }

}
