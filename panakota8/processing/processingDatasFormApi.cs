using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace panakota8.processing
{
    class processingDatasFormApi
    {
        static ConectWithAPI re = new ConectWithAPI();
        List<Deputy> Deputies = re.getResponseOfDeputies();

        public string TheDecision(string name, Decision decision)
        {
            string answer = "";
            DeputyActivity deputyActivity = re.getResponseOfDeputy(name);

            if (deputyActivity.Name.ToLower() == name.ToLower())
            {
                foreach (Vote item in deputyActivity.Votes)
                {
                    if(item.Decision == decision)
                        answer += item.Law.LawName + "\n";
                }
            }
            Console.WriteLine(answer);
            return answer;
        }

        public string CheckDeputy(string text)
        {
            foreach (Deputy item in Deputies)
            {
                if (item.Name.ToLower() == text.ToLower())
                {
                    return item.Name;
                } 
            }
            return "f";
        }






        public List<string> GetCombinations(string MBdeputy)
        {
            List<string> deputies = new List<string>();

            List<char> CapitalLetters = GetCapitalLetters(MBdeputy);
            deputies.AddRange(GetCombination(CapitalLetters));

            return deputies;
        }
        List<string> GetCombination(List<char> CapitalLetters)
        {
            List<string> listEvery = new List<string>();
            List<string> listEveryBackwards = new List<string>();

            //Deputies.Where(x => x.Name.ToLower().Contains("dsf".ToLower())).Count();  --- 

            foreach (Deputy deputy in Deputies)
            {
                string Every = Switcher(true, deputy, CapitalLetters);
                if(Every != "")
                    listEvery.Add(Every);

                string EveryBackwards = Switcher(false, deputy, CapitalLetters);
                if(EveryBackwards != "")
                    listEveryBackwards.Add(EveryBackwards);

            }
            if(listEvery.Count < listEveryBackwards.Count) { return listEvery; }
            return listEveryBackwards;
        }

        private string Switcher(bool switcher, Deputy deputy, List<char> capitalLetters)
        {
            List<char> CapitalLettersAPI = GetCapitalLetters(deputy.Name);

            string list = "";

            if(switcher == true)
            {
                int Curr = 0;
                for (int i = 0; i < capitalLetters.Count; i++)
                {
                    if (CapitalLettersAPI.Contains(capitalLetters[i]))
                        ++Curr;
                }
                if (Curr == capitalLetters.Count)
                    list = deputy.Name;
            }
            else
            {
                int Curr = 0;
                for (int i = 0; i < CapitalLettersAPI.Count; i++)
                {
                    if (capitalLetters.Contains(CapitalLettersAPI[i]))
                        ++Curr;
                }
                if (Curr == capitalLetters.Count)
                    list = deputy.Name;
            }
            return list;
        }

        List<char> GetCapitalLetters(string MBdep)
        {
            List<char> response = new List<char>(3);

            string[] ArrOfNam = MBdep.Split(new char[] { ' ', '.' });
            foreach(string word in ArrOfNam)
            {
                if (word != null && word != " " && word != "")
                    response.Add(word.ToLower()[0]);
            }

            return response;
        }
    }
}
