using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyRetreat.Dto;
using MonopolyRetreat.Monocle;

namespace MonopolyConsole
{
    internal class Botje
    {
        private readonly MonocleClient _monocleClient;
        private readonly string _token;

        public Botje(string token)
        {
            _token = token;
            _monocleClient = new MonocleClient("http://178.62.247.230", token);
        }

        public LeaderboardUserDto GetStats()
        {
            return _monocleClient.GetMyOverview();
        }

        private const int MinHouses = 10;
        private const int MinSkyscrapers = 5;
        private const int MinShop = 100;
        private const int MinOlbBuilding = 10;
        private const int MinLargeBuilding = 5;
        private readonly Random _random = new Random();


        public void PerformNextStep()
        {
            var types = new List<PropertyType>
            {
                PropertyType.CornerShop,
                PropertyType.Shop,
                PropertyType.Petrol,
                PropertyType.SkyScraper
            };

            
            TryBuy(types[_random.Next(0, 4)], 1000);
            TryBuy(types[_random.Next(0, 4)], 1000);
            TryBuy(types[_random.Next(0, 4)], 1000);
            TryBuy(types[_random.Next(0, 4)], 1000);
            TryBuy(types[_random.Next(0, 4)], 1000);
            TryBuy(types[_random.Next(0, 4)], 1000);

            //TryBuy(PropertyType.House, MinHouses);
            //if (GetFromType(PropertyType.House) < MinHouses)
            //    return;

            //TryBuy(PropertyType.Shop, MinShop);
            //if (GetFromType(PropertyType.Shop) < MinShop)
            //    return;

            //TryBuy(PropertyType.OldBuilding, MinOlbBuilding);
            //if (GetFromType(PropertyType.OldBuilding) < MinOlbBuilding)
            //    return;

            //TryBuy(PropertyType.LargeBuilding, MinLargeBuilding);

            //TryBuy(PropertyType.SkyScraper, MinSkyscrapers);
            //if (GetFromType(PropertyType.SkyScraper) < MinSkyscrapers)
            //    return;

            //TryBuy(PropertyType.Shop, MinShop);
        }

        private void TryBuy(PropertyType type, int minimum)
        {
            while (GetFromType(type) < minimum)
            {
                if (!Buy(type))
                    break;
            }
        }

        private bool Buy(PropertyType type)
        {
            var buyableProperties = _monocleClient.GetBuyableProperties();
            var stats = _monocleClient.GetMyOverview();

            var candidates = from p in buyableProperties
                where p.Value < stats.AvailableMoney && p.PropertyType == type
                orderby p.Value descending
                select p;

            foreach (var prop in candidates)
                try
                {
                    _monocleClient.BuyProperty(prop.X, prop.Y);
                    return true;
                }
                catch (Exception e)
                {
                    // aankoop property is om een of andere reden niet gelukt. Eigen retry logica
                }

            return false;
        }

        private int GetFromType(PropertyType type)
        {
            var all = _monocleClient.GetAllProperties();
            var stats = _monocleClient.GetMyOverview();

            var fromType = from p in all
                where p.PropertyType == type && stats.Properties.Any(pr => pr.X == p.X && pr.Y == p.Y)
                select p;
            return fromType.Count();
        }
    }
}