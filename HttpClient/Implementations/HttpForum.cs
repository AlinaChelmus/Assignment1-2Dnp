using System.Net.Http.Json;
using System.Text.Json;
using HTTPClient.ClientInterfaces;
using Shared.DTO;
using Shared.Models;

namespace HTTPClient.Implementations;

public class HttpForum: IForumService
{
    private readonly HttpClient client;

    public HttpForum(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task CreateAsync(ForumCreationDTO dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/forum", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<IEnumerable<Forum>?> GetArticles(string? articleContent= null)
    {
        string uri = "/forum";
        if (!string.IsNullOrEmpty(articleContent))
        {
            uri += $"?tital={articleContent}";
        }

        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<Forum>? forums = JsonSerializer.Deserialize<IEnumerable<Forum>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return forums;
    }

    public async Task DeleteAsync(int articleId)
    {
        HttpResponseMessage response = await client.DeleteAsync($"forum/{articleId}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}