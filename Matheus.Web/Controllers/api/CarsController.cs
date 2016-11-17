using AutoMapper;
using Matheus.Data;
using Matheus.Data.DAL;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
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
		public IQueryable<Car> GetCars()
        {
            return _context.Cars;
        }

        // GET: api/Cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult GetCar(Guid id)
        {
            Car car = _context.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // PUT: api/Cars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCar(Guid id, Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.CarId)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cars
        [ResponseType(typeof(Car))]
        public IHttpActionResult PostCar(CarModel car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			var mappedItem = _mapper.Map<Car>(car);

			_context.Cars.Add(mappedItem);
            _context.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mappedItem.CarId }, car);
        }

        // DELETE: api/Cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult DeleteCar(Guid id)
        {
            Car car = _context.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();

            return Ok(car);
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
            return _context.Cars.Count(e => e.CarId == id) > 0;
        }
    }
}