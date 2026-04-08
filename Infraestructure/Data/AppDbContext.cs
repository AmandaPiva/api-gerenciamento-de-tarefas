using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api_gerenciamento_tarefas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Entities.TaskItem> TaskItem { get; set; }
        public DbSet<Domain.Entities.User> User { get; set; }
        public DbSet<Domain.Entities.SubTask> SubTask { get; set; }
        public DbSet<Domain.Entities.Project> Project { get; set; }
    }
}