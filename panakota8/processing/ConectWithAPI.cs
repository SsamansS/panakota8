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
        public string response;

        public DeputyActivity getResponseOfDeputy(string name)
        {
            HttpWebRequest requestDeputy = (HttpWebRequest)WebRequest.Create(
                "http://localhost:53623/api/v1/Deputy/name/" + name);
            HttpWebResponse responseDeputy = (HttpWebResponse)requestDeputy.GetResponse();
            using (StreamReader streamReader = new StreamReader(responseDeputy.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            DeputyActivity deputy = JsonConvert.DeserializeObject<DeputyActivity>(response);
            return deputy;
        }

        public List<Deputy> getResponseOfDeputies()
        {
            HttpWebRequest requestDeputies = (HttpWebRequest)WebRequest.Create(
                "http://localhost:53623/api/v1/Deputy");
            HttpWebResponse responseDeputies = (HttpWebResponse)requestDeputies.GetResponse();
            using (StreamReader streamReader = new StreamReader(responseDeputies.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            List<Deputy> deputies = JsonConvert.DeserializeObject<List<Deputy>>(response);
            return deputies;
        }
    }
}
