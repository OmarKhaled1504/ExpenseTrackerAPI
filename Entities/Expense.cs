using System;

namespace ExpenseTrackerAPI.Entities;

public class Expense
{
    public int Id { set; get; }
    public string Name { set; get; } = string.Empty;
    public string? Description { set; get; }
    public decimal Amount { set; get; }
    public DateTime CreatedAt { set; get; }
    public DateTime? UpdatedAt { set; get; }

    public  Category? Category { set; get; }
    public int CatId { set; get ; }

    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }

}
