using System;
using System.Net.Http;
using System.Threading.Tasks;
using ORAGH.Models; 

namespace ORAGH.Services
{
    public interface IApiManager
    {
        Task<HttpResponseMessage> GetMakeUps(string brand);
		Task<HttpResponseMessage> AuthoriseUser(string name, string password);
		Task<HttpResponseMessage> GetUser(string username);
		Task<HttpResponseMessage> GetForums();
		Task<HttpResponseMessage> GetActiveThreads(); 
		string FixOraghApiResponse(string response);
    }
}
