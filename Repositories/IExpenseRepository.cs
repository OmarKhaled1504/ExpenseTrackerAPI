using System;

namespace ExpenseTrackerAPI.Repositories;

public interface IExpenseRepository
{
    public Task ReassignCategoryAsync(int from, int to);
}
