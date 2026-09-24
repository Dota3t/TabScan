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

        }

        public async Task<List<Record>> SelectAllRecords()
        {
            await Init();
            return await database.Table<Record>().ToListAsync();
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
    }
}
