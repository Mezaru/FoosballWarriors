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
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IHubContext<PlayerHub> hubContext;
        private readonly IPlayerService playerService;

        public PlayersController(ILogger<PlayersController> logger, IHubContext<PlayerHub> hubContext, IPlayerService playerService)
        {
            _logger = logger;
            this.hubContext = hubContext;
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
        public async Task<int> SaveNewPlayer(PlayerModel player)
        {
            var result = playerService.SaveNewPlayer(player);
            await hubContext.Clients.All.SendAsync("PlayerList", GetAll());
            return result;
        }

        [HttpPut]
        [Route("Players/Put")]
        public async Task<bool> UpdatePlayer(Player player)
        {
            var result = playerService.UppdatePlayer(player);
            await hubContext.Clients.All.SendAsync("PlayerList", GetAll());
            return result;
        }

        [HttpDelete()]
        [Route("Players/Delete/{id}")]
        public async Task<bool> DeletePlayer(int id)
        {
            var result = playerService.DeletePlayer(id);
            await hubContext.Clients.All.SendAsync("PlayerList", GetAll());
            return result;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("PlayersImage/Post/{playerId}")]
        public async void SavePlayerImage(int playerId)
        {
            var file = Request.Form.Files[0];
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), "Images");

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fullPath = Path.Combine(savePath, fileName);
            var dbPath = Path.Combine("Images", fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);

            playerService.SavePlayerImage(playerId, dbPath);
            await hubContext.Clients.All.SendAsync("PlayerList", GetAll());
        }
    }
}
