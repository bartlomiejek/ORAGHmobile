using System;
using Refit;
using System.Threading.Tasks;
using System.Collections.Generic;
using ORAGH.Models;

namespace ORAGH.Services
{
    public interface IMakeUpApi
    {
        [Get("api/v1/products.json?brand={brand}")]
        Task<List<MakeUp>> GetMakeUps(string brand);
    }
}
