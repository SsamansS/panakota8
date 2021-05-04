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

//using panakota8.Data.Models;
using panakota8;
//using panakota8.processing;

namespace panakota8
{
    internal class ConectWithAPI
    {

        

        public string response;



        /*public ConectWithAPI(string name)
        {
            requestDeputies = (HttpWebRequest)WebRequest.Create(
                "http://localhost:53623/api/v1/Deputy/name/" + name);
        }*/




        public List<Vote> getResponseOfDeputy(string name)
        {

            HttpWebRequest requestDeputy = (HttpWebRequest)WebRequest.Create(
                "http://localhost:53623/api/v1/Deputy/name/" + name);

            HttpWebResponse responseDeputy = (HttpWebResponse)requestDeputy.GetResponse();


            using (StreamReader streamReader = new StreamReader(responseDeputy.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            DeputyActivity deputy = JsonConvert.DeserializeObject<DeputyActivity>(response);

            return deputy.Votes;
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


        public List<Law> getResponseOfLaws()
        {
            HttpWebRequest requestLaw = (HttpWebRequest)WebRequest.Create(
                    "http://localhost:53623/api/v1/Law");

            HttpWebResponse responseLaw = (HttpWebResponse)requestLaw.GetResponse();

            using (StreamReader streamReader = new StreamReader(responseLaw.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            List<Law> lawList = JsonConvert.DeserializeObject<List<Law>>(response);

            return lawList;
        }


        public List<Vote> getResponseOfVotes()
        {
            HttpWebRequest requestVote = (HttpWebRequest)WebRequest.Create(
                    "http://localhost:53623/api/v1/Vote");

            HttpWebResponse responseVote = (HttpWebResponse)requestVote.GetResponse();

            using (StreamReader streamReader = new StreamReader(responseVote.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            List<Vote> voteList = JsonConvert.DeserializeObject<List<Vote>>(response);

            return voteList;
        }

    }
}
