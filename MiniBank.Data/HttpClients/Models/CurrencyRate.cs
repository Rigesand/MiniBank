using System;
using System.Collections.Generic;

namespace MiniBank.Data.HttpClients.Models
{
    public class CurrencyRate
    {
        public DateTime Date { get; set; }
        public Dictionary<string, ValueItem> Valute { get; set; }
    }

    public class ValueItem
    {
        public double Value { get; set; }
    }
}