using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Services.Interfaces
{
    public interface ICardService
    {
        Task<CardDetail> AddNewCardForUser(CardRequestModel cardRequestModel);
        Task<List<CardDetail>> GetCardDetailsForUser(Guid userId);
    }
}
