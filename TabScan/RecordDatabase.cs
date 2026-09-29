using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabScan
{
    public class RecordDatabase
    {
        SQLiteAsyncConnection database;
        static string databasePath = Path.Combine(FileSystem.AppDataDirectory, "TabScanRecords.db3");
        async Task Init()
        {
            if(database is not null)
            { 
                return;
            }

            database = new SQLiteAsyncConnection(databasePath, SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create);
            var result = await database.CreateTableAsync<Record>();
            result = await database.CreateTableAsync<Student>();

            await InsertStudent(new Student("Filip", "Tarza", "5TP", 30));
            await InsertRecord(new Record(0, "340i9", new DateTime(2026, 9, 29, 9, 0, 0), new DateTime(2026, 9, 29, 9, 45, 0)));
        }

        public async Task<List<Record>> SelectAllRecords()
        {
            await Init();
            return await database.Table<Record>().ToListAsync();
        }

        public async Task<List<Record>> SelectAllRecordsFilled()
        {
            await Init();
            List<Record> recs = database.Table<Record>().ToListAsync().Result;
            List<Student> studs = database.Table<Student>().ToListAsync().Result;
            for(int i = 0; i < recs.Count; ++i)
            {
                recs[i].StudentData = studs[recs[i].StudentID];
            }

            return recs;
        }

        public async Task<int> InsertRecord(Record item)
        {
            await Init();
            if(item.Id != 0)
            {
                return await database.UpdateAsync(item);
            }
            return await database.InsertAsync(item);
        }

        public async Task<int> DeleteRecord(Record item)
        {
            await Init();
            return await database.DeleteAsync(item);
        }

        public async Task<List<Student>> SelectAllStudents()
        {
            await Init();
            return await database.Table<Student>().ToListAsync();
        }
        public async Task<int> InsertStudent(Student item)
        {
            await Init();
            if (item.Id != 0)
            {
                return await database.UpdateAsync(item);
            }
            return await database.InsertAsync(item);
        }

        public async Task<int> DeleteStudent(Student item)
        {
            await Init();
            return await database.DeleteAsync(item);
        }

        public async Task<List<Date>> SelectAllDates()
        {
            await Init();
            List<Record> recs = SelectAllRecordsFilled().Result;
            List<Date> dates = [];
            List<DateOnly> usedDates = [];
            foreach(Record rec in recs)
            {
                DateOnly date = new DateOnly(rec.StartTime.Year, rec.StartTime.Month, rec.StartTime.Day);
                int index = usedDates.IndexOf(date);
                if(index != -1){
                    dates[index].addToDate(rec);    
                } else
                {
                    dates.Add(new Date(date, rec));
                }
            }

            return dates;
        }
    }
}
