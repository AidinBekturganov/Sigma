using Sigma.Domain.Dto.ClientServices;

namespace Sigma.BLL.Interfaces;

public interface IClientService
{
    Task<AddClientResponseDto> CreateClientAsync(AddClientRequestDto clientInfo);
} 