using ChatApplication.Models;
using ChatApplication.Context_Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChatApplication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> AddUserAsync(Users user);
        Task<Users> GetUserByEmailAsync(string email);
        Task<IEnumerable<Users>> GetUsersAsync();
    }
}


public static class UsersEndpoints
{
	public static void MapUsersEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Users").WithTags(nameof(Users));

        group.MapGet("/", async (UserContext db) =>
        {
            return await db.Users.ToListAsync();
        })
        .WithName("GetAllUsers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Users>, NotFound>> (int id, UserContext db) =>
        {
            return await db.Users.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Users model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUsersById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Users users, UserContext db) =>
        {
            var affected = await db.Users
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, users.Id)
                  .SetProperty(m => m.FullName, users.FullName)
                  .SetProperty(m => m.Email, users.Email)
                  .SetProperty(m => m.Password, users.Password)
                  .SetProperty(m => m.ConfirmPassword, users.ConfirmPassword)
                  .SetProperty(m => m.ContactNumber, users.ContactNumber)
                  .SetProperty(m => m.DateOfBirth, users.DateOfBirth)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUsers")
        .WithOpenApi();

        group.MapPost("/", async (Users users, UserContext db) =>
        {
            db.Users.Add(users);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Users/{users.Id}",users);
        })
        .WithName("CreateUsers")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, UserContext db) =>
        {
            var affected = await db.Users
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUsers")
        .WithOpenApi();
    }
}