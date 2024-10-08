using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestaurantApp.Domain.Entities.GoogleCaptchas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.UserServices.GoogleCaptchaServices
{
    public class GoogleCaptchaService : IGoogleCaptchaService
    {
        private readonly IOptionsMonitor<GoogleCaptchaConfig> _config;

        public GoogleCaptchaService(IOptionsMonitor<GoogleCaptchaConfig> config)
        {
            _config = config;
        }
        public async Task<bool> VerifyToken(string token)
        {
            try
            {
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_config.CurrentValue.SecretKey}&response={token}";

                using (var client = new HttpClient())
                {
                    var httpResult = await client.GetAsync(url);
                    if (httpResult.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }

                    var responseString = await httpResult.Content.ReadAsStringAsync();

                    var googleResult = JsonConvert.DeserializeObject<GoogleCaptchaResponse>(responseString);

                    return googleResult.Success && googleResult.Score >= 0.5;
                }

            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}
