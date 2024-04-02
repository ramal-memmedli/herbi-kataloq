using HerbiKataloq.DatabaseConnection;
using HerbiKataloq.Models;
using HerbiKataloq.Models.TankModels;
using HerbiKataloq.Models.TeyyareModels;
using HerbiKataloq.ViewModels.Teyyare;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.Common;

namespace HerbiKataloq.Services.TeyyareServices
{
    public class TeyyareService : ITeyyareService
    {
        private readonly IMongoCollection<Teyyare> _collection;

        public TeyyareService(HerbiKataloqDbConnection dbConnection)
        {
            IMongoDatabase db = dbConnection.GetDB();
            _collection = db.GetCollection<Teyyare>("Aviasiya");
        }

        public async Task<List<Teyyare>> Find(string key, string value)
        {
            FilterDefinition<Teyyare> filter = Builders<Teyyare>.Filter.Regex(key, value);

            List<Teyyare> teyyareler = await _collection.Find(filter).ToListAsync();

            return teyyareler;
        }

        public async Task<Teyyare> GetAsync(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            FilterDefinition<Teyyare> filter = Builders<Teyyare>.Filter.Eq("_id", objectId);
            Teyyare teyyare = await _collection.Find(filter).FirstOrDefaultAsync();

            return teyyare;
        }

        public async Task<List<Teyyare>> GetAllAsync()
        {
            List<Teyyare> teyyareler = await _collection.Find(_ => true).ToListAsync();

            return teyyareler;
        }

        public async Task<List<Teyyare>> GetAllByCategory(string category)
        {
            FilterDefinition<Teyyare> filter = Builders<Teyyare>.Filter.Eq("Tipi", category);

            List<Teyyare> teyyareler = await _collection.Find(filter).ToListAsync();

            return teyyareler;
        }

        public async Task<string> CreateAsync(Teyyare entity)
        {
            await _collection.InsertOneAsync(entity);

            return entity.Id;
        }

        public async Task UpdateAsync(string id, Teyyare entity)
        {
            ObjectId objectId = ObjectId.Parse(id);
            FilterDefinition<Teyyare> filter = Builders<Teyyare>.Filter.Eq("_id", objectId);

            UpdateDefinition<Teyyare> update = Builders<Teyyare>.Update
            .Set("Adi", entity.Adi)
            .Set("Tipi", entity.Tipi)
            .Set("IlkUcusTarixi", entity.IlkUcusTarixi)
            .Set("IstifadesiDavamEdirmi", entity.IstifadesiDavamEdirmi)
            .Set("Istehsalci", entity.Istehsalci)
            .Set("Olculeri", entity.Olculeri)
            .Set("Qabiliyyetleri", entity.Qabiliyyetleri)
            .Set("Qeyd", entity.Qeyd)
            .Set("Muherriki", entity.Muherriki)
            .Set("Foto", entity.Foto);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            FilterDefinition<Teyyare> filter = Builders<Teyyare>.Filter.Eq("_id", objectId);

            await _collection.DeleteOneAsync(filter);
        }
    }
}
