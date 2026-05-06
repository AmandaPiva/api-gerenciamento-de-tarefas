using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Users.DTO;
using api_gerenciamento_tarefas.Application.Interfaces;
using api_gerenciamento_tarefas.Domain.Entities;

namespace api_gerenciamento_tarefas.Application.Features.Users.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponseDto?> GetByIdAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Usuário não encontrado.");
                
            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();
        }

        public async Task<User> AddAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash
            };
            if (string.IsNullOrWhiteSpace(user.Name))
                throw new Exception("O nome do usuário é obrigatório.");
            if (string.IsNullOrWhiteSpace(user.Email))
                throw new Exception("O email do usuário é obrigatório.");
            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new Exception("A senha do usuário é obrigatória.");

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(UpdateUserDto dto)
        {
            var existingUser = await _unitOfWork.UserRepository.GetByIdAsync(dto.Id);
            if (existingUser == null)
                throw new Exception("Usuário não encontrado.");

            existingUser.Name = dto.Name;
            existingUser.Email = dto.Email;

            await _unitOfWork.UserRepository.UpdateAsync(existingUser);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Usuário não encontrado.");

            await _unitOfWork.UserRepository.DeleteAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}