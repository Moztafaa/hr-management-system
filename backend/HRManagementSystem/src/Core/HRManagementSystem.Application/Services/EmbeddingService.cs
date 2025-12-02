using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HRManagementSystem.Application.Services;

public class EmbeddingService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IEmbeddingService
{
    private readonly string _apiKey = configuration["AISettings:ApiKey"] ?? throw new ArgumentNullException("AISettings:ApiKey");
    private readonly string _endpoint = configuration["AISettings:EmbeddingEndpoint"] ?? throw new ArgumentNullException("AISettings:EmbeddingEndpoint");
    private readonly string _model = configuration["AISettings:EmbeddingModel"] ?? throw new ArgumentNullException("AISettings:EmbeddingModel");

    public async Task<Response<List<float>>> ConvertToEmbeddingAsync(string text, CancellationToken cancellationToken = default)
    {
        try
        {
            using HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var requestBody = new
            {
                input = text,
                model = _model
            };

            string jsonContent = JsonSerializer.Serialize(requestBody);
            using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(_endpoint, content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                EmbeddingResponse? embeddingResponse = JsonSerializer.Deserialize<EmbeddingResponse>(responseContent);

                if (embeddingResponse?.Data != null && embeddingResponse.Data.Count > 0)
                {
                    return new Response<List<float>>(embeddingResponse.Data[0].Embedding, string.Empty, false);
                }
            }

            string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            return new Response<List<float>>([], $"Embedding service error: {response.StatusCode} - {errorContent}", true);
        }
        catch (Exception ex)
        {
            return new Response<List<float>>([], $"Error generating embedding: {ex.Message}", true);
        }
    }
}

internal class EmbeddingResponse
{
    [JsonPropertyName("data")]
    public List<EmbeddingData> Data { get; set; } = [];
}

internal class EmbeddingData
{
    [JsonPropertyName("embedding")]
    public List<float> Embedding { get; set; } = [];
}
