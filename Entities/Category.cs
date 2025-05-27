namespace ExpenseTrackerAPI.Entities;

public class Category
{
    public int Id { set; get; }
    public string Name { set; get; } = string.Empty;
    public List<Expense> Expenses { set; get; } = new();
}
