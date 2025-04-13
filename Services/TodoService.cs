using System.Collections.ObjectModel;
using System.Diagnostics;
using To_Do_List.Model;

namespace To_Do_List.Services;

public class TodoService
{
    public ObservableCollection<TodoItem> Items { get; } = new();

    private static TodoService _istance;
    public static  TodoService Istance => _istance ??= new TodoService();
}