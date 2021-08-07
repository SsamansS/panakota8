using System;

namespace NotPanakota
{
    static class Helper
    {
        public static bool CheckOnNum(string obj)
        {
            try
            {
                Convert.ToInt32(obj);
                return true;
            }
            catch { return false; }
        }
    }
}