using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartMauiTest
{
    public class PayCalculator
    {
        // 15% superannuation rate
        private const decimal SuperRate = 0.15m;

        // Calculate the superannuation amount based on the gross payment
        public static decimal CalculateSuperannuation(decimal grossPayment)
        {
            return grossPayment * SuperRate;
        }

        // Calculate the gross pay based on the hourly rate and hours worked
        public static decimal CalculateGross(decimal hourlyRate, decimal hoursWorked)
        {
            return hourlyRate * hoursWorked;
        }

        // Calculate the net pay based on the gross pay and tax amount
        public static decimal CalculateNet(decimal grossPayment, decimal taxAmount)
        {
            return grossPayment - taxAmount;
        }
    }
}