namespace PimpYourBlech_Tests.ServiceTests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PimpYourBlech_ClassLibrary.Services.Comparator.Implementation;
using PimpYourBlech_Contracts.EntityDTOs;
using System.Collections.Generic;


[TestClass]
public class ComparatorServiceTests
{
    private ComparatorService _service;

    [TestInitialize]
    public void Setup()
    {
        _service = new ComparatorService();
    }

        // ----------------------------------------------------
        // CompareCars
        // ----------------------------------------------------

        [TestMethod]
        public void CompareCars_ReturnsCorrectRowsAndValues()
        {
            // Arrange
            var cars = new List<CarDto>
            {
                new CarDto
                {
                    Name = "Car A",
                    Brand = "BMW",
                    Model = "M3",
                    PS = 480,
                    Price = 80000,
                    Quantity = 2,
                    DateProduction = "2022",
                    DatePermit = "2021"
                },
                new CarDto
                {
                    Name = "Car B",
                    Brand = "Audi",
                    Model = "RS5",
                    PS = 450,
                    Price = 75000,
                    Quantity = 0,
                    DateProduction = "2020",
                    DatePermit = "2019"
                }
            };

            // Act
            var result = _service.CompareCars(cars);

            // Assert – Grundstruktur
            Assert.AreEqual(2, result.Cars.Count);
            Assert.AreEqual(8, result.Rows.Count);

            // Name
            CollectionAssert.AreEqual(
                new[] { "Car A", "Car B" },
                result.Rows.Find(r => r.Label == "Name").Values
            );

            // Marke
            CollectionAssert.AreEqual(
                new[] { "BMW", "Audi" },
                result.Rows.Find(r => r.Label == "Marke").Values
            );

            // Baujahr
            CollectionAssert.AreEqual(
                new[] { "2022", "2020" },
                result.Rows.Find(r => r.Label == "Baujahr").Values
            );

            // Erstzulassung
            CollectionAssert.AreEqual(
                new[] { "2021", "2019" },
                result.Rows.Find(r => r.Label == "Erstzulassung").Values
            );

            // Leistung
            CollectionAssert.AreEqual(
                new[] { "480 PS", "450 PS" },
                result.Rows.Find(r => r.Label == "Leistung").Values
            );

            // Preis
            CollectionAssert.AreEqual(
                new[] { "80.000 €", "75.000 €" },
                result.Rows.Find(r => r.Label == "Preis").Values
            );

            // Verfügbarkeit
            CollectionAssert.AreEqual(
                new[] { "Verfügbar", "Nicht verfügbar" },
                result.Rows.Find(r => r.Label == "Verfügbarkeit").Values
            );
        }

        // ----------------------------------------------------
        // CompareConfigurations
        // ----------------------------------------------------

        [TestMethod]
        public void CompareConfigurations_UsesTotalPsAndTotalPrice()
        {
            // Arrange
            var configurations = new List<ConfigurationDto>
            {
                new ConfigurationDto
                {
                    TotalPs = 300,
                    TotalPrice = 50000,
                    Car = new CarDto
                    {
                        Name = "Golf",
                        Brand = "VW",
                        Model = "GTI",
                        Quantity = 1,
                        DateProduction = "2020",
                        DatePermit = "2021"
                    }
                },
                new ConfigurationDto
                {
                    TotalPs = 250,
                    TotalPrice = 45000,
                    Car = new CarDto
                    {
                        Name = "Focus",
                        Brand = "Ford",
                        Model = "ST",
                        Quantity = 0,
                        DateProduction = "2019",
                        DatePermit = "2018"
                    }
                }
            };

            // Act
            var result = _service.CompareConfigurations(configurations);

            // Assert – Grundstruktur
            Assert.AreEqual(2, result.Configurations.Count);
            Assert.AreEqual(8, result.Rows.Count);

            // Leistung (TotalPs!)
            CollectionAssert.AreEqual(
                new[] { "300 PS", "250 PS" },
                result.Rows.Find(r => r.Label == "Leistung").Values
            );

            // Preis (TotalPrice!)
            CollectionAssert.AreEqual(
                new[] { "50.000 €", "45.000 €" },
                result.Rows.Find(r => r.Label == "Preis").Values
            );

            // Baujahr
            CollectionAssert.AreEqual(
                new[] { "2020", "2019" },
                result.Rows.Find(r => r.Label == "Baujahr").Values
            );

            // Verfügbarkeit
            CollectionAssert.AreEqual(
                new[] { "Verfügbar", "Nicht verfügbar" },
                result.Rows.Find(r => r.Label == "Verfügbarkeit").Values
            );
        }

        // ----------------------------------------------------
        // Edge Case
        // ----------------------------------------------------

        [TestMethod]
        public void CompareCars_WithEmptyList_ReturnsEmptyValues()
        {
            // Act
            var result = _service.CompareCars(new List<CarDto>());

            // Assert
            Assert.AreEqual(0, result.Cars.Count);
            Assert.AreEqual(8, result.Rows.Count);

            foreach (var row in result.Rows)
            {
                Assert.AreEqual(0, row.Values.Count);
            }
        }
    }
