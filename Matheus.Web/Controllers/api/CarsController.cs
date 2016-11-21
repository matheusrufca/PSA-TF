using AutoMapper;
using Matheus.Data;
using Matheus.Data.DAL;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
		private readonly DataContext _context;


		public CarsController(DataContext context, IMapper mapper)
		{
			this._context = context;
			this._mapper = mapper;
		}

		// GET: api/Cars
		public IHttpActionResult GetCars()
		{
			var itemList = _context.Cars.DistinctBy(x => x.LicencePlate.ToUpper());
			var result = _mapper.Map<IEnumerable<Car>, IEnumerable<CarModel>>(itemList);

			return Ok(result);
		}

		// GET: api/Cars/5
		[ResponseType(typeof(CarModel))]
		public IHttpActionResult GetCar(Guid id)
		{
			Car car;
			CarModel result;

			car = _context.Cars.Find(id);
			result = _mapper.Map<CarModel>(car);

			if (car == null)
				return NotFound();

			return Ok(result);
		}

		// PUT: api/Cars/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutCar(Guid id, EditCarViewModel model)
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
				_context.Entry(car).State = EntityState.Modified;
				_context.SaveChanges();
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

				_context.Cars.Add(car);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				result = _mapper.Map<Car, CarModel>(car);
			}

			return CreatedAtRoute("DefaultApi", new { id = result.Id }, result);
		}

		// DELETE: api/Cars/5
		[ResponseType(typeof(Car))]
		public IHttpActionResult DeleteCar(Guid id)
		{
			Car car = _context.Cars.Find(id);
			CarModel result;
			if (car == null)
				return NotFound();

			_context.Cars.Remove(car);
			_context.SaveChanges();
			result = _mapper.Map<Car, CarModel>(car);

			return Ok(result);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_context.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool CarExists(Guid id)
		{
			return _context.Cars.Any(e => e.CarId == id);
		}
	}
}