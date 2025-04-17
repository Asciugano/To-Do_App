using CommunityToolkit.Mvvm.ComponentModel;

namespace To_Do_List.Model;

public partial class TodoItem : ObservableObject
{
    private static int _nextID = 1;
    public int Id { get; }

    [ObservableProperty]
    public string title;

    [ObservableProperty]
    public string description;

    [ObservableProperty]
    public bool isCompleted;

    [ObservableProperty]
    public PriorityLevel priority;
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