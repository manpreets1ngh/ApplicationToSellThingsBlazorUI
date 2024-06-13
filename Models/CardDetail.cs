namespace ApplicationToSellThings.BlazorUI.Models
{
    public class CardDetail
    {
        public Guid CardId { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Cvv { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
