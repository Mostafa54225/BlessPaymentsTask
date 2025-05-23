using SalesTax;

namespace SalesTaxTests
{
    public class SaleLineTests
    {
        [Theory]
        [InlineData("packet of headache tablets")]
        [InlineData("iodine tablets")]
        [InlineData("medicine for cold")]
        [InlineData("bandage")]
        [InlineData("pharmaceutical product")]
        public void MedicalItem_ShouldHaveNoBaseTax(string item)
        {
            // Act
            var saleLine = new SaleLine(1, item, 10.00m, false);

            // Assert
            Assert.Equal(0.00m, saleLine.Tax);
        }

        [Theory]
        [InlineData("chocolate bar")]
        [InlineData("box of chocolates")]
        [InlineData("packet of chips")]
        [InlineData("potato chips")]
        [InlineData("snack")]
        [InlineData("candy")]
        public void FoodItem_ShouldHaveNoBaseTax(string item)
        {
            // Act
            var saleLine = new SaleLine(1, item, 10.00m, false);

            // Assert
            Assert.Equal(0.00m, saleLine.Tax);
        }

        [Fact]
        public void BookItem_ShouldHaveNoBaseTax()
        {
            // Act
            var saleLine = new SaleLine(1, "book", 10.00m, false);

            // Assert
            Assert.Equal(0.00m, saleLine.Tax);
        }

        [Theory]
        [InlineData("music CD")]
        [InlineData("perfume")]
        [InlineData("transformer")]
        [InlineData("barrell of oil")]
        [InlineData("bottle of whiskey")]
        public void StandardItem_ShouldHaveTenPercentTax(string item)
        {
            // Act
            var saleLine = new SaleLine(1, item, 10.00m, false);

            // Assert
            Assert.Equal(1.00m, saleLine.Tax);
        }

        [Theory]
        [InlineData("box of chocolates")]
        [InlineData("packet of headache tablets")]
        [InlineData("book")]
        public void ImportedExemptItem_ShouldHaveFivePercentTax(string item)
        {
            // Act
            var saleLine = new SaleLine(1, item, 10.00m, true);

            // Assert - checking for 0.50 tax (5% of 10.00)
            Assert.Equal(0.50m, saleLine.Tax);
        }

        [Theory]
        [InlineData("music CD")]
        [InlineData("perfume")]
        [InlineData("transformer")]
        [InlineData("barrell of oil")]
        public void ImportedStandardItem_ShouldHaveFifteenPercentTax(string item)
        {
            // Act
            var saleLine = new SaleLine(1, item, 10.00m, true);

            // Assert - checking for 1.50 tax (15% of 10.00)
            Assert.Equal(1.50m, saleLine.Tax);
        }


        [Fact]
        public void MultipleQuantities_ShouldMultiplyTax()
        {
            // Act
            var saleLine = new SaleLine(5, "music CD", 10.00m, false);

            // Assert
            Assert.Equal(5.00m, saleLine.Tax);
            Assert.Equal(55.00m, saleLine.LineValue);
        }

        [Fact]
        public void ProductName_WithImported_ShouldNotAffectTaxation()
        {
            // Arrange - create with isImported flag = true but no "imported" in name
            var saleLine1 = new SaleLine(1, "box of chocolates", 10.00m, true);

            // Arrange - create with both isImported flag = true and "imported" in name
            var saleLine2 = new SaleLine(1, "imported box of chocolates", 10.00m, true);

            // Assert - both should have the same tax
            Assert.Equal(saleLine1.Tax, saleLine2.Tax);
        }
    }
}
