using System;

namespace DataEntities.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string elem, string validationMessage)
        {
            Element = elem;
            ValidationMessage = validationMessage;
        }


        public string Element { get; set; }
        public string ValidationMessage { get; set; }
    }
}
