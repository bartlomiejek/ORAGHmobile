using System.Threading.Tasks;
using Refit;
using ORAGH.Models;
using System.Collections.Generic;

namespace ORAGH
{
    public interface IMakeUpApi
    {
        // http://makeup-api.herokuapp.com/api/v1/products.json?brand=maybelline
        [Get("/api/v1/products.json?brand=maybelline")]
        Task<List<MakeUp>> GetMakeUps();
    }
}
