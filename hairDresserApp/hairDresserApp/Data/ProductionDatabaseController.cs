using hairDresserApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace hairDresserApp.Data
{
   public class ProductionDatabaseController
    {
		static object locker = new object();
		SQLiteConnection database;

		public ProductionDatabaseController()
		{
			database = DependencyService.Get<ISQLite>().GetConnection();
			database.CreateTable<Production>();
		}

		public TableQuery<Production> GetProductionsMonth(int month)
		{
			lock (locker)
			{
				if (database.Table<Production>().Count() == 0) return null;
				else return from p in database.Table<Production>()
							where p.year.Equals(DateTime.Today.Year) && p.month.Equals(month)
							orderby p.day descending
							select p;
			}
		}

		public void delete()
		{
			lock (locker)
			{
				var prod = from p in database.Table<Production>()
				select p;
				foreach (var p in prod)
				{
					database.Delete(p);
				}
			}

		}



		public Production GetProduction(Production production)
		{

			if (database.Table<Production>().Count() == 0) return null;
			else
			{
				TableQuery<Production> prod = from p in database.Table<Production>()
				where p.year.Equals(production.year) && p.month.Equals(production.month) && p.day.Equals(production.day)
											  select p;
				if (prod.Count() == 0) return null;
				else return prod.First();
			}
		}

		public Production GetProductionByDate(DateTime date)
		{

			if (database.Table<Production>().Count() == 0) return null;
			else
			{
				TableQuery<Production> prod = from p in database.Table<Production>()
											  where p.year.Equals(date.Year) && p.month.Equals(date.Month) && p.day.Equals(date.Day)
											  select p;
				if (prod.Count() == 0) return null;
				else return prod.First();
			}
		}


		public int SaveProduction(Production production)
		{
			lock (locker)
			{
				if (production.id != 0)
				{
					database.Update(production);
					return production.id;
				}
				else
				{
					return	database.Insert(production);
				}
			}
		}

		public int DeleteProduction(int id)
		{
			lock (locker)
			{
				return database.Delete<Production>(id);
			}

		}
    }
}
