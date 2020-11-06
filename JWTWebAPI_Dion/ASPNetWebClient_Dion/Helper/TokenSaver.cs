using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetWebClient_Dion.Helper
{
    public class TokenSaver
    {
        private string token;
        public TokenSaver(string token)
        {
            SetToken(token);
        }

        public string GetToken()
        {
            return this.token;
        }

        private void SetToken(string token)
        {
            this.token = token;
        }
    }
}
