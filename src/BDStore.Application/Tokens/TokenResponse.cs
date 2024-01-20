using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDStore.Application.Tokens
{
    public class TokenResponse(string token, DateTime? expires, string userName)
    {
        public string Token { get; set; } = token;
        public DateTime? Expires { get; set; } = expires;
        public string UserName { get; set; } = userName;
    }
}