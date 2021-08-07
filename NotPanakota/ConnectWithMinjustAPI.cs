using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NotPanakota
{
    class ConnectWithMinjustAPI
    {
        public string response;
        public string GetDocument(int code)
        {
            HttpWebRequest requestDeputies = (HttpWebRequest)WebRequest.Create(
                @"http://cbd.minjust.gov.kg/OpenData/GetDocument?Code=" + $"{code}&Property=Name&Property=Status&Property=Editions&Editions.Select=all&Editions.Data=last&Editions.Images.Select=all&Editions.Images.Data=none&Replace=anchors,images");
            HttpWebResponse responseDeputies = (HttpWebResponse)requestDeputies.GetResponse();
            using (StreamReader streamReader = new StreamReader(responseDeputies.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            var deputies = JsonConvert.DeserializeObject<FileStream>(response);
            return response;
        }
        public string CheckAvailable()
        {
            HttpWebRequest requestDeputies = (HttpWebRequest)WebRequest.Create(
                @"http://cbd.minjust.gov.kg/OpenData/CheckAvailable");
            HttpWebResponse responseDeputies = (HttpWebResponse)requestDeputies.GetResponse();
            using (StreamReader streamReader = new StreamReader(responseDeputies.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            var deputies = JsonConvert.DeserializeObject<FileStream>(response);
            return response;
        }
    }
}
