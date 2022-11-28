using AspNetMvcWizardSample.DataAccess.Model;

namespace AspNetMvcWizardSample.DataAccess;
public class InMemoryOrderDataAccess : IOrdersDataAccess
{
    private readonly List<Order> _orders = new();

    public int AddOrder(Order newOrder)
    {
        newOrder.Id = _orders.Select(o => o.Id).DefaultIfEmpty().Max() + 1;
        newOrder.TimeCreated= DateTime.Now;
        _orders.Add(newOrder);
        return newOrder.Id;
    }

    public Order? GetOrder(int orderId) => _orders.FirstOrDefault(o => o.Id == orderId);
}