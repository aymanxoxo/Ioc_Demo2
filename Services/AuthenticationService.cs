using DataEntities;
using DataEntities.Exceptions;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> _userRepository;
        public AuthenticationService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User IsAuthorized(string userName, string password)
        {
            return _userRepository.Get(x => x.Email == userName && x.Password == password, null, x => x.Roles).FirstOrDefault();
        }

        public void Register(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrEmpty(user.Email))
                throw new ValidationException(nameof(user.Email), DataEntities.Resources.ServerLabel.EmailRequired);

            if (string.IsNullOrEmpty(user.Password))
                throw new ValidationException(nameof(user.Password), DataEntities.Resources.ServerLabel.PasswordRequired);

            var temp = _userRepository.Get(x => x.Email == user.Email);
            if (_userRepository.Get(x => x.Email == user.Email).Any())
                throw new ValidationException(nameof(user.Email), DataEntities.Resources.ServerLabel.AlreadyRegisteredEmail);

            foreach(var role in user.Roles)
            {
                _userRepository.Attach(role, EntityState.Unchanged);
            }

            _userRepository.Add(user);
            _userRepository.SaveChanges();
        }


        public void Dispose()
        {
            _userRepository.Dispose();
        }


    }
}
