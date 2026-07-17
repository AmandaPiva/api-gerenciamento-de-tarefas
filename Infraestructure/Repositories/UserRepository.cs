using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Users.Interfaces;
using api_gerenciamento_tarefas.Data;
using api_gerenciamento_tarefas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_gerenciamento_tarefas.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.User.AddAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            _context.User.Remove(user);
        }
        
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task UpdateAsync(User user)
        {
            _context.User.Update(user);
        }
        
    }
}