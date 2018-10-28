using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using hairDresserApp.Data;
[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace hairDresserApp
{
	public partial class App : Application
	{
		static ProductionDatabaseController productionDatabase;
		public static bool test { get; set; }

		public App ()
		{
			InitializeComponent();

			MainPage = new MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

		public static ProductionDatabaseController ProductionDatabase
		{
			get
			{
				if (productionDatabase == null)
				productionDatabase = new ProductionDatabaseController();
				return productionDatabase;
			}
		}
	}
}
