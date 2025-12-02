using MongoDB.Bson;

namespace HRManagementSystem.Application.Interfaces;

public interface IMongoDBVectorService
{
    Task InsertDocumentAsync(BsonDocument document, CancellationToken cancellationToken = default);
    Task<List<BsonDocument>> GetAllDocumentsAsync(CancellationToken cancellationToken = default);
    Task DeleteAllDocumentsAsync(CancellationToken cancellationToken = default);
}
