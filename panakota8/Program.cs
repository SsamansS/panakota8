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
            //try
            //{
            //    TelegramBotic hlp = new TelegramBotic(token: "сюда нужно вставить токен вашего телеграм бота");
            //    hlp.GetUppdates();
            //}

            //catch (Exception ex) { Console.WriteLine(ex.Message); }   
            //Console.WriteLine("Бек д.д.");
            //foreach (char letter in GetCapitalLetters("Бек д.д."))
            //    Console.Write(letter);

            //Console.WriteLine(new string('-', 50));

            //Console.WriteLine("Байбакпаев Э.Д.");
            //foreach (char letter in GetCapitalLetters("Байбакпаев Э.Д."))
            //{
            //    Console.Write(letter);
            //}
            //Console.WriteLine(GetCapitalLetters("Байбакпаев Э.Д.").Contains(GetCapitalLetters("Бек д.д.")[0]));
            //Console.WriteLine(GetCapitalLetters("Байбакпаев Э.Д.").Contains(GetCapitalLetters("Бек д.д.")[1]));
            //Console.WriteLine(GetCapitalLetters("Байбакпаев Э.Д.").Contains(GetCapitalLetters("Бек д.д.")[2]));

            //Console.WriteLine(new string('-', 50));
            //Console.WriteLine(GetCapitalLetters("Бек д.д.").Contains(GetCapitalLetters("Байбакпаев Э.Д.")[0]));
            //Console.WriteLine(GetCapitalLetters("Бек д.д.").Contains(GetCapitalLetters("Байбакпаев Э.Д.")[1]));
            //Console.WriteLine(GetCapitalLetters("Бек д.д.").Contains(GetCapitalLetters("Байбакпаев Э.Д.")[2]));
            //Console.WriteLine(new string('-', 50));

            //Console.WriteLine("Аттокуров Д.Б.");
            //foreach (char letter in GetCapitalLetters("Аттокуров Д.Б."))
            //    Console.Write(letter);

            //Console.WriteLine(new string('-', 50));

            //Console.WriteLine("Бакчиев Д.А.");
            //foreach (char letter in GetCapitalLetters("Бакчиев Д.А."))
            //    Console.Write(letter);

            //Console.WriteLine(new string('-', 50));

            //GetCombination();

            //Console.WriteLine(new string('-', 50));

            processingDatasFormApi processing = new processingDatasFormApi();
            foreach (string dep in processing.GetCombinations("Токтошев Э.Т."))
                Console.WriteLine(dep);

            //foreach (char dep in GetCapitalLetters("Бек д.д."))
            //    Console.WriteLine(dep);
        }
        static void  GetCombination()
        {
            ConectWithAPI re = new ConectWithAPI();
            List<Deputy> Deputies = re.getResponseOfDeputies();

            List<string> list = new List<string>();

            foreach (Deputy deputy in Deputies)
            {
                //int Curr = 0;
                List<char> CapitalLettersAPI = GetCapitalLetters(deputy.Name);
                foreach(char letter in CapitalLettersAPI)
                {
                    Console.Write(letter + ",");
                }
                Console.WriteLine("\n");
            }
        }
        public static List<char> GetCapitalLetters(string MBdep)
        {
            List<char> response = new List<char>();

            string[] ArrOfNam = MBdep.Split(new char[] { ' ', '.' });
            foreach (string word in ArrOfNam)
            {
                if (word != null && word != " " && word != "")
                    response.Add(word.ToLower()[0]);
            }

            return response;
        }
    }
}
