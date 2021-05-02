using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC_CodingChallenge.Models
{
    public class Class
    {
        private decimal _paymentAmount;
        private string _name;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _numberOfPayments;
        private double _interestRate;
        private int _rowNumber;

        public int RowNumber { get => _rowNumber; set => _rowNumber = value; }
        public string Name { get => _name; set => _name = value; }
        public DateTime StartDate { get => _startDate; set { _startDate = value; } }
        public DateTime EndDate { get => _endDate; set => _endDate = value; }
        public decimal PaymentAmount { get => _paymentAmount; set => _paymentAmount = value; }
        public int NumberOfPayments { get => _numberOfPayments; set => _numberOfPayments = value; }
        public double InterestRate { get => _interestRate; set => _interestRate = value; }
        public double InterestRatePct { get => (_interestRate * 100); }
    }
}
