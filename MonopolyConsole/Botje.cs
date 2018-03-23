using MonopolyRetreat.Dto;
using MonopolyRetreat.Monocle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MonopolyConsole
{
    class Botje
    {
        private readonly string _token;
        private readonly MonocleClient _monocleClient;

        public Botje(string token)
        {
            _token = token;
            _monocleClient = new MonocleClient("http://178.62.247.230", token);
        }

        public LeaderboardUserDto GetStats()
        {
            return _monocleClient.GetMyOverview();
        }
        public void PerformNextStep()
        {
            var all = _monocleClient.GetAllProperties();
            Console.WriteLine(JsonConvert.SerializeObject(all, Formatting.Indented));
            var buyableProperties = _monocleClient.GetBuyableProperties();
            if (buyableProperties == null) { return; }
            if (buyableProperties.Any())
            {
                var buyableProperty = buyableProperties.OrderByDescending(b => b.Value).FirstOrDefault();
                try
                {
                    _monocleClient.BuyProperty(buyableProperty.X, buyableProperty.Y);
                }
                catch (Exception e)
                {
                    // aankoop property is om een of andere reden niet gelukt. Eigen retry logica
                }
            }
        }
    }
}

