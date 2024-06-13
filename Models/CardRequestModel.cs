namespace ApplicationToSellThings.BlazorUI.Models
{
    public class CardRequestModel
    {
        public string UserId { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Cvv { get; set; }
    }
}
