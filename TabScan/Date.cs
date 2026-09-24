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
        public DateTime date { get; set; }
        public Date()
        {
            ID = globalId++;
            Records = new();
            date = DateTime.Now;
        }
        public void addToDate(Record newRecord)
        {
            Records.Add(newRecord);
        }
    }
}

