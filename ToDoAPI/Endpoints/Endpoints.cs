using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Models;

namespace ToDo.WebAPI.Endpoints
{
    public static class Endpoints
    {
        public static void UseWebApi(this IApplicationBuilder app)
        {
            var appBuilder = app as WebApplication;

            appBuilder!.MapGet("/todos", async ([FromServices]TodoService service) => await service.GetTodosAsync());

            appBuilder.MapGet("/todos/{id}", async (int id, [FromServices]TodoService service) =>
            {
                var todo = await service.GetTodoByIdAsync(id);
                return todo is not null ? Results.Ok(todo) : Results.NotFound();
            });

            appBuilder.MapGet("/todos/incoming", async ([FromServices]TodoService service) => await service.GetIncomingTodosAsync());

            appBuilder.MapPost("/todos", async ([FromBody]ToDoEntity todo, [FromServices]TodoService service) =>
            {
                var createdTodo = await service.CreateTodoAsync(todo);
                return Results.Created($"/todos/{createdTodo.Id}", createdTodo);
            });

            appBuilder.MapPut("/todos/{id}", async (int id, [FromBody]ToDoEntity todo, [FromServices]TodoService service) =>
            {
                try
                {
                    var updatedTodo = await service.UpdateTodoAsync(id, todo);
                    return Results.Ok(updatedTodo);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound();
                }
            });

            appBuilder.MapDelete("/todos/{id}", async (int id, [FromServices]TodoService service) =>
            {
                try
                {
                    await service.DeleteTodoAsync(id);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound();
                }
            });

            appBuilder.MapPost("/todos/{id}/mark-as-done", async (int id, TodoService service) =>
            {
                try
                {
                    await service.MarkAsDoneAsync(id);
                    return Results.Ok();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound();
                }
            });



            appBuilder.UseRouting();
            appBuilder.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

}
