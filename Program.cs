namespace e06;


using System.Text.Json;
using e06.application.dto;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Insert the developer email: ");
        string? email = Console.ReadLine();

        if (email is not null)
        {
            // Get data from the server. Endpoint: https://jsonplaceholder.typicode.com/users?email={email}


            Developer? developer = GetDeveloperByEmail(email).Result;
            if (developer is not null)
            {
                // Get todos from the server. Endpoint: https://jsonplaceholder.typicode.com/users/{userId}/todos
                Todo[] todos = GetTodosByUserId(developer.id).Result;

                //developer.todos = todos; ask to Jose why this is not working and which is the best way to do it

                Developer developerWithTodos = new Developer(
                    developer.id,
                    developer.name,
                    developer.username,
                    developer.email,
                    developer.address,
                    developer.phone,
                    developer.website,
                    todos
                );

                string fileName = "personalAndToDos.json";
                string jsonString = JsonSerializer.Serialize(developerWithTodos);
                File.WriteAllText(fileName, jsonString);

            }
            else
            {
                Console.WriteLine("Developer not found");
            }
        }
        else
        {
            Console.WriteLine("Error reading email");
        }
    }

    private static async Task<Developer?> GetDeveloperByEmail(string email)
    {
        using HttpClient client = new HttpClient();
        using HttpResponseMessage response = await client.GetAsync($"https://jsonplaceholder.typicode.com/users?email={email}");
        using HttpContent content = response.Content;

        string? data = await content.ReadAsStringAsync();
        if (data is not null)
        {
            Developer[]? developers = JsonSerializer.Deserialize<Developer[]>(data);
            if (developers is not null)
            {
                return developers[0];
            }
        }

        return null;
    }

    private static async Task<Todo[]> GetTodosByUserId(int userId)
    {
        using HttpClient client = new HttpClient();
        using HttpResponseMessage response = await client.GetAsync($"https://jsonplaceholder.typicode.com/users/{userId}/todos");
        using HttpContent content = response.Content;

        string? data = await content.ReadAsStringAsync();
        if (data is not null)
        {
            Todo[]? todos = JsonSerializer.Deserialize<Todo[]>(data);
            if (todos is not null)
            {
                return todos;
            }
        }

        return Array.Empty<Todo>();
    }
}