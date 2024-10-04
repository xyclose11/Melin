using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Melin.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ReferenceController : ControllerBase
{
    private readonly ApiService _apiService;

    public ReferenceController(ApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpGet, Authorize]
    public async Task<IActionResult> Get()
    {
        try
        {
            using (FileStream openStream = System.IO.File.OpenRead(@"Q:\development\dotnet\Melin\Melin.Server\Data\tasks.json"))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                List<Task> tasks = await JsonSerializer.DeserializeAsync<List<Task>>(openStream, options);

                foreach (var task in tasks)
                {
                    Console.WriteLine($"ID: {task.Id},  Status: {task.Status}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
        }
        await using FileStream stream = System.IO.File.OpenRead(@"Q:\development\dotnet\Melin\Melin.Server\Data\tasks.json");
        return await JsonSerializer.DeserializeAsync<IActionResult>(stream);
    }
    
}