using System;
using ORAGH.Services.Implementation;

namespace ORAGH.Models
{
    public class PostViewData
    {
		public string Pid { get; set; }
        public string Tid { get; set; }
        public string Fid { get; set; }
        public string Username { get; set; }
		public DateTime PostDate { get; set; }
        public string Message { get; set; }

		public PostViewData(Post post)
		{
			Pid = post.pid;
			Tid = post.tid;
			Fid = post.fid;
			Username = post.username; 
			PostDate = ApiDataParser.UnixTimeStampToDateTime(post.dateline);
			Message = post.message; 
		}
    }
}
