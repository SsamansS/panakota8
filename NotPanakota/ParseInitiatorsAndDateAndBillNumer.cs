using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotPanakota
{
    class ParseInitiatorsAndDateAndBillNumer
    {
        List<string> month = new List<string>
            {
                "января","февраля","марта",
                "апреля", "мая", "июня",
                "июля", "августа", "сентября",
                "октября", "ноября","декабря"
            };


        public string GetAbbreviatedFormBill(string law)
        {
            int index1 = law.IndexOf('«');
            int index2 = law.IndexOf('»') + 1;
            return law.Substring(index1, index2 - index1);
        }
        public (string, DateTime) GetBillNumber(string law)
        {
            try
            {
                string str = law.Substring(law.IndexOf('№') + 2);

                int index1 = 0;
                int index2 = str.IndexOf(')');
                string strin = str.Substring(index1, index2);
                string[] NumAndDate = strin.Split(new string[] { " от " }, StringSplitOptions.RemoveEmptyEntries);

                return (NumAndDate[0], DateTime.Parse(NumAndDate[1]));
            }
            catch
            {
                return ("нет номера", new DateTime());
            }
        }
        public List<string> GetInitiators(string law)
        {
            List<string> ListOfInitiators = new List<string>();

            try
            {
                string StringAfter = law.Substring(law.IndexOf("(инициатор"));

                int index1 = StringAfter.IndexOf('–') + 2;
                int index2 = StringAfter.IndexOf(')');

                string InitiatorsString = StringAfter.Substring(index1, index2 - index1);

                if (!InitiatorsString.Contains(','))
                {
                    ListOfInitiators.Add(InitiatorsString);  //главное
                }
                else
                {
                    if (InitiatorsString.Contains("депутат"))
                    {
                        int Index1 = InitiatorsString.IndexOf(' ') + 1;
                        string Deps = InitiatorsString.Substring(Index1);

                        string[] Deputies = Deps.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string Deputy in Deputies)
                        {
                            ListOfInitiators.Add(Deputy);
                        }
                    }
                    else
                    {
                        string[] Deputies = InitiatorsString.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        ListOfInitiators.Add(Deputies[0]);
                    }
                }
            }
            catch
            {
                ListOfInitiators.Add("В названии не указаны инициаторы");
            }
            return ListOfInitiators;
        }



        public string GetDate(string law)
        {
            List<string> words = law.Split(new char[] { ' ', ',', '!', ')', '(', '?' }).ToList();
            for (int i = 0; i < words.Count; i++)
            {
                try
                {
                    DateTime answer = DateTime.Parse(words[i]);
                    return answer.ToString("dd.MM.yyyy");
                }
                catch
                {
                    if (i == words.Count - 1 || i == words.Count - 2 || i == words.Count - 3)
                    {
                        break;
                    }
                    else if (Helper.CheckOnNum(words[i]) && month.Contains(words[i + 1]) && Helper.CheckOnNum(words[i + 2]) && words[i + 3] == "года")
                    {
                        int numMonth = month.IndexOf(words[i + 1]);
                        return $"{words[i]}.{numMonth}.{words[i + 2]}";
                    }
                }
            }
            return "нету";
        }
    }
}
