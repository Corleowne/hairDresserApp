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
		public long money { get; set; }

		public long jatt { get; set; }
		public long lorealMoney { get; set; }
		public long kerastaseMoney { get; set; }
		public Production(){}
		public Production(int year,int month,int day, long money, long jatt, long lorealMoney, long kerastaseMoney)
		{
			this.year = year;
			this.month = month;
			this.day = day;
			this.money = money;
			this.jatt = jatt;
			this.lorealMoney = lorealMoney;
			this.kerastaseMoney = kerastaseMoney;
		}
		
    }
}
