using AspNetMvcWizardSample.DataAccess.Model;

namespace AspNetMvcWizardSample.DataAccess
{
    public interface IOrdersDataAccess
    {
        int AddOrder(Order newOrder);
        Order? GetOrder(int orderId);
    }
}
