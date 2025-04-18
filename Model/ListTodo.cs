using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace To_Do_List.Model;

public partial class ListTodo : ObservableObject
{
    private static int _nextID = 1;
    public int Id { get; }
    
    [ObservableProperty]
    public string name;
    
    ObservableCollection<TodoItem> items = new();
    
    public ListTodo(string name)
    {
        Name = name;
    }
}