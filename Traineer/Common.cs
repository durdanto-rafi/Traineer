using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Traineer
{
    public class Common
    {
        public string Encode(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }

        public string Decode(string encodedServername)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }
    }
}