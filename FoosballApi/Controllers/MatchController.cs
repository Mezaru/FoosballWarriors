using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FoosballApi.Context;
using FoosballApi.Context.Entitys;
using FoosballApi.Hubs;
using FoosballApi.Interface;
using FoosballApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace FoosballApi.Controllers
{
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly ILogger<MatchController> _logger;
        private readonly IHubContext<PlayerHub> hubContext;
        private readonly IMatchService matchService;

        public MatchController(ILogger<MatchController> logger, IHubContext<PlayerHub> hubContext, IMatchService matchService)
        {
            _logger = logger;
            this.hubContext = hubContext;
            this.matchService = matchService;
        }

        [HttpPost]
        [Route("Match/SaveNew")]
        public async Task<bool> SaveNew(MatchModel match)
        {
            return matchService.SaveNewMatch(match);
        }
    }
}
