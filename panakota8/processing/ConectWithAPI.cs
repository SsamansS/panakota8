using System;
using System.Linq;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;

using System.IO;
using System.Net;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft;
using Newtonsoft.Json;

using panakota8;

namespace panakota8
{
    internal class ConectWithAPI
    {
        public string response1;
        public string response2;


        public DeputyActivity getResponseOfDeputy(string name)
        {
            HttpWebRequest requestDeputy = (HttpWebRequest)WebRequest.Create(
                "http://panakota.azurewebsites.net/api/v1/Deputy/name/%D0%A0%D1%8B%D1%81%D0%B1%D0%B0%D0%B5%D0%B2%20%D0%A0.%D0%A0.");// + name);
            HttpWebResponse responseDeputy = (HttpWebResponse)requestDeputy.GetResponse();
            using (StreamReader streamReader = new StreamReader(responseDeputy.GetResponseStream()))
            {
                response2 = streamReader.ReadToEnd();
            }

            DeputyActivity deputy = JsonConvert.DeserializeObject<DeputyActivity>(response2);
            return deputy;
        }

        public List<Deputy> getResponseOfDeputies()
        {
            HttpWebRequest requestDeputies = (HttpWebRequest)WebRequest.Create(
                "http://panakota.azurewebsites.net/api/v1/Deputy");
            HttpWebResponse responseDeputies = (HttpWebResponse)requestDeputies.GetResponse();
            using (StreamReader streamReader = new StreamReader(responseDeputies.GetResponseStream()))
            {
                response1 = streamReader.ReadToEnd();
            }
            List<Deputy> deputies = JsonConvert.DeserializeObject<List<Deputy>>(response1);
            return deputies;
        }
    }
}
