using Academy.Lib.Models;
using Common.Lib.Core.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Lib.Repositories
{
    public interface IStudentsRepository : IRepository<Student>
    {
        Student FindByDni(string dni);
    }
}
