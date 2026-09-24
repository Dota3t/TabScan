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
            var result = await database.CreateTableAsync<Date>();

        }

        public async Task<List<Date>> SelectAll()
        {
            await Init();
            return await database.Table<Date>().ToListAsync();
        }

        public async Task<int> Insert(Date item)
        {
            await Init();
            if(item.ID != 0)
            {
                return await database.UpdateAsync(item);
            }
            return await database.InsertAsync(item);
        }

        public async Task<int> Delete(Date item)
        {
            await Init();
            return await database.DeleteAsync(item);
        }
    }
}
