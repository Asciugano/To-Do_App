using CommunityToolkit.Mvvm.ComponentModel;
using To_Do_List.ViewModel;

namespace To_Do_List.View;

public partial class DetailsView : ContentPage
{
    public DetailsView()
    {
        InitializeComponent();
        BindingContext = new DetailTodoViewModel();
    }
}