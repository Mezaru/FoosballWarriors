using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoosballApi.Context;
using FoosballApi.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoosballApi.Controllers
{
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IPlayerService playerService;

        public PlayersController(ILogger<PlayersController> logger, IPlayerService playerService)
        {
            _logger = logger;
            this.playerService = playerService;
        }

        [HttpGet]
        [Route("Players/Get/{id}")]
        public Player Get(int id)
        {
            return playerService.GetPlayer(id);
        }
    }
}
