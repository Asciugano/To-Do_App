using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Formats.Asn1;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using To_Do_List.Model;
using To_Do_List.Services;
using To_Do_List.View;

namespace To_Do_List.ViewModel;

[QueryProperty ($"{nameof(ListTodo)}", "ListTodo")]
public partial class TodoViewModel : ObservableObject
{
    [ObservableProperty]
    ListTodo? listTodo;

    [ObservableProperty]
    bool isRefreshing = false;
    
    public ObservableCollection<TodoItem> Items { get; set; } = new();

    [RelayCommand]
    async Task GoToCreatePageAsync()
    {
        await Shell.Current.GoToAsync(nameof(CreateTodoPage), true, new Dictionary<string, object>
        {
            { "ListTodo", ListTodo }
        });
    }

    [RelayCommand]
    async Task GetMoreImportantAsync()
    {
        var items = await DatabaseService.GetTodoItemsInListTodo(ListTodo.Id);
        if (items.Count <= 0)
        {
            await Shell.Current.DisplayAlert("NO Items found", "Nessuna cosa da fare trovata", "OK");
            return ;
        }
        var first = await TodoService.GetMostImportant(ListTodo.Id);
        if(first is not null)
            await Shell.Current.DisplayAlert("Piú importante", $"{first.Title}", "OK");
        else
            await Shell.Current.DisplayAlert("Nulla trovato", "non ho trovato nulla di importatnte da fare", "OK"); 
    }

    [RelayCommand]
    async Task GetAllItemsAsync()
    {
        IsRefreshing = true;
        try
        {
            var itemDB = await DatabaseService.GetTodoItemsInListTodo(ListTodo.Id);
            if(Items.Count <= 0)
                Items.Clear();
            foreach(var item in itemDB)
            {
                Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
            return ;
        }
        finally
        {
            IsRefreshing = false;
        }
    }
    
    [RelayCommand]
    async Task DeleteItemAsync(TodoItem item)
    {
        try
        {
            await TodoService.RemoveItemInListTodo(ListTodo, item);
            await Shell.Current.DisplayAlert("Task eliminata", $"{item.Title} Rimossa con successo", "OK");
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
        }
    }
    
    [RelayCommand]
    async Task GoToDetailsPageAsync(TodoItem todoItem)
    {
        if(todoItem is null)
            return ;
        
        await Shell.Current.GoToAsync($"{nameof(DetailsView)}", true, new Dictionary<string, object>
        {
            { "TodoItem", todoItem }
        });
    }

    [RelayCommand]
    async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..", true);
    }
}