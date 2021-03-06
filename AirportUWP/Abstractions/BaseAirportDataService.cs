﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Newtonsoft.Json;

namespace AirportUWP.Abstractions
{
    public abstract class BaseAirportDataService<TEntity> where TEntity : class
    {
        private readonly string _resourceUrl;

        protected BaseAirportDataService(string resourceUrl)
        {
            _resourceUrl = resourceUrl;
        }

        public async Task<List<TEntity>> GetEntitiesAsync()
        {
            try
            {
                var httpClient = new HttpClient();

                var entities = await httpClient.GetStringAsync($"{_resourceUrl}");
                return JsonConvert.DeserializeObject<List<TEntity>>(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task CreateEntityAsync(TEntity entity)
        {
            var httpClient = new HttpClient();

            using (var memoryStream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(TEntity));
                serializer.WriteObject(memoryStream, entity);
                memoryStream.Position = 0;

                var streamContent = new StreamContent(memoryStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await httpClient.PostAsync(new Uri(_resourceUrl), streamContent);

                var responseMessage = response.EnsureSuccessStatusCode();
                var stream = await responseMessage.Content.ReadAsStreamAsync();

                var readData = serializer.ReadObject(stream) as TEntity;
            }
        }

        public async Task UpdateEntityAsync(string id, TEntity entity)
        {
            var httpClient = new HttpClient();

            using (var memoryStream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(TEntity));
                serializer.WriteObject(memoryStream, entity);
                memoryStream.Position = 0;

                var streamContent = new StreamContent(memoryStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseMessage = await httpClient.PutAsync(new Uri($"{_resourceUrl}/{id}"), streamContent);
                responseMessage.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteEntityAsync(string id)
        {
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.DeleteAsync(new Uri($"{_resourceUrl}/{id}"));
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
