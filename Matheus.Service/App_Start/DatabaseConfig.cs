using Matheus.Data.DAL;
using System.Data.Entity;
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

			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EFDataContext>());

			using (var dataContext = new EFDataContext())
			{
				if (!Database.Exists("DefaultConnection"))
				{
					dataContext.Database.Initialize(true);
				}
			}
		}
	}








}
