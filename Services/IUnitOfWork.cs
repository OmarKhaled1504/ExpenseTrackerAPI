using System;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IExpenseRepository Expenses { get; }
    ICategoryRepository Categories { get; }
    Task<int> CompleteAsync();
}
