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
    public string Get()
    {
        
        return "true";
    }

    [HttpPost, Authorize]
    public void Post() // Create reference for user
    {
    }

}