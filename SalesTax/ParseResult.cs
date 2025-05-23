namespace SalesTax
{
    public class ParseResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public SaleLine SaleLine { get; set; }

        public static ParseResult Ok(SaleLine line) => new ParseResult { Success = true, SaleLine = line };
        public static ParseResult Fail(string message) => new ParseResult { Success = false, ErrorMessage = message };
    }
}
