using Newtonsoft.Json;
using oxygen_tracker.Models;
using oxygen_tracker.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace oxygen_tracker.Services
{
    public class HelperService : IHelperService
    {
        private readonly IHttpClientFactory _httpClient;

        public HelperService(IHttpClientFactory httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<string> GetStateNameAsync(string postalCode)
        {
            try
            {
                var client = _httpClient.CreateClient();
                var response = await client.GetAsync("https://api.postalpincode.in/pincode/" + postalCode);
                if (response.IsSuccessStatusCode)
                {
                    var postAddressString = await response.Content.ReadAsStringAsync();
                    var postalAddress = JsonConvert.DeserializeObject<IEnumerable<PostalResponse>>(postAddressString);
                    var locationAddress = postalAddress.FirstOrDefault().PostOffice.FirstOrDefault();
                    return locationAddress
                        != null ? locationAddress.State : "";
                }

                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}