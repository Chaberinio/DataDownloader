using Core.Interfaces;
using Core.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TokenRefresher
{
    public class RevolutTokenRefresher : ITokenRefresher
    {
        public async Task<string> GetToken()
        {
            string url = "https://sandbox-b2b.revolut.com/api/1.0/auth/token";

            var dict = new Dictionary<string, string>();
            dict.Add("grant_type", "refresh_token");
            dict.Add("refresh_token", "oa_sand_tn2IVETVE-Y-frCxq7xC1ax1_bq4j3u2m4bxtnJATSw");
            dict.Add("client_id", "q5iANB5dr3I9-lxa6yJAKq_8gA0GdFH48mtipR0tQkw");
            dict.Add("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer");
            dict.Add("client_assertion", "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJ3d3cuYW5kcmEuY29tLnBsIiwic3ViIjoicTVpQU5CNWRyM0k5LWx4YTZ5SkFLcV84Z0EwR2RGSDQ4bXRpcFIwdFFrdyIsImF1ZCI6Imh0dHBzOi8vcmV2b2x1dC5jb20iLCJleHAiOjE2ODAxNTk4MTV9.EiqM4ZHgphf58Qfs4l7RW3On26hPv31BZArevZwVn5yoXn6Y05XjtogEFKAN21DwXEy89rzS-GBGmY9IF54pZhJ29RzpJmJ6oLZZU28QAVtBtDEwWowqoWVMsBhtrJ53QaFkRA0Kk_hl9JfA6l8wQVLShMYwdWE2jOBzIGScp0s");

            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(dict) };
            var res = await client.SendAsync(req);
            var json = await res.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Token>(json);
            var tokenString = result.access_token;

            //Console.WriteLine(json);

            return tokenString;
        }
    }
}