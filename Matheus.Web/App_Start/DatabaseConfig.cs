using Matheus.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Matheus.Web
{
	public static class DatabaseConfig
	{
		public static void Register(HttpConfiguration config)
		{
			InitializeDatabase();
		}


		private static void InitializeDatabase()
		{

			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());

			using (var dataContext = new DataContext())
			{
				if (!Database.Exists("DefaultConnection"))
				{
					dataContext.Database.Initialize(true);
				}
			}
		}
	}








}
