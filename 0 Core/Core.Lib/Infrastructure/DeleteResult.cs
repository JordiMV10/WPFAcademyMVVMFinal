using Common.Lib.Core;
using System;

namespace Common.Lib.Infrastructure
{
    public class DeleteResult<T> where T : Entity
    {
        public ValidationResult Validation { get; set; } = new ValidationResult();

        public bool IsSuccess
        {
            get
            {
                return Validation.IsSuccess;
            }
            set
            {
                Validation.IsSuccess = value;
            }
        }

        public string AllErrors
        {
            get
            {
                return Validation.AllErrors;
            }
        }
        
        public Guid Id { get; set; }

        public DeleteResult<TOut> Cast<TOut>() where TOut : Entity
        {
            var output = new DeleteResult<TOut>
            {
                Id = this.Id,
                Validation = this.Validation
            };

            return output;
        }
    }
}
