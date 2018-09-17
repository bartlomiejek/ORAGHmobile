using System;
using ORAGH.Services.Implementation; 

namespace ORAGH.Models
{
    public class ThreadViewData
    {
		public string Subject { get; set; }
		public string Tid { get; set; }
		public string Fid { get; set; }
		public string Lastposter { get; set; }
		public string Forumname { get; set; }
		public DateTime LastPostDate { get; set; }

		public ThreadViewData( Thread thread )
		{
			Subject = thread.subject;
			Tid = thread.tid;
			Fid = thread.fid;
			Forumname = thread.forumname; 
			Lastposter = thread.lastposter;
			LastPostDate = ApiDataParser.UnixTimeStampToDateTime(thread.lastpostdateline); 
		}
    }
}
