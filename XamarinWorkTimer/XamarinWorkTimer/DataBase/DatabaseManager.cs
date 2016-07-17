﻿using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace XamarinWorkTimer
{
    public class DatabaseManager
    {
        SQLiteConnection database;
        SQLiteConnection intervalsDatabase;
        SQLiteConnection summaryDatabase;

        public DatabaseManager()
        {
            database = DependencyService.Get<ISQLite>().GetConnection("database");
            database.CreateTable<DatabaseItem>();

            intervalsDatabase = DependencyService.Get<ISQLite>().GetConnection("intervalsDatabase");
            intervalsDatabase.CreateTable<DatabaseIntervals>();

            summaryDatabase = DependencyService.Get<ISQLite>().GetConnection("summaryDatabase");
            summaryDatabase.CreateTable<DatabaseSummary>();

            //database.DeleteAll<DatabaseItem>();
            //intervalsDatabase.DeleteAll<DatabaseIntervals>();
            //summaryDatabase.DeleteAll<DatabaseSummary>();
        }

        public IEnumerable<DatabaseItem> GetItems()
        {
            return (from i in database.Table<DatabaseItem>() select i).ToList();
        }

        public IEnumerable<DatabaseIntervals> GetIntervals()
        {
            return (from i in intervalsDatabase.Table<DatabaseIntervals>() select i).ToList();
        }
        public IEnumerable<DatabaseSummary> GetSummaries()
        {
            return (from i in summaryDatabase.Table<DatabaseSummary>() select i).ToList();
        }


        public DatabaseItem GetItem(string name)
        {
            return database.Table<DatabaseItem>().FirstOrDefault(x => x.Name == name);
        }

        public DatabaseSummary getSummary(string date)
        {
            return summaryDatabase.Table<DatabaseSummary>().FirstOrDefault(x => x.Date == date);
        }
        public int SummaryTime()
        {
            int result = 0;
            foreach (DatabaseItem item in GetItems())
                if (item.Time > 0)
                    result += item.Time;

            return result;
        }

        public void AddItem(DatabaseItem item)
        {
           database.Insert(item);
        }

        public void AddInterval(DatabaseIntervals interval)
        {
            intervalsDatabase.Insert(interval);
        }

        public void AddSumary(DatabaseSummary summary)
        {
            if (getSummary(summary.Date) == null)
                summaryDatabase.Insert(summary);
            else
                summaryDatabase.Update(summary);
        }

        public void CleanTime()
        {
            database.Query<DatabaseItem>("UPDATE [DatabaseItem] SET Time = 0");
        }

        public void cleanInterval()
        {
            intervalsDatabase.DeleteAll<DatabaseIntervals>();
        }
        public void UpdateItem(string name, int seconds)
        {
            seconds += GetItem(name).Time;
            database.Query<DatabaseItem>("UPDATE [DatabaseItem] SET Time = " + seconds.ToString() + " WHERE Name = '" + name + "'");
        }

        public bool Contain(string name)
        {
            if (GetItem(name) == null)
                return false;
            else
                return true;
        }
        public void DeleteItem(string name)
        {
            database.Delete<DatabaseItem>(name);
        }
    }
}
