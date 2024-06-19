using AutoMapper;
using Sigma.BLL.Interfaces;
using Sigma.DAL.Interfaces;
using Sigma.Domain.Dto.Client;
using Sigma.Domain.Dto.ClientServices;
using Sigma.Domain.Entity;
using Sigma.Domain.Enums.ClientService;

namespace Sigma.BLL.Services;

public class ClientService : IClientService
{
    private readonly IRepository<Client> _repository;
    private readonly IMapper _mapper;
    
    public ClientService(IMapper mapper, IRepository<Client> repository)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<AddClientResponseDto> CreateClientAsync(AddClientRequestDto clientInfo)
    {
        try
        {
            var validator = new AddClientRequestDtoValidation();
            var validationResult = await validator.ValidateAsync(clientInfo);

            if (!validationResult.IsValid)
            {
                return new AddClientResponseDto(ClientServiceResponseStatuses.ValidationFailed) { Errors = validationResult.Errors };
            }
            
            var client = (await _repository.GetAllAsync()).ToList().FirstOrDefault(x => x.Email == clientInfo.Email);

            if (client is not null)
            {
                var clientDto = _mapper.Map<ClientDto>(await _repository.UpdateAsync(client));
                
                return new AddClientResponseDto(ClientServiceResponseStatuses.Success) { Client = clientDto };
            }
            else
            {
                var newClient = _mapper.Map<Client>(clientInfo);

                var clientDto = _mapper.Map<ClientDto>(await _repository.AddAsync(newClient));

                return new AddClientResponseDto(ClientServiceResponseStatuses.Success) { Client = clientDto };
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new AddClientResponseDto(ClientServiceResponseStatuses.Failed);
        }
    }
}