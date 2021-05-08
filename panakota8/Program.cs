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
                TelegramBotic hlp = new TelegramBotic(token: "1702708937:AAG1UTZYtgQLS43EFkrOY-kcC547WFzzjXg");
                hlp.GetUppdates();
            }

            catch (Exception ex) { Console.WriteLine(ex.Message); }   
        }    
    }
}
