using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Projects.Interfaces;
using api_gerenciamento_tarefas.Application.Interfaces;
using api_gerenciamento_tarefas.Domain.Entities;

namespace api_gerenciamento_tarefas.Application.Features.Projects.Services
{
    public class ProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                throw new Exception("Projeto não encontrado.");
            return project;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _unitOfWork.ProjectRepository.GetAllAsync();
        }

        public async Task<Project> AddAsync(Project project)
        {
             if (string.IsNullOrWhiteSpace(project.Name))
                throw new Exception("O nome do projeto é obrigatório.");

            await _unitOfWork.ProjectRepository.AddAsync(project);

            await _unitOfWork.SaveChangesAsync();

            return project;
        }

        public async Task UpdateAsync(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            var existingProject = await _unitOfWork.ProjectRepository.GetByIdAsync(project.Id);
            
            if (existingProject == null)
                throw new Exception("Projeto não encontrado.");

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;
            existingProject.CompletionDate = project.CompletionDate;
            existingProject.Completed = project.Completed;

            await _unitOfWork.ProjectRepository.UpdateAsync(existingProject);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                throw new Exception("Projeto não encontrado.");

            await _unitOfWork.ProjectRepository.DeleteAsync(project);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}