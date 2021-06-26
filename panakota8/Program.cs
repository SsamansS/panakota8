using System;
using Telegram.Bot;

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

using panakota8.processing;


namespace panakota8
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TelegramBotic hlp = new TelegramBotic(token: "1734499302:AAFSMxohJfcQeXQTf6cNh0JS06g6lae3Y0I");
                hlp.GetUppdates();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            //try
            //{
            //    TriplTelegramBot gram = new TriplTelegramBot(token: "1734499302:AAFSMxohJfcQeXQTf6cNh0JS06g6lae3Y0I");
            //    gram.TriplGetUppdates();
            //}
            //catch (Exception ex) { Console.WriteLine(ex.Message); }


        }
        public static IEnumerable<int[]> GetVs()
        {
            List<int[]> vs = new List<int[]>();

            List<int> intt = new List<int>();
            for (int i = 0; i < 10; i++)
                intt.Add(i);
            while (intt.Count >= 3)
            {
                int[] int2 = intt.GetRange(intt.Count - 3, 3).ToArray();
                intt.RemoveRange(intt.Count - 3, 3);

                vs.Add(int2);
            }
            vs.Add(intt.ToArray());

            return vs;
        }
    }
}
