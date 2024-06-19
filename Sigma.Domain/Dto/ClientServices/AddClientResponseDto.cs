using Sigma.Domain.Dto.Client;
using Sigma.Domain.Enums.ClientService;

namespace Sigma.Domain.Dto.ClientServices;

public class AddClientResponseDto : BaseDto<AddClientResponseDto>
{
    public AddClientResponseDto(ClientServiceResponseStatuses status) 
    {
        Status = status;
    }

    public ClientServiceResponseStatuses Status { get; set; }
    public ClientDto Client { get; set; }
}