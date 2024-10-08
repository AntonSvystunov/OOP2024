// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Hello, World!");

using var fileStream = File.OpenRead("books.json");

var books = JsonSerializer.Deserialize<List<Book>>(fileStream, new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    
});

Console.WriteLine(books.Count);

foreach (var book in books)
{
    Console.WriteLine(book);
}

books.Add(
    new Book(Random.Shared.Next().ToString(),
            "123" + Random.Shared.Next().ToString(),
            "Author 1"));

using var fileStreamWrite = File.OpenWrite("books2.json");

JsonSerializer.Serialize(fileStreamWrite, books, new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
});

public class Book(string Id, string Name, string AuthorName)
{
    public string Id { get; } = Id;

    public string Name { get; } = Name;

    [JsonPropertyName("author")]
    public string AuthorName { get; } = AuthorName;

    [JsonIgnore]
    public bool? IsActive => true;
}