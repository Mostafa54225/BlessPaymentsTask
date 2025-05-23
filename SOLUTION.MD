
## InputParser.cs

### Summary of Changes
To improve clarity in error handling and eliminate the use of `null`, I introduced a new class called `ParseResult`. Instead of returning `null` when input parsing fails, `InputParser` now returns a structured `ParseResult` object that clearly indicates whether parsing succeeded or failed.


now the InputParser now returns a ParseResult object that clearly indicates whether parsing succeeded or failed.

example of the usage of `ParseResult`
```csharp
if (!int.TryParse(words[0], out quantity))
    return ParseResult.Fail("Quantity is not a valid integer.");

```

This approach makes the code easier to maintain, improves the user experience with detailed feedback, and avoids the risks of null references throughout the application.



first mistake int input parser file 
that the condition of counting the number that task want the number of words at least 4 

```csharp
if (wordCount > 4)
	return null; // this is wrong because we have more than 4 words in the input
```
this is wrong because we have more than 4 words in the input
this should be
```csharp
if (wordCount < 4)
    return null;
```


another problem in the input parser file
is the try catch blocks
Parsing errors were handled via exceptions (try/catch), which is more costly in performance and not ideal for input validation.

Replaced exception-based validation (try/catch) with safer and cleaner TryParse() methods for both int and decimal.


also another problem in the input parser file is the extracting the product name
Product name extraction was incorrect: it attempted to concatenate all words from index 1 to the end, leading to malformed product names like "imported box of chocolates at".

Fixed the logic for extracting the product name by skipping the first word (quantity), and excluding "at" and the price.



## SaleLine.cs

### Summary of Changes
The `SaleLine` class was refactored to improve **tax logic accuracy**, **code extensibility**, and **readability**.  
The primary goal was to fix incorrect tax classification and make the code flexible for future enhancements.

**Bug:**
```csharp
if (productName.Contains("book") || productName.Contains("tablet") || productName.Contains("chip"))
    taxRate = 0;
else
    taxRate = 10;

if (isImported)
    taxRate = 5;
```

i have made three functions to use it instead of the above code
for medical items, food items, and books

this helped me to make the code more readble and easy to maintain

and there was an issuse related to the taxRate it was
```csharp
if (isImported)
	taxRate = 5;
```
this was wrong it make the taxRate always 5 we need to add the taxRate to the prevoips tax rate if the product is imported


## Sale.cs
### Summary of Changes
The `Sale` class was refactored to improve **code readability**, **error handling**, and **output formatting**.

**Bug:**

1- The original Add() method blindly added parsed lines without validation, risking NullReferenceException.

2- The saleLines list wasn't initialized in the constructor, potentially causing crashes.

**Solution:**

What I have made is added initialization of `saleLines` in constructor.

Implemented robust error handling using the new `ParseResult` structure.

Only process valid inputs that pass parsing validation.