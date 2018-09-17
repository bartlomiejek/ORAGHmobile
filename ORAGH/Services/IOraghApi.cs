using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ORAGH.Models;
using Refit; 

namespace ORAGH.Services
{
    public interface IOraghApi
    {
		[Get("/api.php/user/list?apikey=1ad4e29145fb2d970cc3c925f8f981f6")]
        Task<List<User>> GetUsers();

		[Get("/api.php/user/list?apikey=1ad4e29145fb2d970cc3c925f8f981f6&username={username}")]
        Task<HttpResponseMessage> GetUser(string username);

		[Get("/api.php/authenticate?apikey=1ad4e29145fb2d970cc3c925f8f981f6&username={username}&password={password}")]
        Task<HttpResponseMessage> Authenticate(string username, string password);

		[Get("/api.php/forum/list?apikey=1ad4e29145fb2d970cc3c925f8f981f6")]
        Task<HttpResponseMessage> GetForums();

		[Get("/api.php/thread/latestactive/0?output=json&apikey=1ad4e29145fb2d970cc3c925f8f981f6")]
		Task<HttpResponseMessage> GetActiveThreads();

		[Get("/api.php/thread/posts/{threadId}?output=json&apikey=1ad4e29145fb2d970cc3c925f8f981f6")]
		Task<HttpResponseMessage> GetPosts(string threadId);

		[Get("/api.php/createpost?username={username}&password={password}&tid={threadId}&fid={forumId}&ipaddress={ipaddress}&message={message}&apikey=1ad4e29145fb2d970cc3c925f8f981f6")]
		Task<HttpResponseMessage> CreatePost(string username, string password, string threadId, string forumId, string ipaddress, string message);

		[Get("/api.php/forum/childs/{fid}?apikey=1ad4e29145fb2d970cc3c925f8f981f6")]
		Task<HttpResponseMessage> GetForumChilds(string fid);

		[Get("/api.php/forum/threads/{fid}?output=json&apikey=1ad4e29145fb2d970cc3c925f8f981f6")]
		Task<HttpResponseMessage> GetThreads(string fid); 
    }
}
