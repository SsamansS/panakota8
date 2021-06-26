using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace panakota8.processing
{
    class processingDatasFormApi
    {
        ConectWithAPI re = new ConectWithAPI();
        public List<string> TheDecision(string name, Decision decision)
        {
            List<string> answer = new List<string>();
            DeputyActivity deputyActivity = re.getResponseOfDeputy(name);

            if (deputyActivity.Name.ToLower() == name.ToLower())
            {
                foreach (Vote item in deputyActivity.Votes)
                {
                    if (item.Decision == decision)
                    {
                        answer.Add(item.Law.LawName.Replace("О проекте постановления Жогорку Кенеша ", ""));
                    }

                    //answer += item.Law.LawName + "\n";
                }
            }
            foreach (var bill in answer)
                Console.WriteLine(bill);
            return answer;
        }

        public string CheckDeputy(string text)
        {
            ConectWithAPI depities = new ConectWithAPI();

            foreach (Deputy item in depities.getResponseOfDeputies())
            {
                if (item.Name.ToLower() == text.ToLower())
                {
                    return item.Name;
                } 
            }
            return "f";
        }




        public List<InlineKeyboardButton> GetListOfInlineButtons(string detuty, Decision status)
        {
            List<string> LawsForTheDecision = TheDecision(detuty, status).GetRange(0, 4);

            List<InlineKeyboardButton> ButtonsForLaws = new List<InlineKeyboardButton>();
            for (int i = 0; i < LawsForTheDecision.Count; i++)
            {
                ButtonsForLaws.Add(InlineKeyboardButton.WithCallbackData($"{LawsForTheDecision[i]}", $"return {i}"));
            }
            return ButtonsForLaws;
        }
        public InlineKeyboardButton[][] Gets(List<InlineKeyboardButton> setButtons)
        {
            int len = setButtons.Count;
            InlineKeyboardButton[][] inlineKeyboards = new InlineKeyboardButton[len][];

            int i = 0;
            while (i < len)
            {
                InlineKeyboardButton[] HelperList = setButtons.GetRange(setButtons.Count - 1, 1).ToArray();
                setButtons.RemoveRange(setButtons.Count - 1, 1);

                inlineKeyboards[i] = HelperList;
                i++;
            }

            return inlineKeyboards;
        }


    }
}
