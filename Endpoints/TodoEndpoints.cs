using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Models;

namespace MinimalApi.Endpoints
{
    public static class TodoEndpoints
    {
        public static void MapTodoEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/todos", async (AppDbContext context) =>
            {
                var todos = await context.Todos.ToListAsync();
                return Results.Ok(todos);
            });

            routes.MapPost("/todos", async (AppDbContext context, Todo todo) =>
            {
                context.Todos.Add(todo);
                await context.SaveChangesAsync();
                return Results.Created($"todos/{todo.Id}", todo);
            });

            routes.MapPut("/todos/{id}", async (AppDbContext context, int id, Todo todoInput) =>
            {
                var todo = await context.Todos.FindAsync(id);
                if (todo is null) return Results.NotFound();

                todo.Title = todoInput.Title;
                todo.IsCompleted = todoInput.IsCompleted;

                await context.SaveChangesAsync();
                return Results.Ok(todo);
            });

            routes.MapDelete("/todos/{id}", async (AppDbContext context, int id) =>
            {
                var todo = await context.Todos.FindAsync(id);
                if (todo is null) return Results.NotFound();

                context.Todos.Remove(todo);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
