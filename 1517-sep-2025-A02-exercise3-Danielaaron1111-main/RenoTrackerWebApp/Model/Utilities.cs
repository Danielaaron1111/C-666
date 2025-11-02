using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenoSystem
{
    public static class Utilities
    {
        public static bool IsNonZeroPositive(int value)
        {
            bool flag = true;
            if (value <= 0)
            {
                flag = false;
            }
            return flag;
        }

        public static bool MeetsMinimumCriteria(int value, int criteria)
        {
            bool flag = true;
            if (value < criteria)
            {
                flag = false;
            }
            return flag;
        }
    }
}
