﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using TheWorld.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<TripController> _logger;
        private IWorldRepository _repository;

        public TripController(IWorldRepository repository, ILogger<TripController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            //if (true) return BadRequest("Bad things happened");

            try
            {
                var results = _repository.GetAllTrips();

                return Ok(Mapper.Map<TripViewModel>(results));
            }
            catch (Exception ex)
            {
                // TODO logging
                _logger.LogError($"Failed to get all trips {ex}");

                return BadRequest("Error occured");
            }
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                // save to the database 
                var newTrip = Mapper.Map<Trip>(theTrip);

                return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
            }

            return BadRequest(ModelState);
        }
    }
}
