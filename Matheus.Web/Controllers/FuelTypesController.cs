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

namespace Matheus.Web.Controllers
{
    public class FuelTypesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/FuelTypes
        public IQueryable<FuelType> GetFuelTypes()
        {
            return db.FuelTypes;
        }

        // GET: api/FuelTypes/5
        [ResponseType(typeof(FuelType))]
        public IHttpActionResult GetFuelType(Guid id)
        {
            FuelType fuelType = db.FuelTypes.Find(id);
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

            db.Entry(fuelType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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

            db.FuelTypes.Add(fuelType);

            try
            {
                db.SaveChanges();
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
            FuelType fuelType = db.FuelTypes.Find(id);
            if (fuelType == null)
            {
                return NotFound();
            }

            db.FuelTypes.Remove(fuelType);
            db.SaveChanges();

            return Ok(fuelType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FuelTypeExists(Guid id)
        {
            return db.FuelTypes.Count(e => e.Id == id) > 0;
        }
    }
}