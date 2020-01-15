using Common.Lib.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Lib.Models
{
    public class Subject : Entity
    {
        public string Name { get; set; }
        public string Teacher { get; set; }

        public Subject()
        {

        }
    }

}
