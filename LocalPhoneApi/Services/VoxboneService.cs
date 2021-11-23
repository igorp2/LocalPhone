using LocalPhoneApi.Data;
using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocalPhoneApi.Services
{
    public class VoxboneService : IVoxboneService
    {
        private readonly IConfiguration _configuration;
        private readonly string voxboneBaseUrl;
        private readonly string voxboneUsername;
        private readonly string voxbonePass;
        private readonly HttpClientHandler httpClientHandler;

        private readonly INonUpdatableItemsRepository<SmsRelatedInformationModel> _smsRepository;

        public VoxboneService(IConfiguration configuration,
            INonUpdatableItemsRepository<SmsRelatedInformationModel> smsRepository)
        {
            _configuration = configuration;
            _smsRepository = smsRepository;
            voxboneBaseUrl = _configuration["Voxbone:BaseUrl"];
            voxboneUsername = _configuration["Voxbone:Username"];
            voxbonePass = _configuration["Voxbone:Password"];
            //voxboneBaseUrl = _configuration.GetValue<string>("Voxbone:BaseUrl");
            //voxboneUsername = _configuration.GetValue<string>("Voxbone:Username");
            //voxbonePass = _configuration.GetValue<string>("Voxbone:Password");

            httpClientHandler = new HttpClientHandler
            {
                Credentials = new CredentialCache {
                    { new Uri(voxboneBaseUrl), "Digest", new NetworkCredential(voxboneUsername, voxbonePass) }
                }
            };
        }

        public async Task<SmsRelatedInformationModel> MakeRequestToVoxboneApiAsync
            (StringContent requestContent, string extraUrl)
        {
            using HttpClient httpClient = new HttpClient(httpClientHandler);

            httpClient.BaseAddress = new Uri(voxboneBaseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.PostAsync(extraUrl, requestContent);

            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.Accepted)
            {
                var data = await response.Content.ReadAsStringAsync();
                var DataJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

                var newSmsRelatedInfos = new SmsRelatedInformationModel
                {
                    IdCustomer = extraUrl,
                    TransactionId = DataJson["transaction_id"]
                };

                return await _smsRepository.AddNewItemAsync(newSmsRelatedInfos);

            }

            return null;
        }
    }
}
