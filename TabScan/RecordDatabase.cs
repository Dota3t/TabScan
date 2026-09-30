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
                //await InsertStudent(new Student("Filip", "Tarza", "5TP", 30));
                return;
            }

            database = new SQLiteAsyncConnection(databasePath, SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create);
            var result = await database.CreateTableAsync<Record>();
            result = await database.CreateTableAsync<Student>();
            //await InsertStudent(new Student("Filip", "Tarza", "5TP", 30));
        }

        public async Task<List<Record>> SelectAllRecords(Func<Record, bool>? where=null)
        {
            await Init();
            List<Record> recs = await database.Table<Record>().ToListAsync();
            if (recs.Count == 0) return [];

            if (where is null) return recs;
            return new List<Record>(recs.Where(where));
        }

        public async Task<List<Record>> SelectAllRecordsFilled(Func<Record, bool>? where=null)
        {
            await Init();
            List<Record> recs = await database.Table<Record>().ToListAsync();
            List<Student> studs = await database.Table<Student>().ToListAsync();
            if (recs.Count == 0 || studs.Count == 0) return [];
            for(int i = 0; i < recs.Count; ++i)
            {
                recs[i].StudentData = studs[recs[i].StudentID];
            }

            if (where is null) return recs;
            return new List<Record>(recs.Where(where));
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

        public async Task<List<Student>> SelectAllStudents(Func<Student, bool>? where=null)
        {
            await Init();
            List<Student> studs = await database.Table<Student>().ToListAsync();
            if (studs.Count == 0) return [];
            if (where is null) return studs;
            return new List<Student>(studs.Where(where));
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

        public async Task<List<Date>> SelectAllDates(Func<Record, bool>? where=null)
        {
            await Init();
            List<Record> recs = await SelectAllRecordsFilled();
            if (recs.Count == 0) return [];
            List<Date> dates = [];
            List<DateOnly> usedDates = [];
            int i = 0;
            
            foreach(Record rec in recs)
            {
                if (where is null || where(rec))
                {
                    DateOnly date = new DateOnly(rec.StartTime.Year, rec.StartTime.Month, rec.StartTime.Day);
                    int index = usedDates.IndexOf(date);
                    if (index != -1)
                    {
                        dates[index].addToDate(rec);
                    }
                    else
                    {
                        dates.Insert(0, new Date(date, rec));
                        usedDates.Insert(0, date);
                    }
                }

                ++i;
            }

            return dates;
        }
    }
}
