using GogoKit;
using GogoKit.Models.Request;
using GogoKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ViaGoGo.CD.Services
{
    public class ViaGoGoRESTService
    {
        //OAuth2 authentication
        public async Task<ViagogoClient> authenticateViaGoGo()
        {
            ViagogoClient client = new ViagogoClient(new ProductHeaderValue("ViaGoGo", "1.0"),
                                "TaRJnBcw1ZvYOXENCtj5",
                                "ixGDUqRA5coOHf3FQysjd704BPptwbk6zZreELW2aCYSmIT8XJ9ngvN1MuKV");

            OAuth2Token token = await client.OAuth2.GetClientAccessTokenAsync(/*List of scopes*/ new string[] { });
            await client.TokenStore.SetTokenAsync(token);

            return client;
        }
    }
}