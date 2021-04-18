using System;
using Telegram.Bot;

namespace panakota8
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TelegramBotic hlp = new TelegramBotic(token: "1780295283:AAGphcKLN8_T5wS-mnGgirwtVoiHbqyE-LA");
                hlp.GetUppdates();
            }

            catch(Exception ex) { Console.WriteLine(ex.Message); }
            

            
        }
    }
}
