using HTTPClient.ClientInterfaces;
using Shared.DTO;
using Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace HTTPClient.Implementations;

public class HttpUser: IUserService
{
    private readonly HttpClient client;
    

    public HttpUser(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<User> Create(UserCreationDTO dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/users", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<IEnumerable<User>> GetUsers(string? usernameContains = null)
    {
        string uri = "/users";
        if (!string.IsNullOrEmpty(usernameContains))
        {
            uri += $"?username={usernameContains}";
        }
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }
    
    private IList<User> users { get;  }

    public User ValidateUser(string userName, string Password)
    {
        User first = users.FirstOrDefault(user => user.UserName.Equals(userName));
        if (first == null)
        {
            throw new Exception("User not found");
        }

        if (!first.Password.Equals(Password))
        {
            throw new Exception("Incorrect password");
        }
        return first;
    }
    
    public void SaveChanges()
    {
        var jsonUsers = JsonSerializer.Serialize(users, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        using (var outputFile = new StreamWriter(jsonUsers, false))
        {
            outputFile.Write(jsonUsers);
        }
    }

    public void RegisterUser(User user)
    {
        users.Add(user);
        SaveChanges();
    }
}