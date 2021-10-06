using AppFit.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppFit.Helpers
{
    class SQLiteDatabaseHelper
    {
        private readonly SQLiteAsyncConnection _connection;

        public SQLiteDatabaseHelper(string databasePath)
        {
            _connection = new SQLiteAsyncConnection(databasePath);

            _connection.CreateTableAsync<Atividade>().Wait();
        }

        public Task<List<Atividade>> GetAllRows()
        {
            return _connection.Table<Atividade>()
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }

        public Task<Atividade> GetById(int id)
        {
            return _connection.Table<Atividade>()
                .Where(item => item.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> Insert(Atividade model)
        {
            return _connection.InsertAsync(model);
        }

        public Task<int> Update(Atividade model)
        {
            return _connection.UpdateAsync(model);
        }

        public Task<int> Delete(Atividade model)
        {
            return _connection.Table<Atividade>()
                .DeleteAsync(item => item.Id == model.Id);
        }

        public Task<List<Atividade>> Search(string query)
        {
            return _connection.Table<Atividade>()
                .Where(item => item.Descricao.Contains(query))
                .ToListAsync();
        }
    }
}
