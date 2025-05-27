using System;
using ExpenseTrackerAPI.Dtos.UsersDtos;
using ExpenseTrackerAPI.Entities;

namespace ExpenseTrackerAPI.Repositories;

public interface IUserRepository
{
    public Task<(User? User, IEnumerable<string>? Errors)> CreateAsync(User user, string password);
}
