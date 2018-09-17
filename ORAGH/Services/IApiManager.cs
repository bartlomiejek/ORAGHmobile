﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using ORAGH.Models; 

namespace ORAGH.Services
{
    public interface IApiManager
    {
		Task<HttpResponseMessage> AuthoriseUser(string name, string password);
		Task<HttpResponseMessage> GetUser(string username);
		Task<HttpResponseMessage> GetForums();
		Task<HttpResponseMessage> GetActiveThreads();
		Task<HttpResponseMessage> GetPosts(string threadId);
		Task<HttpResponseMessage> CreatePost(string username, string password, string threadId, string forumId, string ipaddress, string message);
		string FixOraghApiResponse(string response);
    }
}
