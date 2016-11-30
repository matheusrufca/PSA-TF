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

namespace Matheus.Web.Controllers
{
	public class FuelTypesController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly IFuelTypeRepository _repository;


		public FuelTypesController(IFuelTypeRepository fuelTypeRepository, IMapper mapper)
		{
			this._mapper = mapper;
			this._repository = fuelTypeRepository;
		}

		// GET: api/FuelTypes
		[ResponseType(typeof(IEnumerable<FuelTypeModel>))]
		public IHttpActionResult GetFuelTypes()
		{
			var itemList = _repository.Get().DistinctBy(x => x.Name.ToUpper());
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
				_repository.Add(fuelType);
				model = _mapper.Map<FuelTypeModel>(fuelType);
			}
			catch (DbUpdateException ex)
			{
				if (FuelTypeExists(fuelType.Id))
				{
					return Conflict();
				}
				else
				{
					throw ex;
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
				_repository.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool FuelTypeExists(int id)
		{
			return _repository.Contains(id);
		}
	}
}