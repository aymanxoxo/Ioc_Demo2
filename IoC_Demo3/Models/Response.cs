using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoC_Demo3.Models
{
    public class Response
    {
        public Response()
        {
            IsSuccess = false;
        }

        public Response(ValidationError validationError)
        {
            ValidationErrors = new List<ValidationError> { validationError };
        }

        public Response(List<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public bool IsSuccess { get; set; }

        public List<ValidationError> ValidationErrors { get; set; }
    }

    public class Response<T> : Response
    {
        public Response(T result)
        {
            IsSuccess = true;
            Result = result;
        }


        public T Result { get; set; }
    }

    public class ValidationError
    {
        public ValidationError(string error, string fieldName = null)
        {
            FieldName = fieldName;
            Error = error;
        }

        public string FieldName { get; set; }

        public string Error { get; set; }
    }
}