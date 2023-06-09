﻿using System.Text.Json.Serialization;

namespace Shared.Models;

public class User
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
    [JsonPropertyName("role")]
    public string Role { get; set; }
    
}