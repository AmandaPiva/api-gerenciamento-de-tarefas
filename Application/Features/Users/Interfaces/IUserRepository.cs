using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Domain.Entities;

namespace api_gerenciamento_tarefas.Application.Features.Users.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetByIdAsync(Guid id);
        public Task<List<User>> GetAllAsync();
        public Task AddAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(User user);
    }
}