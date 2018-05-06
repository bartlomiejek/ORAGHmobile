using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ORAGH.Models;
using Refit; 

namespace ORAGH
{
   // [Headers("apikey: 473c076a5c815b70ae9f59ac0800cfaa")]
    public interface IOraghApi
    {
        [Get("/api.php/user/list?apikey=473c076a5c815b70ae9f59ac0800cfaa")]
        Task<List<User>> GetUsers();

        [Get("/api.php/user/list?apikey=473c076a5c815b70ae9f59ac0800cfaa&username={username}")]
        Task<string> GetUser(string username);//Task<user> - sprawdźić kodowanie api
    }
}
