using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using hairDresserApp.Data;
using hairDresserApp.Droid.Data;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]

namespace hairDresserApp.Droid.Data
{
	class SQLite_Android : ISQLite
	{

		public SQLite_Android() { }
		public SQLite.SQLiteConnection GetConnection()
		{
			string sqliteFileName = "TestDB.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			string path = Path.Combine(documentsPath, sqliteFileName);
			SQLiteConnection conn = new SQLite.SQLiteConnection(path,true);
			return conn;
		}
	}
}