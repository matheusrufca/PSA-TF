using AutoMapper;
using Matheus.Data;
using Matheus.Data.DAL;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Web.Models;

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
		[ResponseType(typeof(IEnumerable<FuelTypeModel>))]
		public IHttpActionResult GetFuelTypes()
		{
			var itemList = _context.FuelTypes.DistinctBy(x => x.Name.ToUpper());
			var result = _mapper.Map<IEnumerable<FuelType>, IEnumerable<FuelTypeModel>>(itemList);

			return Ok(result);
		}

		// POST: api/FuelTypes
		[ResponseType(typeof(FuelTypeModel))]
		public IHttpActionResult PostFuelType(FuelTypeModel model)
		{
			FuelType fuelType = null;

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			try
			{
				fuelType = _mapper.Map<FuelType>(model);
				_context.FuelTypes.Add(fuelType);
				_context.SaveChanges();
				model = _mapper.Map<FuelTypeModel>(fuelType);
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
			catch (Exception ex)
			{
				throw ex;
			}

			return CreatedAtRoute("DefaultApi", new { id = fuelType.Id }, model);
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