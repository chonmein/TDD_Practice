using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxTest
{
    public class Tax
    {
        private readonly TaxRepository _tax;

        public Tax(TaxRepository tax)
        {
            this._tax = tax;
        }

        public double GetBasicTax(CarType carType, CCType ccType, double day)
        {
            var taxs = this._tax.GetTaxTable();
            if (day <= 0) return 0;
            if (!taxs.Keys.Contains(ccType)) return 0;
            if ((int) carType >= taxs.Values.First().Length || (int) carType < 0) return 0;
            return day <= 365 ? taxs[ccType][(int)carType] * day / 360.0 : taxs[ccType][(int)carType];
        }
    }
}