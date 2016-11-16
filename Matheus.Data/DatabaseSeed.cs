using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matheus.Data.DAL
{
	public class DbDataInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DataContext>
	{
		protected override void Seed(DataContext context)
		{
			this.AddFuelTypes(context); //add fuel types
			this.AddCars(context); //add cars
		}


		private ICollection<FuelType> AddFuelTypes(DataContext context)
		{
			var fuelTypes = new List<FuelType>
			{
				new FuelType {
					Name ="Gasolina",
					Price = 3.48M
				},
				new FuelType {
					Name ="Gasolina Aditivada",
					Price = 3.60M
				},
				new FuelType {
					Name ="Álcool",
					Price = 2.56M
				}
			};

			fuelTypes.ForEach(x => context.FuelTypes.Add(x)); // add fuel types
			context.SaveChanges();

			return fuelTypes;
		}

		private ICollection<Car> AddCars(DataContext context)
		{
			var cars = new List<Car>
			{
				new Car {
					LicencePlate = "CAR0001",
					Model = "Pálio",
					Manufacturer = "Fiat",
					Year = 2010,
					FuelTankSize = 80,
					Odometer = new Odometer {
						CurrentDistance = 150,
						TotalDistance = 35000
					}
				}
			};

			cars.ForEach(x => context.Cars.Add(x)); // add fuel types
			context.SaveChanges();

			return cars;
		}
	}



}
