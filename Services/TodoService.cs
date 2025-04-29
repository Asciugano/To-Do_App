using System.Collections.ObjectModel;
using To_Do_List.Model;

namespace To_Do_List.Services;

public class TodoService
{
    public ObservableCollection<TodoItem> Items { get; } = new();
    public ObservableCollection<ListTodo> ItemList { get; set; } = new();

    private static TodoService _istance;
    public static TodoService Istance => _istance ??= new TodoService();

    public static async Task AddTodoItemInListTodo(ListTodo listTodo, TodoItem item)
    {
        try
        {
            if (listTodo is null || item is null)
                return;

            if (listTodo.Id == 0 || item.ListId == 0)
                return;

            listTodo.Count++;
            await DatabaseService.AddTodoItem(item);
            // listTodo.Items.Add(item);
            await Shell.Current.DisplayAlert("SUCCESSO", $"{item.Title} aggiunto con successo", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
            return;
        }
    }

    public static async Task RemoveItemInListTodo(ListTodo listTodo, TodoItem item)
    {
        try
        {
            await DatabaseService.DeleteTodoItem(item);
            // listTodo.Items.Remove(item);
            await Shell.Current.DisplayAlert("SUCCESSO", $"{item.Title} rimosso con successo", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
            return;
        }
    }

    public static async Task DeleteTodoListAsync(ListTodo listTodo)
    {
        Istance.ItemList.Remove(listTodo);
        await DatabaseService.DeleteListTodo(listTodo);
    }
    // public static TodoItem? GetMostImportant(ListTodo list) => list.Items.OrderByDescending(i => i.Priority).FirstOrDefault();

    public static async Task<TodoItem?> GetMostImportant(int listID)
    {
        var items = await DatabaseService.GetTodoItemsInListTodo(listID);
        return items.OrderByDescending(i => i.Priority).FirstOrDefault();
    }

    public async Task AggiornaItems()
    {
        foreach (var item in Items)
        {
            var oldItem = await DatabaseService.GetItemByID(item.Id);
            if (oldItem is null)
                await DatabaseService.AddTodoItem(item);
        }
    }
    
    public static async Task AddList(ListTodo list)
    {
        await DatabaseService.AddListTodo(list);
        await Istance.AggiornaList();
    }

    public async Task AggiornaList()
    {
        foreach (var list in ItemList)
        {
            var oldList = await DatabaseService.GetListTodoByID(list.Id);
            if (oldList is null)
                await DatabaseService.AddListTodo(list);

        }
        await RefreshListe();
    }

    public async Task RefreshListe()
    {
        var listDB = await DatabaseService.GetAllListTodo();
        if (listDB is null)
            return;

        if (ItemList is null || ItemList.Count <= 0)
            ItemList.Clear();
        foreach (var list in listDB)
        {
            ItemList.Add(list);
        }
    }

    public async Task RefreshItems(int listID)
    {
        var ItemsDB = await DatabaseService.GetTodoItemsInListTodo(listID);
        if (ItemsDB is null)
            return;

        if (Items is null || Items.Count <= 0)
            Items.Clear();
        foreach (var item in ItemsDB)
        {
            Items.Add(item);
        }
    }
}