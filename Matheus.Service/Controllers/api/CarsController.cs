using AutoMapper;
using Matheus.DAL.Models;
using Matheus.Repository.IRepositories;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Web.Models;

namespace Matheus.Web.Controllers.api
{
	public class CarsController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly ICarRepository _repository;


		public CarsController(ICarRepository carRepository, IMapper mapper)
		{
			this._mapper = mapper;
			this._repository = carRepository;
		}

		// GET: api/Cars
		[ResponseType(typeof(IEnumerable<CarModel>))]
		public IHttpActionResult GetCars()
		{
			var result = Enumerable.Empty<CarModel>();


			using (var repository = _repository)
			{
				//var itemList = _context.Cars
				 //.DistinctBy(x => x.LicencePlate.ToUpper())
				 //.ToList();
				var itemList = _repository.Get()
					.DistinctBy(x => x.LicencePlate.ToUpper())
					.ToList();

				result = _mapper.Map<List<CarModel>>(itemList);
			}


			return Ok(result);
		}

		// GET: api/Cars/5
		[ResponseType(typeof(CarModel))]
		public IHttpActionResult GetCar(int id)
		{
			Car car;
			CarModel result;

			car = _repository.GetById(id);
			result = _mapper.Map<CarModel>(car);

			if (car == null)
				return NotFound();

			return Ok(result);
		}

		// PUT: api/Cars/5
		[ResponseType(typeof(CarModel))]
		public IHttpActionResult PutCar(int id, EditCarViewModel model)
		{
			Car car = null;
			CarModel result;

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (id != model.Id)
				return BadRequest();

			if (!CarExists(id))
				return NotFound();

			try
			{
				car = _mapper.Map<Car>(model);

				_repository.Edit(car.Id, car);
				//_context.Entry(car).State = EntityState.Modified;
				//_context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				throw ex;
			}
			finally
			{

				result = _mapper.Map<CarModel>(car);
			}

			return Ok(result);
		}

		// POST: api/Cars
		[ResponseType(typeof(CarModel))]
		public IHttpActionResult PostCar(CreateCarViewModel model)
		{
			Car car = null;
			CarModel result;

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				car = _mapper.Map<CreateCarViewModel, Car>(model);

				_repository.Add(car);
				//_context.Cars.Add(car);
				//_context.SaveChanges();
				result = _mapper.Map<Car, CarModel>(car);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return CreatedAtRoute("DefaultApi", new { id = result.Id }, result);
		}


		// POST: api/Cars
		[ResponseType(typeof(CarModel))]
		[Route("api/cars/{id:int}/add_fuel_suppy")]
		public IHttpActionResult PutAddFuelSupply(int id, AddFuelSupplyViewModel model)
		{
			Car result = null;

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var fuelSupply = _mapper.Map<AddFuelSupplyViewModel, FuelSupply>(model);

				result = _repository.AddFuelSupply(id, fuelSupply);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Ok(result);
		}


		// DELETE: api/Cars/5
		[ResponseType(typeof(CarModel))]
		public IHttpActionResult DeleteCar(int id)
		{
			_repository.Remove(id);
			//_context.Cars.Remove(car);
			//_context.SaveChanges();
			//result = _mapper.Map<Car, CarModel>(car);

			return Ok();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				_repository.Dispose();
			
			base.Dispose(disposing);
		}

		private bool CarExists(int id)
		{
			return _repository.Contains(id);
		}
	}
}