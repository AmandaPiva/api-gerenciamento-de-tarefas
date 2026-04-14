using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Interfaces;

namespace api_gerenciamento_tarefas.Application.Features.SubTasks.Services
{
    public class SubTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubTaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }   
    }
}