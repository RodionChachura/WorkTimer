﻿using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace XamarinWorkTimer.DataBase
{
    public class DB<T> where T : struct
    {
        protected SQLiteConnection database;
        public string Name { get; private set; }
        public DB(string name)
        {
            Name = name;
            database = DependencyService.Get<ISQLite>().GetConnection(name);
            database.CreateTable<T>();
        }

        public List<T> GetAll()
        {
            return (from i in database.Table<T>() select i).ToList();
        }

        public T Get(string pk)
        {
            return database.Find<T>(pk);
        }

        public void DeleteAll()
        {
            database.DeleteAll<T>();
        }

        public void Delete(string pk)
        {
            database.Delete<T>(pk);
        }

        public void Add(T instance)
        {
            database.Insert(instance);
        }
        
        public void Update(T instance)
        {
            database.Update(instance);
        }

        public int Count()
        {
            return GetAll().Count();
        }
    }
}
