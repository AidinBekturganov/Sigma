using Microsoft.AspNetCore.Mvc;
using Sigma.BLL.Interfaces;
using Sigma.Domain.Dto.ClientServices;

namespace Sigma.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ClientServiceController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientServiceController(IClientService clientService)
    {
        _clientService = clientService;
    }
    
    [HttpPut]
    public async Task<AddClientResponseDto> Update(AddClientRequestDto clientRequestDto)
    {
        return await _clientService.CreateClientAsync(clientRequestDto);
    }
}