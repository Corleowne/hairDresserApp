using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace hairDresserApp.Models
{
	public class Production
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		
		public int year { get; set; }
		public int month { get; set; }
		public int day { get; set; }
		public int money { get; set; }

		public int jatt { get; set; }
		public Production(){}
		public Production(int year,int month,int day, int money, int jatt)
		{
			this.year = year;
			this.month = month;
			this.day = day;
			this.money = money;
			this.jatt = jatt;
		}
		
    }
}
