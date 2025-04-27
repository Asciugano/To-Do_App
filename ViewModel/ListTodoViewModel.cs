using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using To_Do_List.Model;
using To_Do_List.Services;
using To_Do_List.View;

namespace To_Do_List.ViewModel;

public partial class ListTodoViewModel : ObservableObject
{
    public ObservableCollection<ListTodo> ListTodo { get; set; } = TodoService.Istance.ItemList;

    [ObservableProperty]
    bool isRefreshing = false;

    [RelayCommand]
    async Task CreateNewListAsync()
    {
        string title = await Shell.Current.DisplayPromptAsync(
            "Nuova Lista",
            "Inserisci il Titolo: ",
            accept: "OK",
            cancel: "Cancel",
            maxLength: 100,
            keyboard: Keyboard.Default
        );

        if (title is null)
        {
            await Shell.Current.DisplayAlert("ERROR", "Devi inserire un titolo per creare una lista", "OK");
            return;
        }

        try
        {
            TodoService.Istance.ItemList.Add(new ListTodo(title));
            await DatabaseService.AddListTodo(new ListTodo(title));
            await Shell.Current.DisplayAlert("Successo", $"hai aggiunto {title} con successo", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
        }
    }

    [RelayCommand]
    async Task RemoveListAsync(ListTodo list)
    {
        try
        {
            await TodoService.DeleteTodoListAsync(list);
            await Shell.Current.DisplayAlert("Lista eliminata", $"{list.Name} eliminata con successo", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
        }
    }

    [RelayCommand]
    async Task GoToItemsPageAsync(ListTodo listTodo)
    {
        if (listTodo is null)
            return;

        try
        {
            await Shell.Current.GoToAsync($"{nameof(ItemsPage)}", true, new Dictionary<string, object>
            {
                { "ListTodo", listTodo }
            });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
        }
    }
    // [RelayCommand]
    // async Task GoToItemsPageAsync(ListTodo listTodo)
    // {
    //     if(listTodo is null)
    //         return ;

    //     try
    //     {
    //         await Shell.Current.GoToAsync($"{nameof(ItemsPage)}?ListTodoID={listTodo.Id}");
    //     }
    //     catch (Exception ex)
    //     {
    //         await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
    //     }
    // }

    [RelayCommand]
    async Task GetAllListAsync()
    {
        IsRefreshing = true;

        try
        {
            var ListDB = await DatabaseService.GetAllListTodo();
            if (ListTodo.Count <= 0)
                ListTodo.Clear();
            foreach (var list in ListDB)
            {
                ListTodo.Add(list);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
        }
        finally
        {
            IsRefreshing = false;
        }
    }
}