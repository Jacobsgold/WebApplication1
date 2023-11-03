using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

class Client
{
    static async Task Main()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:7244"); // Укажите адрес вашего сервера

        // Пример GET-запроса для получения списка пользователей
        var getUsersResponse = await client.GetAsync("/api/users");
        if (getUsersResponse.IsSuccessStatusCode)
        {
            var users = await getUsersResponse.Content.ReadFromJsonAsync<Person[]>();
            foreach (var user in users)
            {
                Console.WriteLine($"Name: {user.Name}, Age: {user.Age}");
            }
        }

        // Пример POST-запроса для создания нового пользователя
        var newUser = new Person { Name = "Alice", Age = 30 };
        var postUserResponse = await client.PostAsJsonAsync("/api/users", newUser);
        if (postUserResponse.IsSuccessStatusCode)
        {
            var createdUser = await postUserResponse.Content.ReadFromJsonAsync<Person>();
            Console.WriteLine($"New user created - Id: {createdUser.Id}");
        }
    }
}
