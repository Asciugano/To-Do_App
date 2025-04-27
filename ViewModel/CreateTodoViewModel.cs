using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using To_Do_List.Model;
using To_Do_List.Services;

namespace To_Do_List.ViewModel;

[QueryProperty ($"{nameof(ListTodo)}", "ListTodo")]
public partial class CreateTodoViewModel : ObservableObject
{
    [ObservableProperty]
    ListTodo listTodo;

    [ObservableProperty]
    string title;
    [ObservableProperty]
    string description;
    
    public ObservableCollection<PriorityLevel> Priorities { get; } = new()
    {
        PriorityLevel.Low,
        PriorityLevel.Medium,
        PriorityLevel.High
    };
    
    [ObservableProperty]
    PriorityLevel selectedPriority = PriorityLevel.Medium;
    
    [RelayCommand]
    async Task SaveAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Description))
            {
                await Shell.Current.DisplayAlert("Error", "Devi inserire tutti i campi", "OK");
                return;
            }

            var newTodo = new TodoItem(Title, Description, ListTodo.Id, SelectedPriority);
            await TodoService.AddTodoItemInListTodo(ListTodo, newTodo);
            await Shell.Current.DisplayAlert("Success", $"Aggiunto {newTodo.Title}, con listID: {newTodo.ListId} la lista ha id: {ListTodo.Id} con successo", "OK");
            CancelInputAsync();
            await Shell.Current.GoToAsync("..", true);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
        }
    }

    [RelayCommand]
    async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..", true);
    }
    
    private async void CancelInputAsync()
    {
        Title = "";
        Description = "";
        SelectedPriority = PriorityLevel.Medium;
    }
}