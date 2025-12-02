using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HRManagementSystem.Application.Services;

public class AIChatService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IAIChatService
{
    private readonly string _apiKey = configuration["AISettings:ApiKey"] ?? throw new ArgumentNullException("AISettings:ApiKey");
    private readonly string _endpoint = configuration["AISettings:ChatEndpoint"] ?? throw new ArgumentNullException("AISettings:ChatEndpoint");
    private readonly string _model = configuration["AISettings:ChatModel"] ?? throw new ArgumentNullException("AISettings:ChatModel");

    public async Task<Response<string>> GetChatCompletionAsync(string prompt, CancellationToken cancellationToken = default)
    {
        try
        {
            using HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var requestBody = new
            {
                model = _model,
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful HR assistant. Answer questions about employees, departments, leave policies, and other HR-related topics based on the provided context. Be concise and professional." },
                    new { role = "user", content = prompt }
                }
            };

            string jsonContent = JsonSerializer.Serialize(requestBody);
            using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(_endpoint, content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                ChatCompletionResponse? chatResponse = JsonSerializer.Deserialize<ChatCompletionResponse>(responseContent);

                if (chatResponse?.Choices != null && chatResponse.Choices.Count > 0)
                {
                    return new Response<string>(chatResponse.Choices[0].Message.Content, string.Empty, false);
                }
            }

            string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            return new Response<string>(string.Empty, $"AI service error: {response.StatusCode} - {errorContent}", true);
        }
        catch (Exception ex)
        {
            return new Response<string>(string.Empty, $"Error getting chat completion: {ex.Message}", true);
        }
    }
}

internal class ChatCompletionResponse
{
    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; } = [];
}

internal class Choice
{
    [JsonPropertyName("message")]
    public MessageContent Message { get; set; } = null!;
}

internal class MessageContent
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}
