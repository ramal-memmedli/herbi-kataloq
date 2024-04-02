using HerbiKataloq.DatabaseConnection;
using HerbiKataloq.Models;
using HerbiKataloq.Models.TankModels;
using HerbiKataloq.Models.TeyyareModels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HerbiKataloq.Services.TankServices
{
    public class TankService : ITankService
    {
        private readonly IMongoCollection<Tank> _collection;

        public TankService(HerbiKataloqDbConnection dbConnection)
        {
            IMongoDatabase db = dbConnection.GetDB();
            _collection = db.GetCollection<Tank>("ZirehliTexnikalar");
        }

        public async Task<List<Tank>> Find(string key, string value)
        {
            FilterDefinition<Tank> filter = Builders<Tank>.Filter.Regex(key, value.Trim());

            List<Tank> tanklar = await _collection.Find(filter).ToListAsync();

            return tanklar;
        }

        public async Task<Tank> GetAsync(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            FilterDefinition<Tank> filter = Builders<Tank>.Filter.Eq("_id", objectId);
            Tank tank = await _collection.Find(filter).FirstOrDefaultAsync();

            return tank;
        }

        public async Task<List<Tank>> GetAllAsync()
        {
            List<Tank> tanklar = await _collection.Find(_ => true).ToListAsync();

            return tanklar;
        }

        public async Task<List<Tank>> GetAllByCategory(string category)
        {
            FilterDefinition<Tank> filter = Builders<Tank>.Filter.Eq("Sinfi", category);

            List<Tank> tanklar = await _collection.Find(filter).ToListAsync();

            return tanklar;
        }

        public async Task<string> CreateAsync(Tank entity)
        {
            await _collection.InsertOneAsync(entity);

            return entity.Id;
        }

        public async Task UpdateAsync(string id, Tank entity)
        {
            ObjectId objectId = ObjectId.Parse(id);
            FilterDefinition<Tank> filter = Builders<Tank>.Filter.Eq("_id", objectId);

            UpdateDefinition<Tank> update = Builders<Tank>.Update
            .Set("Adi", entity.Adi)
            .Set("Sinfi", entity.Sinfi)
            .Set("Zirehi", entity.Zirehi)
            .Set("YanacaqTutumu", entity.YanacaqTutumu)
            .Set("PersonalSayi", entity.PersonalSayi)
            .Set("Istehsalci", entity.Istehsalci)
            .Set("Olculeri", entity.Olculeri)
            .Set("Qabiliyyetleri", entity.Qabiliyyetleri)
            .Set("Muherriki", entity.Muherriki)
            .Set("Foto", entity.Foto)
            .Set("Qeyd", entity.Qeyd);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            FilterDefinition<Tank> filter = Builders<Tank>.Filter.Eq("_id", objectId);

            await _collection.DeleteOneAsync(filter);
        }

        public async Task AddWeapon(string id, TankSilahi uEntity)
        {
            ObjectId objectId = ObjectId.Parse(id);
            FilterDefinition<Tank> filter = Builders<Tank>.Filter.Eq("_id", objectId);

            UpdateDefinition<Tank> update = Builders<Tank>.Update.Push<TankSilahi>("Silahlari", uEntity);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task RemoveWeapon(string id, TankSilahi uEntity)
        {
            ObjectId objectId = ObjectId.Parse(id);
            FilterDefinition<Tank> filter = Builders<Tank>.Filter.Eq("_id", objectId);

            UpdateDefinition<Tank> update = Builders<Tank>.Update.Pull("Silahlari", uEntity);

            await _collection.UpdateOneAsync(filter, update);
        }
    }
}