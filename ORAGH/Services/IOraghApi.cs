using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ORAGH.Models;
using Refit; 

namespace ORAGH.Services
{
    public interface IOraghApi
    {
		[Get("/api.php/user/list?apikey=0c7a707c37748d5c09a5f7581ed7aa2c")]
        Task<List<User>> GetUsers();

		[Get("/api.php/user/list?apikey=0c7a707c37748d5c09a5f7581ed7aa2c&username={username}")]
        Task<HttpResponseMessage> GetUser(string username);

		[Get("/api.php/authenticate?apikey=0c7a707c37748d5c09a5f7581ed7aa2c&username={username}&password={password}")]
        Task<HttpResponseMessage> Authenticate(string username, string password);

		[Get("/api.php/forum/list?apikey=0c7a707c37748d5c09a5f7581ed7aa2c")]
        Task<HttpResponseMessage> GetForums();

		[Get("/api.php/thread/latestactive/0?output=json&apikey=0c7a707c37748d5c09a5f7581ed7aa2c")] //TODO - active threads
		Task<HttpResponseMessage> GetActiveThreads();

		[Get("/api.php/thread/posts/{threadId}?output=json&apikey=0c7a707c37748d5c09a5f7581ed7aa2c")]
		Task<HttpResponseMessage> GetPosts(string threadId); 
    }
}
