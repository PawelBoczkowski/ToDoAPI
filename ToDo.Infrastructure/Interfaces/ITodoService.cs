using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Models;

namespace ToDo.Infrastructure.Interfaces
{
    internal interface ITodoService
    {
        Task<IEnumerable<ToDoEntity>> GetTodosAsync();
        Task<ToDoEntity?> GetTodoByIdAsync(int id);
        Task<IEnumerable<ToDoEntity>> GetIncomingTodosAsync();
        Task<ToDoEntity> CreateTodoAsync(ToDoEntity todo);
        Task<ToDoEntity> UpdateTodoAsync(int id, ToDoEntity todo);
        Task DeleteTodoAsync(int id);
        Task MarkAsDoneAsync(int id);
    }
}
