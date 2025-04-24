using System.Threading.Tasks;
using To_Do_List.Services;

namespace To_Do_List;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}
	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}