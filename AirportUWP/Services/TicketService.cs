using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AirportUWP.Abstractions;
using AirportUWP.Models;
using Newtonsoft.Json;

namespace AirportUWP.Services
{
    public class TicketService : BaseAirportDataService<TicketDto>
    {
        public TicketService() : base("http://localhost:32157/api/v1/flights/tickets")
        {

        }

        public async Task<List<TicketDto>> GetTicketsAsync(Guid flightId)
        {
            try
            {
                var httpClient = new HttpClient();
                var fullUrl = "http://localhost:32157/api/v1/flights/" + flightId.ToString() + "/tickets";

                var entities = await httpClient.GetStringAsync($"{fullUrl}");
                return JsonConvert.DeserializeObject<List<TicketDto>>(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
