using SalesTax;

namespace SalesTaxTests
{
    public class SaleTests
    {
        [Fact]
        public void Test1_BasicSale()
        {
            // Arrange
            Sale sale = new();
            sale.Add("1 book at 12.49");
            sale.Add("1 music CD at 14.99");
            sale.Add("1 packet of chips at 0.85");

            // Act
            string result = sale.ToString();

            // Assert
            string expected = "1 book: 12.49\n" +
                              "1 music CD: 16.49\n" +
                              "1 packet of chips: 0.85\n" +
                              "Sales Taxes: 1.50\n" +
                              "Total: 29.83";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test2_ImportedSale()
        {
            // Arrange
            Sale sale = new();
            sale.Add("1 imported box of chips at 10.00");
            sale.Add("1 imported transformer at 47.50");

            // Act
            string result = sale.ToString();

            // Assert
            string expected = "1 imported box of chips: 10.50\n" +
                              "1 imported transformer: 54.65\n" +
                              "Sales Taxes: 7.65\n" +
                              "Total: 65.15";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test3_MixedSale()
        {
            // Arrange
            Sale sale = new();
            sale.Add("1 barrell of imported oil at 27.99");
            sale.Add("1 bottle of perfume at 18.99");
            sale.Add("1 packet of headache tablets at 9.75");
            sale.Add("1 box of imported chocolates at 11.25");
            // Act
            string result = sale.ToString();
            // Assert
            string expected = "1 imported barrell of oil: 32.19\n" +
                              "1 bottle of perfume: 20.89\n" +
                              "1 packet of headache tablets: 9.75\n" +
                              "1 imported box of chocolates: 11.85\n" +
                              "Sales Taxes: 6.70\n" +
                              "Total: 74.68";
            Assert.Equal(expected, result);
        }
        [Fact]
        public void Test4_MultipleQuantities()
        {
            // Arrange
            var sale = new Sale();
            sale.Add("10 imported bottles of whiskey at 27.99");
            sale.Add("10 bottles of local whiskey at 18.99");
            sale.Add("10 packets of iodine tablets at 9.75");
            sale.Add("10 boxes of imported potato chips at 11.25");

            // Act
            string result = sale.ToString();

            // Assert
            string expected = "10 imported bottles of whiskey: 321.90\n" +
                              "10 bottles of local whiskey: 208.90\n" +
                              "10 packets of iodine tablets: 97.50\n" +
                              "10 imported boxes of potato chips: 118.15\n" +
                              "Sales Taxes: 66.65\n" +
                              "Total: 746.45";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_InvalidInput()
        {
            // Arrange
            Sale sale = new();
            bool result = sale.Add("js s jss s");

            Assert.False(result, "Should return false for invalid input format");
        }
    }
}
