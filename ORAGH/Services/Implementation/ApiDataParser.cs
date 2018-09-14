using System;
namespace ORAGH.Services.Implementation
{
    public static class ApiDataParser
    {
		public static DateTime UnixTimeStampToDateTime(string unixTimestampString)
        {
			// Unix timestamp is seconds past epoch
			double unixTimeStamp = Convert.ToDouble(unixTimestampString);  
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
