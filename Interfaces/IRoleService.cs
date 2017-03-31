using DataEntities;
using System;

namespace Interfaces
{
    public interface IRoleService : IDisposable
    {
        Role GetByName(string roleName, bool createIfNotExist = false);

        void Add(Role role);
    }
}
