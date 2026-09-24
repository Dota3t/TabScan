using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabScan
{
    internal class Date
    {
        public ObservableCollection<Record> Records { get; set; }
        public string date { get; set; }
        public Date()
        {
            Records = new();
            date = DateTime.Now.Date.ToString();
        }
        public void addToDate(Record newRecord)
        {
            Records.Add(newRecord);
        }
    }
}

