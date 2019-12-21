using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyPointData
{
    [Serializable]
    public class LoginInfo
    {
        public string Username;
        public string Password;
        public LoginInfo(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
