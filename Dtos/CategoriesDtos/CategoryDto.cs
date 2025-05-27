using ExpenseTrackerAPI.Dtos.ExpensesDtos;

public class CategoryDto
{
    public int Id { get; }
    public string Name { get; }
    public List<ExpenseDto> Expenses { get; }

    public CategoryDto(int id, string name, List<ExpenseDto> expenses)
    {
        Id = id;
        Name = name;
        Expenses = expenses;
    }

    // Overload for cases without expenses, but you may want to remove it for clarity.
    public CategoryDto(int id, string name) : this(id, name, new List<ExpenseDto>()) { }
}
