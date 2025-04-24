using System.Threading.Tasks;
using To_Do_List.Services;
using To_Do_List.ViewModel;

namespace To_Do_List.View;

public partial class MainPage : ContentPage
{
	public MainPage(ListTodoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		
		try
		{
			await DatabaseService.InitDB();
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
		}
    }
}