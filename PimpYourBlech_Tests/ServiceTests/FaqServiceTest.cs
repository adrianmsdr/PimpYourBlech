using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PimpYourBlech_ClassLibrary.Services.FAQ;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Data.Inventories;
using PimpYourBlech_Data.Models;


/* Hier wird mit Moq ein Objekt zur Laufzeit erzeugt, das:

    -das Interface implementiert

    -exakt das zurückgibt, was du vorgibst

    -Aufrufe protokolliert
    
 ((  dotnet add package Moq )) */

namespace PimpYourBlech_Tests.Services.FAQ;

[TestClass]
public class FaqServiceTests
{
    private Mock<ICustomerInventory> _inventoryMock;
    private FaqService _service;

    [TestInitialize]
    public void Setup()
    {
        _inventoryMock = new Mock<ICustomerInventory>();
        _service = new FaqService(_inventoryMock.Object);
    }

    // ---------------------------------------------
    // GetCommunityQuestionsAsync
    // ---------------------------------------------

    [TestMethod]
    public async Task GetCommunityQuestionsAsync_MapsQuestionsAndAnswersCorrectly()
    {
        // Arrange
        var questions = new List<CommunityQuestion>
        {
            new CommunityQuestion
            {
                Id = 1,
                Content = "Frage 1",
                CreatedAt = DateTime.Now,
                Answers = new List<CommunityAnswer>
                {
                    new CommunityAnswer
                    {
                        Id = 10,
                        Content = "Antwort 1",
                        CreatedAt = DateTime.Now,
                        QuestionId = 1
                    }
                }
            }
        };

        _inventoryMock
            .Setup(i => i.GetCommunityQuestionsAsync())
            .ReturnsAsync(questions);

        // Act
        var result = await _service.GetCommunityQuestionsAsync();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Frage 1", result[0].Content);
        Assert.AreEqual(1, result[0].Answers.Count);
        Assert.AreEqual("Antwort 1", result[0].Answers[0].Content);
    }

    // ---------------------------------------------
    // AddCommunityQuestionAsync
    // ---------------------------------------------

    [TestMethod]
    public async Task AddCommunityQuestionAsync_ForwardsContentToInventory()
    {
        // Arrange
        var dto = new CommunityQuestionCreateDto
        {
            Content = "Neue Frage"
        };

        // Act
        await _service.AddCommunityQuestionAsync(dto);

        // Assert
        _inventoryMock.Verify(
            i => i.AddCommunityQuestionAsync("Neue Frage"),
            Times.Once
        );
    }

    // ---------------------------------------------
    // AddCommunityAnswerAsync
    // ---------------------------------------------

    [TestMethod]
    public async Task AddCommunityAnswerAsync_ForwardsAnswerCorrectly()
    {
        // Arrange
        var dto = new CommunityAnswerCreateDto
        {
            QuestionId = 5,
            Content = "Neue Antwort"
        };

        // Act
        await _service.AddCommunityAnswerAsync(dto);

        // Assert
        _inventoryMock.Verify(
            i => i.AddCommunityAnswerAsync(5, "Neue Antwort"),
            Times.Once
        );
    }
}
