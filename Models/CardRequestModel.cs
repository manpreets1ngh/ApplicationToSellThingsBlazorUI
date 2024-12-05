using System.ComponentModel.DataAnnotations;

namespace ApplicationToSellThings.BlazorUI.Models
{
    public class CardRequestModel
    {
        public CardRequestModel()
        {
            _cardNumber = string.Empty; // Initialize to an empty string
        }

        public string UserId { get; set; }

        [Required(ErrorMessage = "Cardholder name is required.")]
        public string CardHolderName { get; set; }

        private string _cardNumber = string.Empty;

        [Required(ErrorMessage = "Card number is required.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be exactly 16 digits.")]
        public string CardNumber
        {
            get => _cardNumber.Replace(" ", string.Empty); // Remove spaces for validation
            set => _cardNumber = value;
        }

        [Required(ErrorMessage = "Expiry date is required.")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVV is required.")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "Invalid CVV.")]
        public int Cvv { get; set; }


    }
}
