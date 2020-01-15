using Common.Lib.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Lib.Models
{
    public class Student : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Dni { get; set; }

        public int ChairNumber { get; set; }

        public Student()
        {

        }

    }
}
