using System.Threading.Tasks;
using To_Do_List.ViewModel;

namespace To_Do_List.View;

public partial class MainPage : ContentPage
{
	public MainPage(TodoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}