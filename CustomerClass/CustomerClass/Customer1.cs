using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer
{
    public enum TariffPlan
    {
        Hourly,
        After10MinutesCheaper,
        PayLessBefore5Minutes
    }

    class Customer
    {
        public string Name { get; set; }
        public double Balance { get; private set; }
        public TariffPlan SelectedTariff { get; set; }

        public Customer(string name, double balance = 100, TariffPlan selectedTariff = TariffPlan.Hourly)
        {
            Name = name;
            Balance = balance;
            SelectedTariff = selectedTariff;
        }

        public string ToString()
        {
            return string.Format("Клиент: {0} имеет баланс: {1}", Name, Balance);
        }

        public void RecordPayment(double amountPaid)
        {
            if (amountPaid > 0)
                Balance += amountPaid;
        }

        public void RecordCall(char callType, int minutes)
        {
            switch (SelectedTariff)
            {
                case TariffPlan.Hourly:
                    ApplyHourlyPricing(callType, minutes);
                    break;

                case TariffPlan.After10MinutesCheaper:
                    ApplyAfter10MinutesCheaperPricing(callType, minutes);
                    break;

                case TariffPlan.PayLessBefore5Minutes:
                    ApplyPayLessBefore5MinutesPricing(callType, minutes);
                    break;
                default:
                    throw new InvalidOperationException("Invalid tariff plan");
            }
        }

        private void ApplyHourlyPricing(char callType, int minutes)
        {
            if (callType == 'Г')
                Balance -= minutes * 5;
            else if (callType == 'М')
                Balance -= minutes * 1;
        }

        private void ApplyAfter10MinutesCheaperPricing(char callType, int minutes)
        {
            if (callType == 'Г')
            {
                if (minutes > 10)
                {
                    int billableMinutes = minutes - 5; // каждая вторая минута бесплатная после 10 минут разговора
                    Balance -= billableMinutes * 5;
                }
                else
                {
                    Balance -= minutes * 5;
                }
            }
            else if (callType == 'М')
            {
                Balance -= minutes * 1;
            }
        }

        private void ApplyPayLessBefore5MinutesPricing(char callType, int minutes)
        {
            if (callType == 'Г')
            {
                if (minutes <= 5)
                {
                    Balance -= minutes * 2.5; // в два раза дешевле после 5 минут
                }
                else
                {
                    Balance -= minutes * 5; // в два раза дороже после 5 минут
                }
            }
            else if (callType == 'М')
            {
                Balance -= minutes * 1;
            }
        }
    }

    class Customer1
    {
        static void Main(string[] args)
        {
            Customer Ivan = new Customer("Иван Петров", 500, TariffPlan.After10MinutesCheaper);
            Customer Elena = new Customer("Елена Иванова", selectedTariff: TariffPlan.PayLessBefore5Minutes);

            Ivan.RecordCall('Г', 12);
            Elena.RecordCall('М', 25);

            Console.WriteLine(Ivan);
            Console.WriteLine(Elena);
        }
    }
}
