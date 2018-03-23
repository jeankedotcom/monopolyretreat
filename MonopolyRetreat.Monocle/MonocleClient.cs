using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MonopolyRetreat.Dto;
using RestSharp;

namespace MonopolyRetreat.Monocle
{
    public interface IMonocleClient
    {
        LeaderboardUserDto GetMyOverview();
        IEnumerable<PropertyDto> GetAllProperties();
        IEnumerable<PropertyDto> GetBuyableProperties();
        void BuyProperty(long x, long y);
        void SellProperty(long x, long y);
    }

    public class MonocleClient : IMonocleClient
    {
        private readonly RestClient _restClient;
        private readonly string _userToken;

        public MonocleClient(string baseUrl, string userToken)
        {
            _restClient = new RestClient($"{baseUrl}/api");
            _userToken = userToken;
        }

        public void BuyProperty(long x, long y)
        {
            var request = new RestRequest($"properties/buy/{x}/{y}", Method.POST);
            request.AddHeader("Authorization", _userToken);
            var response = _restClient.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException("Property not bought");
            }
        }

        public IEnumerable<PropertyDto> GetAllProperties()
        {
            var request = new RestRequest("properties", Method.GET);
            request.AddHeader("Authorization", _userToken);
            var response = _restClient.Execute<List<PropertyDto>>(request);
            return response.Data;
        }

        public IEnumerable<PropertyDto> GetBuyableProperties()
        {
            var request = new RestRequest("properties/buyable", Method.GET);
            request.AddHeader("Authorization", _userToken);
            var response = _restClient.Execute<List<PropertyDto>>(request);
            return response.Data;
        }

        public LeaderboardUserDto GetMyOverview()
        {
            var request = new RestRequest($"users/leaderboard/{_userToken}", Method.GET);
            var response = _restClient.Execute<LeaderboardUserDto>(request);
            return response.Data;
        }

        public void SellProperty(long x, long y)
        {
            var request = new RestRequest($"properties/sell/{x}/{y}", Method.POST);
            request.AddHeader("Authorization", _userToken);
            var response = _restClient.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException("Property not sold");
            }
        }
    }
}
