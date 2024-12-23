using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Database;
using ToDo.Domain.Models;
using ToDo.Infrastructure.Interfaces;

public class TodoService : ITodoService
{
    private readonly ApplicationDbContext _context;

    public TodoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ToDoEntity>> GetTodosAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<ToDoEntity?> GetTodoByIdAsync(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task<IEnumerable<ToDoEntity>> GetIncomingTodosAsync()
    {
        var now = DateTime.UtcNow;
        return await _context.Todos
            .Where(t => t.ExpireDateTime >= now && t.ExpireDateTime < now.AddDays(7))
            .ToListAsync();
    }

    public async Task<ToDoEntity> CreateTodoAsync(ToDoEntity todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task<ToDoEntity> UpdateTodoAsync(int id, ToDoEntity updatedTodo)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) throw new KeyNotFoundException("Todo not found");

        todo.Title = updatedTodo.Title;
        todo.Description = updatedTodo.Description;
        todo.ExpireDateTime = updatedTodo.ExpireDateTime;
        todo.PercentComplete = updatedTodo.PercentComplete;

        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task DeleteTodoAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) throw new KeyNotFoundException("Todo not found");

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
    }

    public async Task MarkAsDoneAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) throw new KeyNotFoundException("Todo not found");

        todo.PercentComplete = 100;
        await _context.SaveChangesAsync();
    }
}