using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GlownyShop.Core.Data;
using GlownyShop.Models;
using Microsoft.Extensions.Logging;

namespace GlownyShop.Data.EntityFramework.Repositories
{
    public class AdminUserRepository : IAdminUserRepository
    {
        public AdminUserRepository()
        {
        }

        public AdminUserRepository(ILogger<AdminUserRepository> logger)
        {
        }

        public AdminUser Add(AdminUser entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<AdminUser> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AdminUser> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AdminUser> Get(int id, string include)
        {
            throw new NotImplementedException();
        }

        public Task<AdminUser> Get(int id, IEnumerable<string> includes)
        {
            throw new NotImplementedException();
        }

        public Task<List<AdminUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<AdminUser>> GetAll(string include)
        {
            throw new NotImplementedException();
        }

        public Task<List<AdminUser>> GetAll(IEnumerable<string> includes)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(AdminUser entity)
        {
            throw new NotImplementedException();
        }
    }
}