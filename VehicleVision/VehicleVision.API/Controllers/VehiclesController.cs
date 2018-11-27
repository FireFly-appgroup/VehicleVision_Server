using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using VehicleVision.BLL.Services;

namespace VehicleVision.API.Controllers
{
    public class VehiclesController : ControllerBase
    {
        BinaryFormatter formatter = new BinaryFormatter();
        private IVehiclesInformationService VehiclesInformationService;
        private readonly ILogger _logger;
        private readonly IStringLocalizer<VehiclesController> _localizer;

        public VehiclesController(IVehiclesInformationService VehiclesInformationService, ILogger<VehiclesController> logger, IStringLocalizer<VehiclesController> localizer)
        {
            this.VehiclesInformationService = VehiclesInformationService;
            _logger = logger;
            _localizer = localizer;
        }

        [HttpGet]
        [Route("api/[controller]/listofvehicles")]
        public IActionResult GetListOfVehicles()
        {
            var ListOfVehicles = VehiclesInformationService.GetVehicleInformation();

            if (ListOfVehicles == null)
                return NotFound();
            else
                return Ok(ListOfVehicles);
        }
    }
}