using DataEntities;
using System;

namespace Interfaces
{
    public interface IAuthenticationService : IDisposable
    {
        User IsAuthorized(string userName, string password);
        void Register(User p);
    }
}
