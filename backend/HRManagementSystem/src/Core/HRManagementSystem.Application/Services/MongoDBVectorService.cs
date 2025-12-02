using HRManagementSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HRManagementSystem.Application.Services;

public class MongoDBVectorService : IMongoDBVectorService
{
    private readonly IMongoCollection<BsonDocument> _collection;

    public MongoDBVectorService(IConfiguration configuration)
    {
        string connectionString = configuration["MongoDBSettings:ConnectionString"] ?? throw new ArgumentNullException("MongoDBSettings:ConnectionString");
        string databaseName = configuration["MongoDBSettings:DatabaseName"] ?? throw new ArgumentNullException("MongoDBSettings:DatabaseName");
        string collectionName = configuration["MongoDBSettings:CollectionName"] ?? throw new ArgumentNullException("MongoDBSettings:CollectionName");

        var client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<BsonDocument>(collectionName);
    }

    public async Task InsertDocumentAsync(BsonDocument document, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(document, cancellationToken: cancellationToken);
    }

    public async Task<List<BsonDocument>> GetAllDocumentsAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(new BsonDocument()).ToListAsync(cancellationToken);
    }

    public async Task DeleteAllDocumentsAsync(CancellationToken cancellationToken = default)
    {
        await _collection.DeleteManyAsync(new BsonDocument(), cancellationToken);
    }
}
