using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FoosballApi.Context;
using FoosballApi.Interface;
using FoosballApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoosballApi.Controllers
{
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly ILogger<RatingsController> _logger;
        private readonly IPlayerService playerService;

        public RatingsController(ILogger<RatingsController> logger, IPlayerService playerService)
        {
            _logger = logger;
            this.playerService = playerService;
        }

        [HttpGet]
        [Route("Ratings/Get")]
        public List<PlayerModel> GetAll()
        {
            return playerService.GetAllPlayer();
        }
    }
}
