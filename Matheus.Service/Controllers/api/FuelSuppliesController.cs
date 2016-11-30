using AutoMapper;
using Matheus.DAL;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Matheus.DAL.Models;
using Matheus.Repository.IRepositories;
using Web.Models;

namespace Matheus.Web.Controllers.api
{
	public class FuelSuppliesController : ApiController
    {
		private readonly IMapper _mapper;
		private readonly IFuelSupplyRepository _repository;

		public FuelSuppliesController(IFuelSupplyRepository fuelSupplyRepository, IMapper mapper)
		{
			this._mapper = mapper;
			this._repository = fuelSupplyRepository;
		}


		// GET: api/FuelSupplies

		[ResponseType(typeof(IEnumerable<FuelSupplyModel>))]
		public IHttpActionResult GetFuelSupplies()
        {
			var itemList = _repository.Get().AsEnumerable();
			var result = _mapper.Map<IEnumerable<FuelSupply>, IEnumerable<FuelSupply>>(itemList);

			return Ok(result);
		}
		

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _repository.Dispose();
            
            base.Dispose(disposing);
        }
    }
}