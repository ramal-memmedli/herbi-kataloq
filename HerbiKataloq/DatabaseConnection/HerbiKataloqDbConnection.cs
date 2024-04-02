using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HerbiKataloq.DatabaseConnection
{
    public class HerbiKataloqDbConnection(IOptions<MongoDbServerTenzimlemeleri> serverTenzimlemeleri)
    {
        public IMongoDatabase GetDB()
        {
            MongoClient mongoClient = new MongoClient(serverTenzimlemeleri.Value.ServerAdresi);

            return mongoClient.GetDatabase(serverTenzimlemeleri.Value.DatabaseAdi);
        }
    }

    public class MongoDbServerTenzimlemeleri
    {
        public string ServerAdresi { get; set; } = null!;
        public string DatabaseAdi { get; set; } = null!;
    }
}
