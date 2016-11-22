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

namespace Matheus.Web.Controllers.api
{
	public class FuelSuppliesController : ApiController
    {
		private readonly IMapper _mapper;
		private readonly DataContext _context;

		public FuelSuppliesController(DataContext context, IMapper mapper)
		{
			this._context = context;
			this._mapper = mapper;
		}


		// GET: api/FuelSupplies

		[ResponseType(typeof(IEnumerable<FuelSupplyModel>))]
		public IHttpActionResult GetFuelSupplies()
        {
			var itemList = _context.FuelSupplies.AsEnumerable();
			var result = _mapper.Map<IEnumerable<FuelSupply>, IEnumerable<FuelSupply>>(itemList);

			return Ok(result);
		}

        // GET: api/FuelSupplies/5
        [ResponseType(typeof(FuelSupplyModel))]
        public IHttpActionResult GetFuelSupply(Guid id)
        {
			FuelSupply fuelSupply;
			FuelSupplyModel result;

			fuelSupply = _context.FuelSupplies.Find(id);
			result = _mapper.Map<FuelSupplyModel>(fuelSupply);

			if (fuelSupply == null)
				return NotFound();

			return Ok(result);
		}

        // PUT: api/FuelSupplies/5
        [ResponseType(typeof(FuelSupplyModel))]
        public IHttpActionResult PutFuelSupply(Guid id, FuelSupplyModel model)
        {
			FuelSupply fuelSupply = null;
			FuelSupplyModel result;

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (id != model.Id)
				return BadRequest();

			if (!FuelSupplyExists(id))
				return NotFound();

			try
			{
				fuelSupply = _mapper.Map<FuelSupply>(model);
				_context.Entry(fuelSupply).State = EntityState.Modified;
				_context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				throw ex;
			}
			finally
			{
				result = _mapper.Map<FuelSupplyModel>(fuelSupply);
			}

			return Ok(result);
		}

        // POST: api/FuelSupplies
        [ResponseType(typeof(FuelSupply))]
        public IHttpActionResult PostFuelSupply(AddFuelSupplyViewModel model)
        {
			FuelSupply fuelSupply = null;
			FuelSupplyModel result;

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				fuelSupply = _mapper.Map<AddFuelSupplyViewModel, FuelSupply>(model);

				_context.FuelSupplies.Add(fuelSupply);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				result = _mapper.Map<FuelSupply, FuelSupplyModel>(fuelSupply);
			}

			return CreatedAtRoute("DefaultApi", new { id = result.Id }, result);
		}

        // DELETE: api/FuelSupplies/5
        [ResponseType(typeof(FuelSupply))]
        public IHttpActionResult DeleteFuelSupply(Guid id)
        {
			FuelSupply fuelSupply = _context.FuelSupplies.Find(id);
			FuelSupplyModel result;
			if (fuelSupply == null)
				return NotFound();

			_context.FuelSupplies.Remove(fuelSupply);
			_context.SaveChanges();
			result = _mapper.Map<FuelSupply, FuelSupplyModel>(fuelSupply);

			return Ok(result);
		}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            
            base.Dispose(disposing);
        }

        private bool FuelSupplyExists(Guid id)
        {
            return _context.FuelSupplies.Count(e => e.FuelSupplyId == id) > 0;
        }
    }
}