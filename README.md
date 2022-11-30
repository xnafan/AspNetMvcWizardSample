# Asp.Net Mvc Wizard
Sample ASP.NET MVC page with step-by-step wizard for ordering the company x-mas gift.

<img width="713" alt="image" src="https://user-images.githubusercontent.com/3811290/204812610-8a7e1444-ba20-46ea-b8bb-1f030913ae99.png">

#Basic functionality
This solution focuses on the step-by-step nature of a wizard (e.g. in creating an order).  
It is basically an ASP.NET MVC Controller class [OrdersController](https://github.com/xnafan/AspNetMvcWizardSample/blob/master/AspNetMvcWizardSample/Controllers/OrdersController.cs), which has an action for each step in the wizard.
Each action has a view, with a form which posts the user's selections to the next action (i.e. gift category, gift, place of delivery):

```cs
//NOTE: only selected actions of the controller are shown here
public class OrdersController : Controller
{
    //Step 1 of 4 - select the category of gift
    public ActionResult SelectCategory() {...}

    //Step 2 of 4 - select the actual gift
    [HttpPost]
    public ActionResult SelectGift(int giftCategoryId){...}

    //Step 3 of 4 - select where the gift should be delivered
    [HttpPost]
    public ActionResult SelectDelivery(int giftId) {...}

    //Step 4 of 4 - save the finished order and display it
    [HttpPost]
    public ActionResult ShowOrder(DeliveryLocation deliveryLocation) {...}
}
```
To enable the order object (created in the SelectDelivery() method) to be sent to the final action (ShowOrder()), the Order object is stored in and retrieved from TempData[] using two helpermethods:

```cs
 #region Helpermethods

//////////// HELPER METHODS /////////////////////////////////
//The following two methods are helpermethods
//to allow for the storage and retrieval of an Order object
//between actions on the controller
//////////////////////////////////////////////////////////
private void StoreOrderInTempData(Order order)
{
    TempData["Order"] = JsonConvert.SerializeObject(order);
}
private Order GetOrderFromTempData()
{
    return JsonConvert.DeserializeObject<Order>((string)TempData["Order"]);
}
////////////////////////////////////////////////////////// 
#endregion
```
**Note** that it is perfectly fine to store an object in TempData through the entire wizard flow. It is just not necessary in the four steps above, as the only thing sent from the `SelectCategory` action is the category id and the only thing sent from the `SelectGift`action is the gift id.

#Architecture
This project uses Dependency Injection to enable low coupling between components.
This means that the `OrdersController` receives the components it is dependent on in its constructor:

```cs
private readonly IGiftsDataAccess _giftsData;
private readonly IOrdersDataAccess _ordersData;
private readonly IAuthenticationProvider _authenticationProvider;

public OrdersController(IGiftsDataAccess giftsData, IOrdersDataAccess ordersData, IAuthenticationProvider authenticationProvider)
{
    _giftsData = giftsData;
    _ordersData = ordersData;
    _authenticationProvider = authenticationProvider;
}
```
The [IGiftsDataAccess](https://github.com/xnafan/AspNetMvcWizardSample/blob/master/AspNetMvcWizardSample/DataAccess/IGiftsDataAccess.cs) interface defines the needed functionality for the controller to be able to 
* get all gift categories 
* get gifts within a specific category
* get a gift from its id
This is implemented as an in-memory store, but could easily use a database.

The the [IOrdersDataAccess](https://github.com/xnafan/AspNetMvcWizardSample/blob/master/AspNetMvcWizardSample/DataAccess/IOrdersDataAccess.cs) interface defines the needed functionality for the controller to be able to
* add a new order
* get an order by its id
This is implemented as an in-memory store, but could easily use a database.

The [IAuthenticationProvider](https://github.com/xnafan/AspNetMvcWizardSample/blob/master/AspNetMvcWizardSample/DataAccess/IAuthenticationProvider.cs) interface defines the needed functionality for the controller to be able to
* retrieve the current user
This is implemented as a stub, which just returns 42, but could be hooked up to ASP.NET Authentication.
