using System.Collections.Immutable;
using System.Xml.Linq;
using AspNetMvcWizardSample.DataAccess.Model;

namespace AspNetMvcWizardSample.DataAccess;
public class HardcodedGiftsDataAcess : IGiftsDataAccess
{

    private readonly List<GiftCategory> _categories = new List<GiftCategory>()
    {
new GiftCategory(){Id=1, Name="Candy"},new GiftCategory(){Id=2, Name="Wearables"},new GiftCategory(){Id=3, Name="Household items"},new GiftCategory(){Id=4, Name="Electronics"},
    };

    private readonly Dictionary<int, List<Gift>> _giftsByCategory = new Dictionary<int, List<Gift>>()
    {
        {1, new List<Gift>() { 
        new Gift(){Id = 11, Name="Dark coding chocolates", Description="description, description, description, description, description, description, description, " },
        new Gift(){Id = 12, Name="Programmers Energy Drink '!Boozta!'", Description="description, description, description, description, description, description, description, " },
        new Gift(){Id = 13, Name="Debugging Edibles", Description="description, description, description, description, description, description, description, " },
        } },
        {2, new List<Gift>() {
        new Gift(){Id = 21, Name="Visual Studio Beanie", Description="description, description, description, description, description, description, description, " },
        new Gift(){Id = 22, Name="'Java is my type of C#affeine' T-shirt", Description="description, description, description, description, description, description, description, " },
        new Gift(){Id = 23, Name="'C# is for winners!' hoodie", Description="description, description, description, description, description, description, description, " },
        } },
        {3, new List<Gift>() {
        new Gift(){Id = 31, Name="Navy blue Microsoft fireplace rug", Description="description, description, description, description, description, description, description, " },
        new Gift(){Id = 32, Name="Sturdy mug with text: 'I code because I can!'", Description="description, description, description, description, description, description, description, " },
        new Gift(){Id = 33, Name="Limited edition Eclipse Tupperware", Description="description, description, description, description, description, description, description, " },
        } },
        {4, new List<Gift>() {
        new Gift(){Id = 41, Name="Visual Studio headphones, purple", Description="description, description, description, description, description, description, description, " },
        new Gift(){Id = 42, Name="Microsoft 25 yr anniversary keyboard", Description="description, description, description, description, description, description, description, " },
        new Gift(){Id = 43, Name="USB mouse with programmable LED panel", Description="description, description, description, description, description, description, description, " },
        } }
    };
    public IEnumerable<GiftCategory> GetCategories() => _categories;

    public Gift? GetGift(int giftId)
    {
        var categoryWithGift = _giftsByCategory.Values.FirstOrDefault(giftList => giftList.Any(gift => gift.Id == giftId));
        return categoryWithGift?.FirstOrDefault(gift => gift.Id == giftId);
    }
    public IEnumerable<Gift> GetGifts(int categoryId) => _giftsByCategory[categoryId];
}