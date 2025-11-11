using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopSystem
{
    public static class Utilities
    {
        public static bool IsNonZeroPositive(int value)
        {
            return value > 0;
        }

        public static bool IsValidPrice(double price)
        {
            return price >= 0.01 && price <= 100.00;
        }
    }
}