using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration;
using LC_CodingChallenge.Extensions;
using Microsoft.AspNetCore.Http;

namespace LC_CodingChallenge.Repository.Models
{
    public class Lease
    {
        public Lease()
        {
            Logs = new Dictionary<int, string>();
        }
        private decimal _paymentAmount;
        private string _name;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _numberOfPayments;
        private double _interestRate;
        private int _rowNumber;
        [DisplayName("#")]
        public int RowNumber { get => _rowNumber; set => _rowNumber = value; }
        [DisplayName("Name")]
        public string Name { get => _name; set => _name = value; }
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get => _startDate; set { _startDate = value; } }
        [DisplayName("End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get => _endDate; set => _endDate = value; }
        [DisplayName("Payment Amount")]
        public decimal PaymentAmount { get => _paymentAmount; set => _paymentAmount = value; }
        [DisplayName("Number of Payments")]
        public int NumberOfPayments { get => _numberOfPayments; set => _numberOfPayments = value; }
        [DisplayName("Interest Rate")]
        public double InterestRate { get => _interestRate; set => _interestRate = value; }
        [DisplayName("Interest Rate")]
        public double InterestRatePct { get => (_interestRate * 100).Truncate(); }
        public IFormFile file { set; get; }
        public Dictionary<int, string> Logs { get; set; }


    }
    public sealed class LeaseMap : ClassMap<Lease>
    {
        public LeaseMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.StartDate).Name("Start Date");
            Map(m => m.EndDate).Name("End Date");
            Map(m => m.PaymentAmount).Name("Payment Amount");
            Map(m => m.NumberOfPayments).Name("Number of Payments");
            Map(m => m.InterestRate).Name("Interest Rate");
        }
    }
}
