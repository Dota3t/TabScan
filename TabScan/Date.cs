using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TabScan
{
    public class Date
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public ObservableCollection<Record> Records { get; set; }
        public DateOnly date { get; set; }
        public Date()
        {
            Records = new();
            date = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }
        public Date(DateOnly date_, Record record)
        {
            Records = new([record]);
            date = date_;
        }
        public void addToDate(Record newRecord)
        {
            Records.Insert(0, newRecord);
        }
    }
}

