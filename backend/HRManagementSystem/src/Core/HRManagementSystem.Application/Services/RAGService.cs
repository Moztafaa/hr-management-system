using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Chat;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces;
using MongoDB.Bson;

namespace HRManagementSystem.Application.Services;

public class RAGService(
    IEmbeddingService embeddingService,
    IMongoDBVectorService mongoDBVectorService,
    IAIChatService aiChatService,
    IUnitOfWork unitOfWork) : IRAGService
{
    public async Task<Response<ChatResponseDto>> AskQuestionAsync(string question, CancellationToken cancellationToken = default)
    {
        try
        {
            // Convert question to embedding
            Response<List<float>> embeddingResponse = await embeddingService.ConvertToEmbeddingAsync(question, cancellationToken);

            if (embeddingResponse.HasError || embeddingResponse.Data == null || embeddingResponse.Data.Count == 0)
            {
                return new Response<ChatResponseDto>(
                    null!,
                    embeddingResponse.ErrorMessage ?? "Failed to generate question embedding",
                    true);
            }

            // Get similar context from MongoDB
            string context = await GetSimilarContextAsync(embeddingResponse.Data, cancellationToken);

            // Build the prompt with context (matching AITask pattern)
            string prompt = string.IsNullOrEmpty(context)
                ? question
                : $"according to this HR information: {context}, {question}";

            // Get AI response
            Response<string> chatResponse = await aiChatService.GetChatCompletionAsync(prompt, cancellationToken);

            if (chatResponse.HasError)
            {
                return new Response<ChatResponseDto>(null!, chatResponse.ErrorMessage, true);
            }

            var response = new ChatResponseDto
            {
                Answer = chatResponse.Data ?? "I couldn't generate a response.",
                Timestamp = DateTime.UtcNow
            };

            return new Response<ChatResponseDto>(response, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<ChatResponseDto>(null!, $"Error processing question: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> AddContextAsync(string entityType, string text, CancellationToken cancellationToken = default)
    {
        try
        {
            Response<List<float>> embeddingResponse = await embeddingService.ConvertToEmbeddingAsync(text, cancellationToken);

            if (embeddingResponse.HasError || embeddingResponse.Data == null || embeddingResponse.Data.Count == 0)
            {
                return new Response<bool>(false, embeddingResponse.ErrorMessage ?? "Failed to generate embedding", true);
            }

            var document = new BsonDocument
            {
                { "entityType", entityType },
                { "text", text },
                { "embedding", new BsonArray(embeddingResponse.Data) },
                { "createdAt", DateTime.UtcNow }
            };

            await mongoDBVectorService.InsertDocumentAsync(document, cancellationToken);

            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error adding context: {ex.Message}", true);
        }
    }

    public async Task<Response<int>> SyncAllEmployeesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Get all employees from the database
            IEnumerable<Employee> employees = await unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken);
            int syncedCount = 0;

            foreach (Employee employee in employees)
            {
                string employeeText = $"Employee ID: {employee.Id} - Name: {employee.FirstName} {employee.LastName} - " +
                                      $"Email: {employee.Email} - Contact: {employee.ContactNumber ?? "N/A"} - " +
                                      $"Gender: {employee.Gender ?? "N/A"} - Address: {employee.Address ?? "N/A"} - " +
                                      $"Hire Date: {employee.HireDate:yyyy-MM-dd} - Basic Salary: {employee.BasicSalary}";

                Response<bool> result = await AddContextAsync("Employee", employeeText, cancellationToken);
                if (!result.HasError)
                {
                    syncedCount++;
                }
            }

            return new Response<int>(syncedCount, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<int>(0, $"Error syncing employees: {ex.Message}", true);
        }
    }

    private async Task<string> GetSimilarContextAsync(List<float> promptEmbedding, CancellationToken cancellationToken)
    {
        List<BsonDocument> allDocuments = await mongoDBVectorService.GetAllDocumentsAsync(cancellationToken);

        if (allDocuments.Count == 0)
        {
            return string.Empty;
        }

        var contextList = allDocuments
            .Select(doc => new
            {
                Text = doc.Contains("text") ? doc["text"].AsString : string.Empty,
                Embedding = doc.Contains("embedding")
                    ? doc["embedding"].AsBsonArray.Select(x => (float)x.AsDouble).ToArray()
                    : []
            })
            .Where(e => e.Embedding.Length > 0)
            .Select(e => new
            {
                e.Text,
                Similarity = CosineSimilarity(promptEmbedding.ToArray(), e.Embedding)
            })
            .OrderByDescending(e => e.Similarity)
            .Take(5)
            .Select(e => e.Text)
            .ToList();

        return string.Join("\n", contextList);
    }

    private static double CosineSimilarity(float[] vector1, float[] vector2)
    {
        if (vector1.Length != vector2.Length || vector1.Length == 0)
        {
            return 0;
        }

        double dotProduct = 0;
        double magnitude1 = 0;
        double magnitude2 = 0;

        for (int i = 0; i < vector1.Length; i++)
        {
            dotProduct += vector1[i] * vector2[i];
            magnitude1 += vector1[i] * vector1[i];
            magnitude2 += vector2[i] * vector2[i];
        }

        magnitude1 = Math.Sqrt(magnitude1);
        magnitude2 = Math.Sqrt(magnitude2);

        if (magnitude1 == 0 || magnitude2 == 0)
        {
            return 0;
        }

        return dotProduct / (magnitude1 * magnitude2);
    }
}
