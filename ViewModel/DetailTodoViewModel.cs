using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using To_Do_List.Model;
using To_Do_List.Services;

namespace To_Do_List.ViewModel;

[QueryProperty($"{nameof(ListTodo)}", "ListTodo")]
[QueryProperty($"{nameof(TodoItem)}", "TodoItem")]
public partial class DetailTodoViewModel : ObservableObject
{
    [ObservableProperty]
    TodoItem  todoItem;
    
    public ObservableCollection<PriorityLevel> Priorities { get; } = new()
    {
        PriorityLevel.Low,
        PriorityLevel.Medium,
        PriorityLevel.High
    };

    [ObservableProperty]
    PriorityLevel selectedPriority;

    [ObservableProperty]
    string title;
    
    [ObservableProperty]
    string descrizione;
    
    [ObservableProperty]
    bool isChanging = false;

    partial void OnTodoItemChanged(TodoItem value)
    {
        if(value is null)
            return ;

        Title = value.Title;
        Descrizione = value.Description;
        SelectedPriority = value.Priority;
    }
    
    
    [RelayCommand]
    async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..", true);
        IsChanging = false;
    }
    
    [RelayCommand]
    async Task ToggleChangingAsync()
    {
        IsChanging = !IsChanging;
    }
    
    [RelayCommand]
    async Task SaveAsync()
    {
        if(string.IsNullOrWhiteSpace(TodoItem.Title) || string.IsNullOrWhiteSpace(TodoItem.Description))
        {
            await Shell.Current.DisplayAlert("ERROR", "Devi inserire tutti i campi", "OK");
            return ;
        }

        TodoItem.Title = Title;
        TodoItem.Description = Descrizione;
        TodoItem.Priority = SelectedPriority;
        
        await GoBackAsync();
    }
}