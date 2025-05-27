using System;

namespace ExpenseTrackerAPI.Entities;

public class Expense
{
    public int Id { set; get; }
    public string Name { set; get; } = string.Empty;
    public string Description { set; get; } = string.Empty;
    public decimal Amount { set; get; }
    public DateTime CreatedAt { set; get; }
    public DateTime UpdatedAt { set; get; }

    public  required Category Category { set; get; }
    public int CatId { set; get ; }

    public required string UserId { get; set; }
    public required User User { get; set; }

}
