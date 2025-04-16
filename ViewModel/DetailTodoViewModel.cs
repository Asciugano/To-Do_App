using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using To_Do_List.Model;

namespace To_Do_List.ViewModel;

[QueryProperty(nameof(TodoItem), "TodoItem")]
public partial class DetailTodoViewModel : ObservableObject
{
    [ObservableProperty]
    TodoItem  todoItem;
    
    [ObservableProperty]
    string title;
    
    [ObservableProperty]
    string descrizione;
    
    public ObservableCollection<PriorityLevel> Priorities { get; } = new()
    {
        PriorityLevel.Low,
        PriorityLevel.Medium,
        PriorityLevel.High
    };
    
    [ObservableProperty]
    PriorityLevel selectedPriority = PriorityLevel.Medium;

    [ObservableProperty]
    bool isChanging = false;
    
    [RelayCommand]
    async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..", true);
    }
    
    [RelayCommand]
    async Task ToggleChangingAsync()
    {
        IsChanging = !IsChanging;
    }
}