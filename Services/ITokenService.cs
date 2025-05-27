using System;
using ExpenseTrackerAPI.Entities;

namespace ExpenseTrackerAPI.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
