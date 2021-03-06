﻿using EquipmentStore.BLL.Services;
using EquipmentStore.Core.Entities;
using EquipmentStore.Core.Loggers;
using ExpressMapper;
using System.Web.Mvc;

namespace EquipmentStore.Web.Controllers
{
    public class AdminController : BaseController
	{
		private readonly IService<Product> _productService;
		private readonly IService<Pump> pumpService;
		private readonly IService<Output> _outputService;
		private readonly IMappingServiceProvider _mapper;

		public AdminController(IService<Product> productService,
			IService<Pump> pumpService,
			IService<Output> outputService,
            IMappingServiceProvider mapper,
            ILogger logger) : base(logger)
		{
			_productService = productService;
			this.pumpService = pumpService;
			_outputService = outputService;
			_mapper = mapper;
		}

		[Authorize]
		[HttpGet]
		[Route("admin")]
		public ActionResult Index()
		{
			return View();
		}
	}
}