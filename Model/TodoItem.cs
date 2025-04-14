using CommunityToolkit.Mvvm.ComponentModel;

namespace To_Do_List.Model;

public partial class TodoItem : ObservableObject
{
    private static int _nextID = 1;
    public int Id { get; }
    public string Title { get; set; }
    public string? Description { get; set; }

    [ObservableProperty]
    public bool isCompleted;
    public PriorityLevel Priority { get; set; }
    public DateTime CreatedAt { get; }
    
    public TodoItem(string title, string description, PriorityLevel priorityLevel = PriorityLevel.Medium)
    {
        Title = title;
        Description = description;
        Priority = priorityLevel;
        CreatedAt = DateTime.Now;
        Id = TodoItem._nextID++;
        IsCompleted = false;
    }
}