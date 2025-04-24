using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace To_Do_List.Model;

public partial class ListTodo : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [ObservableProperty]
    public string name;
    
    [ObservableProperty]
    public int count;
    
    // public ObservableCollection<TodoItem> Items { get; set; } = new();
    
    public ListTodo(string name)
    {
        Name = name;
    }
    
    public ListTodo()
    {
    }
}