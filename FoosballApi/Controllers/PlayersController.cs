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
        public PlayerModel Get(int id)
        {
            return playerService.GetPlayer(id);
        }

        [HttpGet]
        [Route("Players/Get")]
        public List<PlayerModel> GetAll()
        {
            return playerService.GetAllPlayer();
        }

        [HttpPost]
        [Route("Players/Post")]
        public int SaveNewPlayer(PlayerModel player)
        {
            return playerService.SaveNewPlayer(player);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("PlayersImage/Post/{playerId}")]
        public void SavePlayerImage(int playerId)
        {
            var file = Request.Form.Files[0];
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), "Images");

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fullPath = Path.Combine(savePath, fileName);
            var dbPath = Path.Combine("Images", fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);

            playerService.SavePlayerImage(playerId, dbPath);
        }
    }
}
