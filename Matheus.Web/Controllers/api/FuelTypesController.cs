using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Matheus.Data;
using Web.Models;
using AutoMapper;
using Ninject;

namespace Matheus.Web.Controllers
{
	public class FuelTypesController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly DataContext _context;


		public FuelTypesController(DataContext context, IMapper mapper)
		{
			this._context = context;
			this._mapper = mapper;
		}



		// GET: api/FuelTypes
		public IEnumerable<FuelTypeModel> GetFuelTypes()
		{
			var itemList = _context.FuelTypes;
			var result = _mapper.Map<IEnumerable<FuelType>, List<FuelTypeModel>>(itemList);

			return result;
		}

		// GET: api/FuelTypes/5
		[ResponseType(typeof(FuelTypeModel))]
		public IHttpActionResult GetFuelType(Guid id)
		{
			FuelType fuelType = _context.FuelTypes.Find(id);
			if (fuelType == null)
			{
				return NotFound();
			}

			return Ok(fuelType);
		}

		// PUT: api/FuelTypes/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutFuelType(Guid id, FuelType fuelType)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != fuelType.Id)
			{
				return BadRequest();
			}

			_context.Entry(fuelType).State = EntityState.Modified;

			try
			{
				_context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!FuelTypeExists(id))
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

		// POST: api/FuelTypes
		[ResponseType(typeof(FuelType))]
		public IHttpActionResult PostFuelType(FuelType fuelType)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.FuelTypes.Add(fuelType);

			try
			{
				_context.SaveChanges();
			}
			catch (DbUpdateException)
			{
				if (FuelTypeExists(fuelType.Id))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtRoute("DefaultApi", new { id = fuelType.Id }, fuelType);
		}

		// DELETE: api/FuelTypes/5
		[ResponseType(typeof(FuelType))]
		public IHttpActionResult DeleteFuelType(Guid id)
		{
			FuelType fuelType = _context.FuelTypes.Find(id);
			if (fuelType == null)
			{
				return NotFound();
			}

			_context.FuelTypes.Remove(fuelType);
			_context.SaveChanges();

			return Ok(fuelType);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_context.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool FuelTypeExists(Guid id)
		{
			return _context.FuelTypes.Count(e => e.Id == id) > 0;
		}
	}
}