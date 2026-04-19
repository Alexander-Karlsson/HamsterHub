using HamsterHub.Application.Services;
using HamsterHub.Domain.Entities;
using HamsterHub.Domain.Interfaces;
using HamsterHub.Domain.Enums;
using Moq;

namespace HamsterHub.Tests;

public class HamsterServiceTests
{
    private readonly Mock<IHamsterRepository> _mockRepo;
    private readonly HamsterService _service;

    public HamsterServiceTests()
    {
        _mockRepo = new Mock<IHamsterRepository>();
        _service = new HamsterService(_mockRepo.Object);
    }
    
    /*
        ORSAK: Implementerar några xUnit-tester enbart för att visa att kiunskapen finns.
        I Detta fallet så är testerna överflödiga enligt min åsikt då appen är så pass enkel.
        
        Oavsett så följer testerna de typiska stegen Arrange, Act och Assert.
     */

    [Fact]
    public async Task GetAllHamsters_ShouldReturnAllHamsters()
    {
        var testData = new List<Hamster>
        {
            new Hamster { Id = 1, Name = "Sven Ingvar" },
            new Hamster { Id = 2, Name = "Glenn Hysén" }
        };
        
        _mockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync(testData);
        
        var result = await _service.GetAllAsync();
        
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetHamsterById_ShouldReturnHamsterWithGivenId()
    {
        var testData = new Hamster { Id = 1, Name = "Kalle Moraeus" };

        _mockRepo.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(testData);

        var result = await _service.GetByIdAsync(1);
        
        Assert.Equal(testData.Id, result?.Id);
    }

    [Fact]
    public async Task GetByPersonality_ShouldReturnHamstersWithGivenPersonality()
    {
        var testData = new List<Hamster>
        {
            new Hamster { Id = 1, Name = "Ulf Kristersson", Personality = Personality.Chill },
            new Hamster { Id = 2, Name = "Lotta Engberg", Personality = Personality.Chill }
        };
        
        _mockRepo.Setup(r => r.GetByPersonalityAsync(Personality.Chill))
            .ReturnsAsync(testData);

        var result = (await _service.GetByPersonalityAsync(Personality.Chill)).ToList();
        
        Assert.All(result, h => Assert.Equal(Personality.Chill, h.Personality));
        Assert.Equal(testData.Count, result.Count);
    }
    
}