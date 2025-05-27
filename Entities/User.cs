using System;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTrackerAPI.Entities;

public class User : IdentityUser
{
    public List<Expense> Expenses { set; get; } = new();

}
