using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace hairDresserApp.Data
{
   public interface ISQLite
    {
		SQLiteConnection GetConnection();
    }
}
