namespace SalesTax
{
    // THIS IS NOT THREAD SAFE (or localised)
    public static class InputParser
    {

        // Assumes that all input is in the format:
        //  <qty> <product> at <price>
        //
        //  If <product> contains the word imported then the product is deemed to attract import tax
        //
        // If it can't be parsed we return null.
        // If it can then we return a sales line, complete with tax information calculated.
        public static ParseResult ProcessInput(string input)
        {
            int quantity;
            string productName;
            decimal price;
            bool isImported;
            SaleLine saleLine;

            if (string.IsNullOrEmpty(input))
                return ParseResult.Fail("Input is empty.");

            string[] words = input.Split(' ');
            int wordCount = words.Length;

            // must have at least 4 words
            if (wordCount < 4)
                return ParseResult.Fail("Input is too short. Must include quantity, product name, and price.");

            // get quantity (first word)
            if (!int.TryParse(words[0], out quantity))
                return ParseResult.Fail("Quantity is not a valid integer.");


            // get price (last word in input string)
            if (!decimal.TryParse(words[wordCount - 1], out price))
                return ParseResult.Fail("Price is not a valid decimal.");

            productName = string.Join(" ", words, 1, wordCount - 3).Trim();
            if (string.IsNullOrEmpty(productName))
                return ParseResult.Fail("Product name could not be determined.");

            //Check if this is an imported product
            isImported = productName.Contains("imported ");
            if (isImported)
            {
                //Ensure the word imported appears at the front of the description
                productName = "imported " + productName.Replace("imported ", string.Empty);
            }

            // create the sale line
            saleLine = new SaleLine(quantity, productName, price, isImported);
            return ParseResult.Ok(saleLine);
        }

    }
}
