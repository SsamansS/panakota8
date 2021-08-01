﻿using System;
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

            processingDatasFormApi processing = new processingDatasFormApi();
            foreach (string dep in processing.GetCombinations("Атто б"))
                Console.WriteLine(dep);
        }
    }
}
