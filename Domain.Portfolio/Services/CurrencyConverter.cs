using System;
using Shared;


namespace Domain.Portfolio.Services
{
    public class CurrencyConverter
    {
        public double ConvertCurrency(CurrencyType from, CurrencyType to, double amount)
        {
            if (from == to && to == CurrencyType.AustralianDollar)
            {
                return amount;
            }
            throw new
                NotSupportedException("Currency conversion for non-Australian dollars is not supported");
        }
    }
}