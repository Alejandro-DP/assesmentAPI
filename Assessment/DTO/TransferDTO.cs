namespace Assessment.DTO
{
    public class TransferDTO
    {
        public int TransactionId { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
