﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Fusillade;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Polly;
using Refit;

namespace ORAGH.Services.Implementation
{
    public class ApiManager : IApiManager
    {
        IUserDialogs _userDialogs = UserDialogs.Instance;
        IConnectivity _connectivity = CrossConnectivity.Current;
		IApiService<IOraghApi> oraghApi; 
        public bool IsConnected { get; set; }
        public bool IsReachable { get; set; }
        Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();
        Dictionary<string, Task<HttpResponseMessage>> taskContainer = new Dictionary<string, Task<HttpResponseMessage>>(); 

		public ApiManager( IApiService<IOraghApi> _oraghApi) 
        {
			oraghApi = _oraghApi;
            IsConnected = _connectivity.IsConnected;
            _connectivity.ConnectivityChanged += OnConnectivityChanged;
        }
        
        void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected; 

            if(!e.IsConnected)
            {
                //Cancel All Running Task 
                var items = runningTasks.ToList(); 
                foreach(var item in items)
                {
                    item.Value.Cancel();
                    runningTasks.Remove(item.Key); 
                }
            }
        }
        
		public async Task<HttpResponseMessage> AuthoriseUser(string name, string password)
		{
			var cts = new CancellationTokenSource();
			var task = RemoteRequestAsync<HttpResponseMessage>(oraghApi.GetApi(Priority.UserInitiated).Authenticate(name, password));
			runningTasks.Add(task.Id, cts); 
			return await task; 
		}
        
		public async Task<HttpResponseMessage> GetUser(string username)
		{
			var cts = new CancellationTokenSource();
			var task = RemoteRequestAsync<HttpResponseMessage>(oraghApi.GetApi(Priority.UserInitiated).GetUser(username));
			runningTasks.Add(task.Id, cts);
			return await task; 
		}

		public async Task<HttpResponseMessage> GetForums()
        {
			var cts = new CancellationTokenSource();
			var task = RemoteRequestAsync<HttpResponseMessage>(oraghApi.GetApi(Priority.UserInitiated).GetForums());
			runningTasks.Add(task.Id, cts);
			return await task; 
        }

		public async Task<HttpResponseMessage> GetActiveThreads()
		{
			var cts = new CancellationTokenSource(); 
			var task = RemoteRequestAsync<HttpResponseMessage>(oraghApi.GetApi(Priority.UserInitiated).GetActiveThreads());
			runningTasks.Add(task.Id, cts);
			return await task;
		}

		public async Task<HttpResponseMessage> GetPosts(string threadId)
        {
            var cts = new CancellationTokenSource();
			var task = RemoteRequestAsync<HttpResponseMessage>(oraghApi.GetApi(Priority.UserInitiated).GetPosts(threadId));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

		public async Task<HttpResponseMessage> CreatePost(string username, string password, string threadId, string forumId, string ipaddress, string message)
		{
			var cts = new CancellationTokenSource();
			var task = RemoteRequestAsync<HttpResponseMessage>(oraghApi.GetApi(Priority.UserInitiated).CreatePost(username, password, threadId, forumId, ipaddress, message));
			runningTasks.Add(task.Id, cts);
			return await task;
		}

		public async Task<HttpResponseMessage> GetForumChilds(string fid)
        {
            var cts = new CancellationTokenSource();
			var task = RemoteRequestAsync<HttpResponseMessage>(oraghApi.GetApi(Priority.UserInitiated).GetForumChilds(fid));
            runningTasks.Add(task.Id, cts);
            return await task;
        }
        
		public async Task<HttpResponseMessage> GetThreads(string fid)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(oraghApi.GetApi(Priority.UserInitiated).GetThreads(fid));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

		public string FixOraghApiResponse(string response)
        {
			string pattern = @"""\d+"":{";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(response, "{");
            result = result.Replace("{{", "[{");
            result = result.Replace("}}", "}]"); 
            return result;
        }

		protected async Task<TData> RemoteRequestAsync<TData>(Task<TData> task)
			where TData : HttpResponseMessage,
			new()
		{
			TData data = new TData();

			if (!IsConnected)
			{
				var stringResponse = "Threre's not network connection";
				data.StatusCode = HttpStatusCode.BadRequest;
				data.Content = new StringContent(stringResponse);

				_userDialogs.Toast(stringResponse, TimeSpan.FromSeconds(1));
				return data;
			}
            
			IsReachable = await _connectivity.IsRemoteReachable(ApiConfig.ApiHostName);

			if (!IsReachable)
			{
				var stringResponse = "There's not an internet connection";
				data.StatusCode = HttpStatusCode.BadRequest;
				data.Content = new StringContent(stringResponse);

				_userDialogs.Toast(stringResponse, TimeSpan.FromSeconds(1));
				return data;
			}

			data = await Policy
				.Handle<WebException>()
				.Or<ApiException>()
				.Or<TaskCanceledException>()
				.WaitAndRetryAsync
				(
					retryCount: 1,
					sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
				)
				.ExecuteAsync(async () =>
			{
				var result = await task;
				if (result.StatusCode == HttpStatusCode.Unauthorized)
				{
					//Logout the user
				}

				return result;
			});

			return data;
		}
	}
}
