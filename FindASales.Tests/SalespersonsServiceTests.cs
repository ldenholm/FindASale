using FindASale.Models;
using FindASale.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FindASales.Tests
{
    public class SalespersonsServiceTests
    {
        [Fact]
        public void Returns_IEnumerable_Of_AvailableSalespersons()
        {
            // Arrange
            var mock = new Mock<ISalespersonRepository>();
            
            var _salesRepo = mock.Object;
            
            var salesperstestList = new List<Salesperson>
            {
                new Salesperson(){Name = "MrTest", Groups = new List<char>{ 'A' }, IsAvailable = true},
                new Salesperson(){Name = "Lochlan Denholm", Groups = new List<char>{ 'C' }, IsAvailable = false},
                new Salesperson(){Name = "Alexander The Great", Groups = new List<char>{ 'A', 'B' }, IsAvailable = true}
            };

            // Act
            var expected = salesperstestList.Where(x => x.IsAvailable == true);
            var test = _salesRepo.GetAllAvailableSalespersons(salesperstestList);


            // Assert
            Assert.Equal(expected, test);
        }
    }
}
