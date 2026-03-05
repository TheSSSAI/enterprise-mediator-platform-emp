using System;
using System.Collections.Generic;
using System.Linq;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;

namespace EnterpriseMediator.Domain.UserManagement.Aggregates
{
    public class Role : AggregateRoot<RoleId>
    {
        private readonly List<string> _permissions = new();

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsSystemRole { get; private set; }

        public IReadOnlyCollection<string> Permissions => _permissions.AsReadOnly();

        #pragma warning disable CS8618
        private Role() { }
        #pragma warning restore CS8618

        private Role(RoleId id, string name, string description, bool isSystemRole)
        {
            Id = id;
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new BusinessRuleValidationException("Role name required.");
            Description = description;
            IsSystemRole = isSystemRole;
        }

        public static Role Create(string name, string description)
        {
            return new Role(new RoleId(Guid.NewGuid()), name, description, false);
        }

        public static Role CreateSystemRole(string name, string description)
        {
            return new Role(new RoleId(Guid.NewGuid()), name, description, true);
        }

        public void UpdateDetails(string name, string description)
        {
            if (IsSystemRole) throw new BusinessRuleValidationException("Cannot modify system role details.");
            
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new BusinessRuleValidationException("Role name required.");
            Description = description;
        }

        public void AddPermission(string permission)
        {
            if (IsSystemRole) throw new BusinessRuleValidationException("Cannot modify permissions of a system role.");
            if (string.IsNullOrWhiteSpace(permission)) throw new ArgumentException("Permission cannot be empty.", nameof(permission));
            
            if (!_permissions.Contains(permission))
            {
                _permissions.Add(permission);
            }
        }

        public void RemovePermission(string permission)
        {
            if (IsSystemRole) throw new BusinessRuleValidationException("Cannot modify permissions of a system role.");
            
            _permissions.Remove(permission);
        }
    }
}