using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using hairDresserApp.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace hairDresserApp
{
	public partial class MainPage : ContentPage
	{
		public ObservableCollection<ElementViewModell> elements { get; set; }
		public MainPage()
		{
			InitializeComponent();
			elements = new ObservableCollection<ElementViewModell>();
			ListData();
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			if (productionEntry.Text == null)
				productionEntry.Text = "0";
			if (jattEntry.Text == null)
				jattEntry.Text = "0";
			Production production = new Production(productionDatePicker.Date.Year,productionDatePicker.Date.Month,
				productionDatePicker.Date.Day, int.Parse(productionEntry.Text), int.Parse(jattEntry.Text));
			if (App.ProductionDatabase.GetProduction(production) != null)
			{
				production = App.ProductionDatabase.GetProduction(production);
				production.money = int.Parse(productionEntry.Text);
				production.jatt = int.Parse(jattEntry.Text);
				App.ProductionDatabase.SaveProduction(production);
			}
			else App.ProductionDatabase.SaveProduction(production);

			ListData();
			productionEntry.Text = null;
			jattEntry.Text = null;

		}

		private void ListData()
		{
			elements = null;
			elements = new ObservableCollection<ElementViewModell>();

			TableQuery<Production> productionInMonth = App.ProductionDatabase.GetProductionsMonth(DateTime.Today.Month);
			TableQuery<Production> productionPreviousMonth = App.ProductionDatabase.GetProductionsMonth(DateTime.Today.Month - 1);
			if (productionInMonth != null)
			{
				foreach (Production p in productionInMonth)
				{
					elements.Add(new ElementViewModell
					{
						Production = String.Format("Dátum: {0}:{1}:{2}, Termelés: {3}Ft, jatt: {4}Ft", p.year, p.month, p.day, p.money.ToString("N0"), p.jatt.ToString("N0"))
					});
				}
				listView.ItemsSource = elements;

				Salary(productionInMonth, productionPreviousMonth);
			}
		}

		private void Salary(TableQuery<Production> productionInMonth, TableQuery<Production> productionPreviousMonth)
		{
			double salary = 0;
			double jatt = 0;
			double previousMonthSalaray = 0;
			double previousMonthJatt = 0;
			foreach (Production p in productionPreviousMonth)
			{
				previousMonthSalaray += p.money;
				previousMonthJatt += p.jatt;
			}
			foreach (Production p in productionInMonth)
			{
				salary += p.money;
				jatt += p.jatt;
			}
			salary *= 0.27;
			Month month = (Month)DateTime.Today.Month;

			switch (month)
			{
				case Month.January:
					SetLabel("December", "Január", previousMonthSalaray, salary, previousMonthJatt, jatt);
					break;
				case Month.February:
					SetLabel("Január", "Február", previousMonthSalaray, salary, previousMonthJatt, jatt);
					break;
				case Month.March:
					SetLabel("Február", "Március", previousMonthSalaray, salary, previousMonthJatt, jatt);
					break;
				case Month.April:
					SetLabel("Március", "Április", previousMonthSalaray, salary, previousMonthJatt, jatt);
					break;
				case Month.May:
					SetLabel("Április", "Május", previousMonthSalaray, salary, previousMonthJatt, jatt);
					break;
				case Month.June:
					SetLabel("Május", "Június", previousMonthSalaray, salary, previousMonthJatt, jatt);
					break;
				case Month.July:
					SetLabel("Június", "Július", previousMonthSalaray, salary, previousMonthJatt, jatt);
					break;
				case Month.August:
					SetLabel("Július", "Augusztus", previousMonthSalaray, salary, previousMonthJatt, jatt);
					break;
				case Month.September:
					SetLabel("Augusztus", "Szeptember", previousMonthSalaray, salary, previousMonthJatt, jatt);
					break;
				case Month.October:
					SetLabel("Szeptember", "Oktober", previousMonthSalaray, salary, previousMonthJatt, jatt);
					break;
				case Month.November:
					SetLabel("Oktober", "November", previousMonthSalaray, salary, previousMonthJatt, jatt);
					break;
				case Month.December:
					SetLabel("November", "December", previousMonthSalaray, salary, previousMonthJatt, jatt);
					break;
				default:
					break;
			}



		}
		void SetLabel(string prevoiusMonth, string month, double previousMonthSalaray, double salary, double previousMonthJatt, double jatt)
		{
			earnPreviousMonthLabel.Text = String.Format("{0}: {1}Ft + {2}Ft = {3}Ft",
				prevoiusMonth ,previousMonthSalaray.ToString("N0"), previousMonthJatt.ToString("N0"), (previousMonthSalaray + previousMonthJatt).ToString("N0"));

			earnInMonthLabel.Text = String.Format("{0}: {1}Ft + {2}Ft = {3}Ft",
				month ,salary.ToString("N0"), jatt.ToString("N0"), (salary + jatt).ToString("N0"));
		}
		public enum Month
		{
			NotSet = 0,
			January = 1,
			February = 2,
			March = 3,
			April = 4,
			May = 5,
			June = 6,
			July = 7,
			August = 8,
			September = 9,
			October = 10,
			November = 11,
			December = 12
		}
	}
}
