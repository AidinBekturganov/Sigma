using AutoMapper;
using Moq;
using Sigma.BLL.Services;
using Sigma.DAL.Interfaces;
using Sigma.Domain.Dto.Client;
using Sigma.Domain.Dto.ClientServices;
using Sigma.Domain.Entity;
using Sigma.Domain.Enums.ClientService;
using Xunit;

namespace Sigma.Test.ClientServiceTests;

public class ClientTests
{
    private readonly Mock<IRepository<Client>> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ClientService _clientService;
    private readonly AddClientRequestDto _sampleClient;

    public ClientTests()
    {
        // Initialize the class to be tested
        _mockMapper = new Mock<IMapper>();
        _mockRepo = new Mock<IRepository<Client>>();
        _clientService = new ClientService(_mockMapper.Object, _mockRepo.Object);
        _sampleClient = new AddClientRequestDto { 
            FirstName = "Aidin", LastName = "Bekturganov", Email = "bekaid@gmail.com",
            Comment = "My comments", EndInterval = new DateTime(2024, 6, 19, 12, 0, 0),
            StartInterval = new DateTime(2024, 6, 19, 10, 0, 0),GithubURL = "https://github.com/",
            LinkednURL = "https://linkedn.com/", PhoneNumber = "+996555222444"
        };
    }

    [Fact]
    public async Task CreateClientAsync_ShouldReturnSuccess_OnSuccess()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Client>());
        _mockRepo.Setup(r => r.AddAsync(It.IsAny<Client>())).ReturnsAsync(new Client());
        _mockMapper.Setup(m => m.Map<Client>(It.IsAny<AddClientRequestDto>())).Returns(new Client());

        // Act
        var result = await _clientService.CreateClientAsync(_sampleClient);

        // Assert
        Assert.Equal(ClientServiceResponseStatuses.Success, result.Status);
    }

    [Fact]
    public async Task CreateClientAsync_ShouldReturnFailed_OnException()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAllAsync()).Throws(new Exception());

        // Act
        var result = await _clientService.CreateClientAsync(_sampleClient);

        // Assert
        Assert.Equal(ClientServiceResponseStatuses.Failed, result.Status);
    }

    [Fact]
    public async Task CreateClientAsync_ShouldReturnSuccess_AndUpdateClient_IfClientExists()
    {
        // Arrange
        var existingClient = new Client { 
            FirstName = "Aidin", LastName = "Bekturganov", Email = "bekaid@gmail.com",
            Comment = "My comments", EndInterval = new DateTime(2024, 6, 19, 10, 0, 0),
            StartInterval = new DateTime(2024, 6, 19, 8, 0, 0),GithubURL = "https://github.com/",
            LinkednURL = "https://linkedn.com/", PhoneNumber = "+996700222444"
        };
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new[] { existingClient });
        _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Client>())).ReturnsAsync(existingClient);
        _mockMapper.Setup(m => m.Map<ClientDto>(existingClient)).Returns(new ClientDto());

        // Act
        var result = await _clientService.CreateClientAsync(_sampleClient);

        // Assert
        Assert.Equal(ClientServiceResponseStatuses.Success, result.Status);
    }
    
    [Fact]
    public async Task CreateClientAsync_ShouldReturnValidationFailed_IfValidationFailed()
    {
        // Arrange: Specify an invalid client DTO here
        var invalidClientDto = new AddClientRequestDto { 
            FirstName = "Aidin", LastName = "Bekturganov", Email = "bekaid@gmail",
            Comment = "My comments", EndInterval = new DateTime(2024, 6, 19, 12, 0, 0),
            StartInterval = new DateTime(2024, 6, 19, 10, 0, 0),GithubURL = "https://github.com/",
            LinkednURL = "https://linkedn.com/", PhoneNumber = "+996555222444"
        };

        // Act
        var result = await _clientService.CreateClientAsync(invalidClientDto);

        // Assert
        Assert.Equal(ClientServiceResponseStatuses.ValidationFailed, result.Status);
      }
}