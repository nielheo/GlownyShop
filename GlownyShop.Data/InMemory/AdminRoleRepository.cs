using GlownyShop.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using GlownyShop.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GlownyShop.Data.InMemory
{
    public class AdminRoleRepository : IAdminRoleRepository
    {
        private readonly ILogger _logger;

        public AdminRoleRepository() { }

        public AdminRoleRepository(ILogger<AdminRoleRepository> logger)
        {
            _logger = logger;
        }

        private List<AdminRole> _adminRoles = new List<AdminRole> {
            new AdminRole { Id = 0, Name = "Super Admin" },
            new AdminRole { Id = 1, Name = "User Admin" }
        };

        public AdminRole Add(AdminRole entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<AdminRole> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AdminRole> Get(int id)
        {
            _logger.LogInformation("Get admin role with id = {id}", id);
            return Task.FromResult(_adminRoles.FirstOrDefault(ar => ar.Id == id));
        }

        public Task<AdminRole> Get(int id, string include)
        {
            throw new NotImplementedException();
        }

        public Task<AdminRole> Get(int id, IEnumerable<string> includes)
        {
            throw new NotImplementedException();
        }

        public Task<List<AdminRole>> GetAll()
        {
            _logger.LogInformation("Get all admin roles");
            return Task.FromResult(_adminRoles.ToList());
        }

        public Task<List<AdminRole>> GetAll(string include)
        {
            throw new NotImplementedException();
        }

        public Task<List<AdminRole>> GetAll(IEnumerable<string> includes)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(AdminRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
