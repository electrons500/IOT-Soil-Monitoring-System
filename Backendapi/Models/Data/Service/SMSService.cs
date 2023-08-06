using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;


namespace Backendapi.Models.Data.Service
{
    public class SMSService
    {
        private IConfiguration _configuration;
        public SMSService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool SendSMS(string message, string receiverNumber)
        {
            string apiKey = "6baad515ca0f0ee731913cbe2f9a3f91c4a77ddea294b205389017c2a50c926a";
            string baseurl = _configuration.GetSection("SmsService").GetSection("smsUrl").Value;
            string senderId = _configuration.GetSection("SmsService").GetSection("senderId").Value;

            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseurl)
            };

            HttpResponseMessage responseMessage = httpClient.GetAsync($"{httpClient.BaseAddress}/v4/message/sms/send?key={apiKey}&text={message}&type=0&sender={senderId}&to={receiverNumber}").Result;
            if(responseMessage.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

    }
}
