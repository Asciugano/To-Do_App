using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace To_Do_List.Model;

public partial class TodoItem : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [ObservableProperty]
    public int listId;

    [ObservableProperty]
    public string title;

    [ObservableProperty]
    public string description;

    [ObservableProperty]
    public bool isCompleted;

    [ObservableProperty]
    public PriorityLevel priority;
    public DateTime CreatedAt { get; }
    
    public TodoItem(string title, string description, int listId, PriorityLevel priorityLevel = PriorityLevel.Medium)
    {
        Title = title;
        Description = description;
        Priority = priorityLevel;
        CreatedAt = DateTime.Now;
        IsCompleted = false;
        ListId = listId;
    }
    
    public TodoItem()
    {
    }
}