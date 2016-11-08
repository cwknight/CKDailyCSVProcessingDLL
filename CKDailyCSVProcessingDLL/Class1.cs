using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;

namespace CKDailyCSVProcessingDLL
{
    class CKDailyRecord
    {
        public string Type { get; set; }
        public decimal Total { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Credit { get; set; }
        public decimal Visa { get; set; }
        public decimal Mastercard { get; set; }
        public decimal Discover { get; set; }
        public decimal Amex { get; set; }
        public decimal PayPal { get; set; }
        public decimal MOCC { get; set; }
        public decimal Storecredit { get; set; }
        public decimal COGS { get; set; }
    }
    class OutputRecord
    {
        public DateTime Date { get; set; }
        public Decimal Subtotal { get; set; }
        public Decimal Shipping { get; set; }
    }
    public class ShippingManagementDataCSV
    {

        public ShippingManagementDataCSV(string filepath)
        {
            CKDailyRecord workingRecord;
            List<OutputRecord> OutputList = new List<OutputRecord>();
            string[] files = Directory.GetFiles(filepath);
            foreach (string file in files)
            {
                DateTime fileDate;
                string filedate = file.Substring(file.Length - 14);
                string year = filedate.Substring(0, 4);
                string month = filedate.Substring(5, 2);
                string day = filedate.Substring(8, 2);
                fileDate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                CsvReader reader;
                OutputRecord outrec = new OutputRecord();
                using (StreamReader sreader = new StreamReader(file))
                {
                    reader = new CsvReader(sreader);
                    reader.Read();
                    workingRecord = reader.GetRecord<CKDailyRecord>();
                    outrec.Subtotal = workingRecord.Subtotal;
                    outrec.Shipping = workingRecord.Shipping;
                    outrec.Date = fileDate;
                    OutputList.Add(outrec);
                }

            }
            using (StreamWriter writer = new StreamWriter(filepath + @"\CKDailyCSVOutput.csv"))
            {
                CsvWriter csv = new CsvWriter(writer);
                csv.WriteRecords(OutputList);
            }

        }
    }
}