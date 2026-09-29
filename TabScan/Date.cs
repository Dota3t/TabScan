using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabScan
{
    public class Date
    {
        public static int globalId = 0;
        public int ID { get; set; }
        public ObservableCollection<Record> Records { get; set; }
        public DateOnly date { get; set; }
        public Date()
        {
            ID = globalId++;
            Records = new();
            date = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }
        public Date(DateOnly date_, Record record)
        {
            ID = globalId++;
            Records = new([record]);
            date = date_;
        }
        public void addToDate(Record newRecord)
        {
            Records.Add(newRecord);
        }
    }
}

