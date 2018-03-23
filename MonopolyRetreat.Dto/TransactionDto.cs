namespace MonopolyRetreat.Dto
{
    public enum TransactionType
    {
        Buy = 1,
        Sell = 2
    }
    public class TransactionDto
    {
        public string Name { get; set; }
        public PropertyDto Property { get; set; }
        public long Value { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}