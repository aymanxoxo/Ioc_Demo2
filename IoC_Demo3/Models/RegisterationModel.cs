using DataEntities;
using System.ComponentModel.DataAnnotations;

namespace IoC_Demo3.Models
{
    public class RegisterationModel : User
    {
        [Required(ErrorMessageResourceType = typeof(DataEntities.Resources.ServerLabel), ErrorMessageResourceName = "MatchPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(DataEntities.Resources.ServerLabel), ErrorMessageResourceName = "MatchPassword")]
        public string RepeatPassword { get; set; }

        internal User GetUser()
        {
            return new User { FullName = FullName, Password = Password, Email = Email};
        }
    }
}

