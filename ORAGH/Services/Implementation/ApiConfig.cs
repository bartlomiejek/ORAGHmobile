﻿using System;
using System.Text.RegularExpressions;

namespace ORAGH.Services.Implementation
{
    public class ApiConfig
    {
		public static string ApiOraghUrl = "http://192.168.0.101/mybbclear";
		public static string ApiUrl = "http://makeup-api.herokuapp.com";

        public static string ApiHostName
        {
            get
            {
				var apiHostName = Regex.Replace(ApiUrl, @"^(?:http(?:s)?://)?(?:www(?:[0-9]+)?\.)?", string.Empty, RegexOptions.IgnoreCase)
                                   .Replace("/", string.Empty);
                return apiHostName;
            }
        }


    }
}
