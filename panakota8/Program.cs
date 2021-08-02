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
using Telegram.Bot.Types.Enums;

using Telegram.Bot.Args;

namespace panakota8
{
    class Program
    {
        //static TelegramBotClient Bot; //создали объект
        static void Main(string[] args)
        {
            try
            {
                TelegramBotic hlp = new TelegramBotic(token: "1807528983:AAHpWxKKkDDYRTtNGD9rXBFKh8no3Hrtkw4");
                hlp.GetUppdates();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            //try
            //{
            //    Bot = new TelegramBotClient("1807528983:AAHpWxKKkDDYRTtNGD9rXBFKh8no3Hrtkw4"); //ввели код от БотФазер

            //    Bot.OnMessage += Bot_OnMessageReceived; //события, чтобы получить смс, которое вел пользователь бота
            //    Bot.OnCallbackQuery += Bot_OnCallbackQueryReceived;

            //    var me = Bot.GetMeAsync().Result;//инфа про бота(имя, ник и тд)

            //    Console.WriteLine(me.FirstName);

            //    Bot.StartReceiving(); //чтобы начать получать смски пользователей(в тг которые пишут)
            //    Console.ReadLine();
            //    Bot.StopReceiving();//чтобы остановить смски пользователей для обработки
            //}

            //catch (Exception ex) { Console.WriteLine(ex.Message); }

            processingDatasFormApi rec = new processingDatasFormApi();
            List<string> TheDecisionLikeList = rec.TheDecisionLikeList("Бекешев Д.Д.", Decision.Rejected);

            foreach(string l in TheDecisionLikeList)
            {
                Console.WriteLine(l);
                Console.WriteLine("\n\n\n");
                Console.WriteLine(new string('-', 80));
                Console.WriteLine("\n\n\n");
            }

            //foreach (string sett in TheDecisionLikeList("ОроЗова К.Б.", Decision.Agreed))
            //{
            //    Console.WriteLine(sett);
            //    Console.WriteLine("\n\n\n");
            //    Console.WriteLine(new string('-', 80));
            //    Console.WriteLine("\n\n\n");
            //}

            //Console.WriteLine(TheDecision("ОроЗова К.Б.", Decision.Agreed));


            Console.WriteLine(@"выав");

            //Console.WriteLine(TheDecisionLikeList.Count);
        }

        public static string TheDecision(string name, Decision decision)
        {
            ConectWithAPI re = new ConectWithAPI();
            string answer = "";
            DeputyActivity deputyActivity = re.getResponseOfDeputy(name);

            if (deputyActivity.Name.ToLower() == name.ToLower())
            {
                foreach (Vote item in deputyActivity.Votes)
                {
                    if (item.Decision == decision)
                        answer += item.Law.LawName + "\n";
                }
            }
            Console.WriteLine(answer);
            return answer;
        }

        public static List<string> TheDecisionLikeList(string name, Decision decision)
        {
            ConectWithAPI re = new ConectWithAPI();
            string answer = "";
            List<string> QuantaOfLaws = new List<string>();
            DeputyActivity deputyActivity = re.getResponseOfDeputy(name);

            if (deputyActivity.Name.ToLower() == name.ToLower())
            {
                int i = 1;
                foreach (Vote item in deputyActivity.Votes)
                {
                    if (item.Decision == decision && 30000 > answer.Length)
                    {
                        if (item.Law.LawName.Length < 30000 - answer.Length)
                        {
                            answer += $"{i++}) " + item.Law.LawName + "\n";
                        }
                        else
                        {
                            QuantaOfLaws.Add(answer);
                            answer = "";
                        }
                    }
                }
            }
            QuantaOfLaws.Add(answer);
            return QuantaOfLaws;
        }
    }
}
