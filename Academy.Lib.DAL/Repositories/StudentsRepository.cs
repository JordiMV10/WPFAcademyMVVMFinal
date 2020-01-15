using Academy.Lib.Models;
using Academy.Lib.Repositories;
using Common.Lib.DAL.EFCore;
using Common.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Lib.DAL.Repositories
{
    
    public class StudentsRepository : EfCoreRepository<Student>, IStudentsRepository
    {
        static Dictionary<string, Student> StudentsByDni { get; set; } = new Dictionary<string, Student>();

        public StudentsRepository()
        {

        }

        public StudentsRepository(AcademyDbContext dbcontext)
            : base(dbcontext)
        {

        }

        public override SaveResult<Student> Add(Student entity)
        {
            var output = base.Add(entity);

            if (output.IsSuccess)
            {
                StudentsByDni.Add(entity.Dni, entity);
            }

            return output;
        }

        public override SaveResult<Student> Update(Student entity)
        {
            var output = base.Update(entity);

            if (output.IsSuccess)
            {
                StudentsByDni[entity.Dni] = entity;
            }

            return output;
        }

        public override DeleteResult<Student> Delete(Student entity)
        {
            var output = base.Delete(entity);

            if (output.IsSuccess)
            {
                StudentsByDni.Remove(entity.Dni);
            }

            return output;
        }

        public Student FindByDni(string dni)
        {
            if (StudentsByDni.ContainsKey(dni))
                return StudentsByDni[dni];

            return null;
        }
    }




}
