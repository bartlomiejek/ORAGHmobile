using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ORAGH.Models;
using Refit; 

namespace ORAGH.Services
{
    public interface IOraghApi
    {
        [Get("/api.php/user/list?apikey=473c076a5c815b70ae9f59ac0800cfaa")]
        Task<List<User>> GetUsers();

        [Get("/api.php/user/list?apikey=473c076a5c815b70ae9f59ac0800cfaa&username={username}")]
        Task<HttpResponseMessage> GetUser(string username);

        [Get("/api.php/authenticate?apikey=473c076a5c815b70ae9f59ac0800cfaa&username={username}&password={password}")]
        Task<HttpResponseMessage> Authenticate(string username, string password);

        [Get("/api.php/forum/list?apikey=473c076a5c815b70ae9f59ac0800cfaa")]
        Task<HttpResponseMessage> GetForums();

		[Get("/api.php/forum/threads/5?apikey=473c076a5c815b70ae9f59ac0800cfaa")] //TODO - active threads
		Task<HttpResponseMessage> GetActiveThreads(); 
    }
}
