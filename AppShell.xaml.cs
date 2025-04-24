using To_Do_List.View;

namespace To_Do_List;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(CreateTodoPage), typeof(CreateTodoPage));
		Routing.RegisterRoute(nameof(DetailsView), typeof(DetailsView));
		Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
	}
}
