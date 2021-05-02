using LC_CodingChallenge.Extensions;
using LC_CodingChallenge.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LC_CodingChallenge.Helper
{
    public static class LeaseModelToDataset
    {
        public static DataTable DesignDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("PaymentAmount", typeof(decimal));
            dt.Columns.Add("NumberOfPayment", typeof(int));
            dt.Columns.Add("InterestRate", typeof(double));
            return dt;
        }
        public static DataTable Convert(List<Lease> leases, Dictionary<int, string> errorLogs)
        {
            DataTable dt = DesignDataTable();
            foreach (var lease in leases)
            {
                var row = dt.NewRow();
                row["Name"] = lease.Name.ToString();
                row["StartDate"] = lease.StartDate.ToString("MM/dd/yyyy");
                row["EndDate"] = lease.EndDate.ToString("MM/dd/yyyy");

                #region PAYMENT AMOUNT
                if (lease.PaymentAmount < -1000000000 || lease.PaymentAmount > 1000000000)
                {
                    errorLogs.Add(-1, string.Format("{0} - payment amount should be greater than -1,000,000,000 and less than 1,000,000,000", lease.Name));
                    continue;
                }
                row["PaymentAmount"] = lease.PaymentAmount.Truncate();
                #endregion

                #region NUMBER OF PAYMENT
                if (lease.NumberOfPayments <= 0)
                {
                    errorLogs.Add(-1, string.Format("{0} - number of payment must be greater than 0", lease.Name));
                    continue;
                }
                int dateMonthDiff = ((lease.EndDate.Year - lease.StartDate.Year) * 12) + (lease.EndDate.Month - lease.StartDate.Month) + 1;
                if (lease.NumberOfPayments > dateMonthDiff)
                {
                    errorLogs.Add(-1, string.Format("{0} - number of payment must be less than or equal to the number of months between Start Date and End Date", lease.Name));
                    continue;
                }
                row["NumberOfPayment"] = lease.NumberOfPayments;
                #endregion

                #region INTEREST RATE
                if (lease.InterestRate <= 0)
                {
                    errorLogs.Add(-1, string.Format("{0} - interest rate must be greater than 0", lease.Name));
                    continue;
                }
                if (lease.InterestRate > 9.9999)
                {
                    errorLogs.Add(-1, string.Format("{0} - interest rate must be less than or equal to 9.9999", lease.Name));
                    continue;
                }
                row["InterestRate"] = lease.InterestRate;
                #endregion
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
