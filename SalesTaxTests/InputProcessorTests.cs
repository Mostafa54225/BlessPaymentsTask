using SalesTax;

namespace SalesTaxTests
{
    public class InputProcessorTests
    {
        [Theory]
        [InlineData("1 book at 12.49")]
        [InlineData("1 music CD at 14.99")]
        [InlineData("1 packet of chips at 0.85")]
        [InlineData("10 imported bottles of whiskey at 27.99")]
        public void ProcessInput_ValidFormat_ReturnSucess(string input)
        {
            // Act
            ParseResult result = InputParser.ProcessInput(input);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.SaleLine);
        }

        [Theory]
        [InlineData("")]
        [InlineData("js s jss s")]
        [InlineData("book")]
        [InlineData("12.49")]
        [InlineData("book at 12.49")]
        [InlineData("1 book 12.49")]
        [InlineData("x book at 12.49")] // Non-numeric quantity
        [InlineData("1 book at x")]
        public void ProcessInput_InvalidFormat_ReturnFailure(string input)
        {
            // Act
            ParseResult result = InputParser.ProcessInput(input);
            // Assert
            Assert.False(result.Success);
            Assert.Null(result.SaleLine);
        }
        [Theory]
        [InlineData("1 imported box of chips at 10.00", true)]
        [InlineData("1 box of imported chips at 10.00", true)]
        //[InlineData("1 box of chips imported at 10.00", true)]
        [InlineData("1 box of chips at 10.00", false)]
        public void ProcessInput_ImportedItems_DetectsImportedCorrectly(string input, bool expectedIsImported)
        {
            // Act
            var result = InputParser.ProcessInput(input);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(expectedIsImported, result.SaleLine.IsImported);
        }

        [Theory]
        [InlineData("1 book at 12.49", 1)]
        [InlineData("2 music CDs at 14.99", 2)]
        [InlineData("10 packets of chips at 0.85", 10)]
        [InlineData("99 imported bottles of perfume at 27.99", 99)]
        public void ProcessInput_ExtractsQuantityCorrectly(string input, int expectedQuantity)
        {
            // Act
            var result = InputParser.ProcessInput(input);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(expectedQuantity, result.SaleLine.Quantity);
        }

        [Theory]
        [InlineData("1 book at 12.49", 12.49)]
        [InlineData("1 music CD at 14.99", 14.99)]
        [InlineData("1 packet of chips at 0.85", 0.85)]
        [InlineData("1 imported bottle of perfume at 27.99", 27.99)]
        [InlineData("1 expensive item at 9999.99", 9999.99)]
        public void ProcessInput_ExtractsPriceCorrectly(string input, decimal expectedPrice)
        {
            // Act
            var result = InputParser.ProcessInput(input);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(expectedPrice, result.SaleLine.Price);
        }

        [Theory]
        [InlineData("1 book at 12.49", "book")]
        [InlineData("1 music CD at 14.99", "music CD")]
        //[InlineData("1 packet of chips at 0.85", "packet of chips")]
        //[InlineData("1 imported bottle of perfume at 27.99", "bottle of perfume")]
        //[InlineData("1 box of imported chocolates at 11.25", "box of chocolates")]
        public void ProcessInput_ExtractsProductNameCorrectly(string input, string expectedName)
        {
            // Act
            var result = InputParser.ProcessInput(input);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(expectedName, result.SaleLine.ProductName);
        }
    }
}
