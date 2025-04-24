using To_Do_List.ViewModel;

namespace To_Do_List.View;

public partial class ItemsPage : ContentPage
{
	public ItemsPage(TodoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}