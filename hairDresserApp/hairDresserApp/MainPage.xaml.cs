using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using hairDresserApp.Models;
using SQLite;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace hairDresserApp
{
	public partial class MainPage : ContentPage
	{
		public ObservableCollection<ElementViewModell> elements { get; set; }
		public MainPage()
		{
			InitializeComponent();
			elements = new ObservableCollection<ElementViewModell>();
			//App.ProductionDatabase.delete();
	
			ListData();
		}


		private void Button_Clicked(object sender, EventArgs e)
		{
			Production prod = App.ProductionDatabase.GetProductionByDate(productionDatePicker.Date);
			if (prod != null)
			{
				if (productionEntry.Text == ""  || productionEntry.Text == null)
					productionEntry.Text = prod.money.ToString();
				if (jattEntry.Text == "" || jattEntry.Text == null)
					jattEntry.Text = prod.jatt.ToString();
				if (lorealEntry.Text == "" || lorealEntry.Text == null)
					lorealEntry.Text = prod.lorealMoney.ToString();
				if (kerastaseEntry.Text == "" || kerastaseEntry.Text == null)
					kerastaseEntry.Text = prod.kerastaseMoney.ToString();
			}
			else
			{
				if (productionEntry.Text == "" || productionEntry.Text == null)
					productionEntry.Text = "";
				if (jattEntry.Text == "" || jattEntry.Text == null)
					jattEntry.Text = "";
				if (lorealEntry.Text == "" || lorealEntry.Text == null)
					lorealEntry.Text = "";
				if (kerastaseEntry.Text == "" || kerastaseEntry.Text == null)
					kerastaseEntry.Text = "";
			}

			Production production = new Production(productionDatePicker.Date.Year,productionDatePicker.Date.Month,
				productionDatePicker.Date.Day, long.Parse(productionEntry.Text, System.Globalization.NumberStyles.Number),
				long.Parse(jattEntry.Text, System.Globalization.NumberStyles.Number), long.Parse(lorealEntry.Text, System.Globalization.NumberStyles.Number), long.Parse(kerastaseEntry.Text, System.Globalization.NumberStyles.Number));
			if (App.ProductionDatabase.GetProduction(production) != null)
			{
				production = App.ProductionDatabase.GetProduction(production);
				production.money = long.Parse(productionEntry.Text, System.Globalization.NumberStyles.Number);
				production.jatt = long.Parse(jattEntry.Text, System.Globalization.NumberStyles.Number);
				production.lorealMoney = long.Parse(lorealEntry.Text, System.Globalization.NumberStyles.Number);
				production.kerastaseMoney = long.Parse(kerastaseEntry.Text, System.Globalization.NumberStyles.Number);
				App.ProductionDatabase.SaveProduction(production);
			}
			else App.ProductionDatabase.SaveProduction(production);

			ListData();
			productionEntry.Text = "";
			jattEntry.Text = "";
			lorealEntry.Text = "";
			kerastaseEntry.Text = "";

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
						Production = String.Format("Dátum: {0}:{1}:{2}, Termelés: {3}Ft\n jatt: {4}Ft, L'oréal: {5}Ft\n Kérastase: {6}Ft",
						p.year, p.month, p.day, p.money.ToString("N0"), p.jatt.ToString("N0"),p.lorealMoney.ToString("N0"), p.kerastaseMoney.ToString("N0"))
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
			double loreal = 0;
			double previousMonthLoreal = 0;
			double kerastase = 0;
			double previousMonthKerastase = 0;
			foreach (Production p in productionPreviousMonth)
			{
				previousMonthSalaray += p.money;
				previousMonthJatt += p.jatt;
				previousMonthLoreal += p.lorealMoney;
				previousMonthKerastase += p.kerastaseMoney;
			}
			foreach (Production p in productionInMonth)
			{
				salary += p.money;
				jatt += p.jatt;
				loreal += p.lorealMoney;
				kerastase += p.kerastaseMoney;
			}
			salary *= 0.27;
			loreal *= 0.05;
			kerastase *= 0.1;

			Month month = (Month)DateTime.Today.Month;

			switch (month)
			{
				case Month.January:
					SetLabel("December", "Január", previousMonthSalaray, salary, previousMonthJatt, jatt, previousMonthLoreal, loreal, previousMonthKerastase, kerastase);
					break;
				case Month.February:
					SetLabel("Január", "Február", previousMonthSalaray, salary, previousMonthJatt, jatt, previousMonthLoreal, loreal, previousMonthKerastase, kerastase);
					break;
				case Month.March:
					SetLabel("Február", "Március", previousMonthSalaray, salary, previousMonthJatt, jatt, previousMonthLoreal, loreal, previousMonthKerastase, kerastase);
					break;
				case Month.April:
					SetLabel("Március", "Április", previousMonthSalaray, salary, previousMonthJatt, jatt, previousMonthLoreal, loreal, previousMonthKerastase, kerastase);
					break;
				case Month.May:
					SetLabel("Április", "Május", previousMonthSalaray, salary, previousMonthJatt, jatt, previousMonthLoreal, loreal, previousMonthKerastase, kerastase);
					break;
				case Month.June:
					SetLabel("Május", "Június", previousMonthSalaray, salary, previousMonthJatt, jatt, previousMonthLoreal, loreal, previousMonthKerastase, kerastase);
					break;
				case Month.July:
					SetLabel("Június", "Július", previousMonthSalaray, salary, previousMonthJatt, jatt, previousMonthLoreal, loreal, previousMonthKerastase, kerastase);
					break;
				case Month.August:
					SetLabel("Július", "Augusztus", previousMonthSalaray, salary, previousMonthJatt, jatt, previousMonthLoreal, loreal, previousMonthKerastase, kerastase);
					break;
				case Month.September:
					SetLabel("Augusztus", "Szeptember", previousMonthSalaray, salary, previousMonthJatt, jatt, previousMonthLoreal, loreal, previousMonthKerastase, kerastase);
					break;
				case Month.October:
					SetLabel("Szeptember", "Oktober", previousMonthSalaray, salary, previousMonthJatt, jatt, previousMonthLoreal, loreal, previousMonthKerastase, kerastase);
					break;
				case Month.November:
					SetLabel("Oktober", "November", previousMonthSalaray, salary, previousMonthJatt, jatt, previousMonthLoreal, loreal, previousMonthKerastase, kerastase);
					break;
				case Month.December:
					SetLabel("November", "December", previousMonthSalaray, salary, previousMonthJatt, jatt, previousMonthLoreal, loreal, previousMonthKerastase, kerastase);
					break;
				default:
					break;
			}



		}
		void SetLabel(string prevoiusMonth, string month, double previousMonthSalaray, double salary, double previousMonthJatt, double jatt,
			double previousMonthLoreal, double loreal, double previousMonthKerastase, double kerastase)
		{
			earnPreviousMonthLabel.Text = String.Format("{0}: {1}Ft + {2}Ft + {3} + {4}Ft = {5}Ft",
				prevoiusMonth ,previousMonthSalaray.ToString("N0"), previousMonthJatt.ToString("N0"),previousMonthLoreal.ToString("N0"), previousMonthKerastase.ToString("N0"),
				(previousMonthSalaray + previousMonthJatt + previousMonthLoreal + previousMonthKerastase).ToString("N0"));

			earnInMonthLabel.Text = String.Format("{0}: {1}Ft + {2}Ft + {3} + {4}Ft = {5}Ft",
				month ,salary.ToString("N0"), jatt.ToString("N0"),loreal.ToString("N0"),kerastase.ToString("N0"), (salary + jatt + loreal + kerastase).ToString("N0"));
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
