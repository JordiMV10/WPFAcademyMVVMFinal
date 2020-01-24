using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
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

         
        public SaveResult<Student> Save()
        {
            return base.Save<Student>();


        }


        public DeleteResult<Student> Delete()  
        {
            var deleteResult = base.Delete<Student>();

            return deleteResult;
        }




        public override ValidationResult Validate()
        {
            var output = base.Validate();

            ValidateName(output);
            ValidateDni(output);
            ValidateChairNumber(output);
            ValidateEmail(output);

            return output;
        }

        public Student Clone()
        {
            return Clone<Student>();
        }

        public override T Clone<T>()
        {
            var output = base.Clone<T>() as Student;

            output.Dni = this.Dni;
            output.ChairNumber = this.ChairNumber;
            output.Name = this.Name;
            output.Email = this.Email;

            return output as T;
        }


        #region Static Validations

        public static ValidationResult<string> ValidateDni(string dni, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(dni))
            {
                output.IsSuccess = false;
                output.Errors.Add("el dni del alumno no puede estar vacío");
            }

            #region check duplication
            var repo = DepCon.Resolve<IRepository<Student>>();

            var entityWithDni = repo.QueryAll().FirstOrDefault(s => s.Dni == dni);


            if (currentId == default && entityWithDni != null)
            {
                // on create
                output.IsSuccess = false;
                output.Errors.Add("ya existe un alumno con ese dni");

            }

            else if (currentId != default && entityWithDni != null && entityWithDni.Id != currentId)    
            {
                if (entityWithDni.Dni == dni)
                {
                    // on update
                    output.IsSuccess = false;
                    output.Errors.Add("ya existe un alumno con ese dni");
                }
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = dni;

            return output;
        }

        public static ValidationResult<string> ValidateName(string name)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(name))
            {
                output.IsSuccess = false;
                output.Errors.Add("el nombre del alumno no puede estar vacío");
            }

            if (output.IsSuccess)
                output.ValidatedResult = name;

            return output;
        }

        public static ValidationResult<int> ValidateChairNumber(string chairNumberText, Guid currentId = default)
        {
            var output = new ValidationResult<int>()
            {
                IsSuccess = true
            };

            var chairNumber = 0;
            var isConversionOk = false;

            #region check null or empty
            if (string.IsNullOrEmpty(chairNumberText))
            {
                output.IsSuccess = false;
                output.Errors.Add("el número de la silla no puede estar vacío o nulo");
            }
            #endregion

            #region check format conversion

            isConversionOk = int.TryParse(chairNumberText, out chairNumber);

            if (!isConversionOk)
            {
                output.IsSuccess = false;
                output.Errors.Add($"no se puede convertir {chairNumberText} en número");
            }

            #endregion

            #region check if the chair is already in use

            if (isConversionOk)
            {

                var repo = DepCon.Resolve<IRepository<Student>>();


                var currentStudentInChair = repo.QueryAll().FirstOrDefault(s => s.ChairNumber == chairNumber);

                if (currentStudentInChair != null && currentStudentInChair.Id != currentId)  
                {

                    output.IsSuccess = false;
                    output.Errors.Add($"ya hay un alumno {currentStudentInChair.Name} en la silla {chairNumber}");
                }
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = chairNumber;

            return output;
        }

        public static ValidationResult<string> ValidateEmail(string email, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(email))
            {
                output.IsSuccess = false;
                output.Errors.Add("el email del alumno no puede estar vacío");


            }

            #region check duplication
            var repo = DepCon.Resolve<IRepository<Student>>();

            var entityWithEmail = repo.QueryAll().FirstOrDefault(s => s.Email == email);

            if (currentId == default && entityWithEmail != null)
            {
                // on create
                output.IsSuccess = false;
                output.Errors.Add("ya existe un alumno con ese email");

            }

            else if (currentId != default && entityWithEmail != null && entityWithEmail.Id != currentId)    //Modificado
            {
                if (entityWithEmail.Email == email)
                {
                    // on update
                    output.IsSuccess = false;
                    output.Errors.Add("ya existe un alumno con ese email");
                }
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = email;

            return output;
        }

        #endregion


        #region Domain Validations

        public void ValidateDni(ValidationResult validationResult)
        {
            var vr = ValidateDni(this.Dni, this.Id);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public void ValidateName(ValidationResult validationResult)
        {
            var vr = ValidateName(this.Name);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }



        public void ValidateChairNumber(ValidationResult validationResult)
        {
            var vr = ValidateChairNumber(this.ChairNumber.ToString(), this.Id);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public void ValidateEmail(ValidationResult validationResult)
        {
            var vr = ValidateEmail(this.Email, this.Id);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }


        #endregion


    }


}

