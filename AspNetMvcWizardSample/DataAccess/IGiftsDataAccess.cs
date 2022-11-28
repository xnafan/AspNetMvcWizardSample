using AspNetMvcWizardSample.DataAccess.Model;

namespace AspNetMvcWizardSample.DataAccess
{
    public interface IGiftsDataAccess
    {
        IEnumerable<GiftCategory> GetCategories();
        IEnumerable<Gift> GetGifts(int categoryId);
        Gift? GetGift(int giftId);
    }
}
