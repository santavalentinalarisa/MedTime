using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using MedTime.Models;

namespace MedTime.Data
{
    public class DataMedicament
    {
        readonly SQLiteAsyncConnection _database;
        public DataMedicament(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Medicament>().Wait();
        }
        public Task<List<Medicament>> GetMedicationsAsync()
        {
            return _database.Table<Medicament>().ToListAsync();
        }
        public Task<Medicament> GetMedicationAsync(int id)
        {
            return _database.Table<Medicament>()
            .Where(i => i.ID == id)
           .FirstOrDefaultAsync();
        }
        public Task<int> SaveMedicationAsync(Medicament medicament)
        {
            if (medicament.ID != 0)
            {
                return _database.UpdateAsync(medicament);
            }
            else
            {
                return _database.InsertAsync(medicament);
            }
        }
        public Task<int> DeleteMedicationAsync(Medicament medicament)
        {
            return _database.DeleteAsync(medicament);
        }
    }
}

