using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoosballApi.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        private string imageUrl;
        public string ImageUrl
        {
            get {
                if (string.IsNullOrWhiteSpace(imageUrl))
                    return "images/user-icon.png";
                else
                    return imageUrl;
            }
            set { imageUrl = value; }
        }
    }
}
