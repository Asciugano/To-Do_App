using Microsoft.Extensions.Logging;
using To_Do_List.View;
using To_Do_List.ViewModel;

namespace To_Do_List;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		
		builder.Services.AddTransient<CreateTodoViewModel>();

		builder.Services.AddTransient<TodoViewModel>();
		builder.Services.AddTransient<DetailTodoViewModel>();
		builder.Services.AddSingleton<ListTodoViewModel>();
		
		builder.Services.AddSingleton<CreateTodoPage>();
		builder.Services.AddTransient<DetailsView>();

		builder.Services.AddTransient<ItemsPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
