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

public partial class TodoViewModel : ObservableObject
{
    public ObservableCollection<TodoItem> Items { get; set; } = TodoService.Istance.Items;

    [ObservableProperty]
    bool isRefreshing = false;

    [RelayCommand]
    async Task GoToCreatePageAsync()
    {
        await Shell.Current.GoToAsync(nameof(CreateTodoPage), true);
    }

    [RelayCommand]
    async Task GetMoreImportantAsync()
    {
        if (Items.Count <= 0)
        {
            await Shell.Current.DisplayAlert("NO Items found", "Nessuna cosa da fare trovata", "OK");
            return;
        }
        var fist = Items.OrderByDescending(i => i.Priority).FirstOrDefault();
        await Shell.Current.DisplayAlert("Piú importante", $"{fist.Title}", "OK");
    }

    [RelayCommand]
    async Task GetAllItemsAsync()
    {
        if (IsRefreshing)
            return;
        IsRefreshing = true;
        try
        {
            if (Items.Count > 0)
                Items.Clear();

            foreach(var item in TodoService.Istance.Items)
                Items.Add(item);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("ERROR", $"Impossibile caricare le cosa da fare: {ex.Message}", "OK");
            return;
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
            TodoService.Istance.Items.Remove(item);
            await Shell.Current.DisplayAlert("Task eliminata", $"{item.Title} Rimossa con successo", "OK");
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
        }
    }
}