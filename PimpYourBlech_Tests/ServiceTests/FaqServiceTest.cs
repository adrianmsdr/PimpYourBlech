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
    // Mock für das Inventory, um die Datenbankschicht zu simulieren
    private Mock<ICustomerInventory> _inventoryMock;
    
    private FaqService _service;

   
    // Wird vor jedem Test ausgeführt.
    // Initialisiert eine frische Service-Instanz mit einem Mock,
    // um Seiteneffekte zwischen Tests zu vermeiden.
    [TestInitialize]
    public void Setup()
    {
        _inventoryMock = new Mock<ICustomerInventory>();
        _service = new FaqService(_inventoryMock.Object);
    }

    // ---------------------------------------------------------
    // GetCommunityQuestionsAsync
    // ---------------------------------------------------------

    
    // Prüft, ob Community-Fragen inklusive ihrer Antworten
    // korrekt aus den Entities in DTOs gemappt werden.
    [TestMethod]
    public async Task GetCommunityQuestionsAsync_MapsQuestionsAndAnswersCorrectly()
    {
        // Arrange:
        // Simulierte Daten, wie sie aus der Datenbank kommen würden
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

        // Mock gibt beim Aufruf genau diese Testdaten zurück
        _inventoryMock
            .Setup(i => i.GetCommunityQuestionsAsync())
            .ReturnsAsync(questions);

        // Act:
        // Aufruf der zu testenden Service-Methode
        var result = await _service.GetCommunityQuestionsAsync();

        // Assert:
        // Überprüfung, ob Mapping und Datenübernahme korrekt erfolgt sind
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Frage 1", result[0].Content);
        Assert.AreEqual(1, result[0].Answers.Count);
        Assert.AreEqual("Antwort 1", result[0].Answers[0].Content);
    }

    // ---------------------------------------------------------
    // AddCommunityQuestionAsync
    // ---------------------------------------------------------
    
    // Prüft, ob eine neue Community-Frage korrekt
    // an das Inventory weitergeleitet wird.
    
    [TestMethod]
    public async Task AddCommunityQuestionAsync_ForwardsContentToInventory()
    {
        // Arrange:
        // DTO, das aus der UI stammen würde
        var dto = new CommunityQuestionCreateDto
        {
            Content = "Neue Frage"
        };

        // Act:
        // Service-Methode ausführen
        await _service.AddCommunityQuestionAsync(dto);

        // Assert:
        // Überprüfung, ob das Inventory genau einmal
        // mit dem richtigen Inhalt aufgerufen wurde
        _inventoryMock.Verify(
            i => i.AddCommunityQuestionAsync("Neue Frage"),
            Times.Once
        );
    }

    // ---------------------------------------------------------
    // AddCommunityAnswerAsync
    // ---------------------------------------------------------

    
    // Prüft, ob eine neue Antwort korrekt
    // mit QuestionId und Text an das Inventory weitergegeben wird.
    [TestMethod]
    public async Task AddCommunityAnswerAsync_ForwardsAnswerCorrectly()
    {
        // Arrange:
        // DTO mit Referenz auf die zugehörige Frage
        var dto = new CommunityAnswerCreateDto
        {
            QuestionId = 5,
            Content = "Neue Antwort"
        };

        // Act:
        // Service-Methode ausführen
        await _service.AddCommunityAnswerAsync(dto);

        // Assert:
        // Sicherstellen, dass genau ein Aufruf
        // mit den erwarteten Parametern erfolgt ist
        _inventoryMock.Verify(
            i => i.AddCommunityAnswerAsync(5, "Neue Antwort"),
            Times.Once
        );
    }
}