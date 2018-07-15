using System;
using Refit;
using System.Threading.Tasks;
using System.Collections.Generic;
using ORAGH.Models;
using System.Net.Http;

namespace ORAGH.Services
{
    [Headers("Content-Type: application/json")]
    public interface IMakeUpApi
    {
        [Get("/api/v1/products.json?brand={brand}")]
        Task<HttpResponseMessage> GetMakeUps(string brand);

        [Post("/api/v1/addMakeUp")]
        Task<MakeUp> CreateMakeUp([Body] MakeUp makeUp, [Header("Authorization")] string token); 
    }
}
