using To_Do_List.ViewModel;

namespace To_Do_List.View;

public partial class CreateTodoPage : ContentPage
{
    public CreateTodoPage()
    {
        InitializeComponent();
        BindingContext = new CreateTodoViewModel();
    }
}