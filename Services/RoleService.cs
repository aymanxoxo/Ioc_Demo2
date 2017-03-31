using DataEntities;
using Interfaces;
using System;
using System.Linq;

namespace Services
{
    public class RoleService : IRoleService
    {

        private readonly IRepository<Role> _roleRepository;
        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Role GetByName(string roleName, bool createIfNotExists = false)
        {
            var role = _roleRepository.Get(x => x.RoleName == roleName)?.FirstOrDefault();
            if (createIfNotExists && role == null)
            {
                role = new Role { RoleName = roleName };
                Add(role);
            }
            return role;
        }

        public void Add(Role role)
        {
            if (_roleRepository.Get(x => x.RoleName == role.RoleName).Any())
                throw new ArgumentException(DataEntities.Resources.ServerLabel.AlreadyRegisteredRole);

            _roleRepository.Add(role);
            _roleRepository.SaveChanges();
        }

        public void Dispose()
        {
            _roleRepository.Dispose();
        }
    }
}
