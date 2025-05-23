using SalesTax;

namespace SalesTaxTests
{
    public class IntegrationTests
    {
        private void ExecuteTests(string[] inputs, string expectedOutput)
        {
            // Arrange
            Sale sale = new();
            foreach (var input in inputs)
            {
                sale.Add(input);
            }
            // Act
            string result = sale.ToString();
            // Assert
            Assert.Equal(expectedOutput, result);
        }
        [Fact]
        public void IntegrationTest1_BasicSale()
        {
            string[] inputs = new[]
            {
                "1 book at 12.49",
                "1 music CD at 14.99",
                "1 packet of chips at 0.85"
            };

            string expected = "1 book: 12.49\n" +
                              "1 music CD: 16.49\n" +
                              "1 packet of chips: 0.85\n" +
                              "Sales Taxes: 1.50\n" +
                              "Total: 29.83";

            ExecuteTests(inputs, expected);
        }
    }
}

