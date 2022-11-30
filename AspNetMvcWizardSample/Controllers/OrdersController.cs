using AspNetMvcWizardSample.DataAccess;
using AspNetMvcWizardSample.DataAccess.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNetMvcWizardSample.Controllers;
public class OrdersController : Controller
{
    private readonly IGiftsDataAccess _giftsData;
    private readonly IOrdersDataAccess _ordersData;
    private readonly IAuthenticationProvider _authenticationProvider;

    public OrdersController(IGiftsDataAccess giftsData, IOrdersDataAccess ordersData, IAuthenticationProvider authenticationProvider)
    {
        _giftsData = giftsData;
        _ordersData = ordersData;
        _authenticationProvider = authenticationProvider;
    }

    //Step 1 of 4 - select the category of gift
    public ActionResult SelectCategory()
    {
        return View(_giftsData.GetCategories());
    }

    //Step 2 of 4 - select the actual gift
    [HttpPost]
    public ActionResult SelectGift(int giftCategoryId)
    {
        return View(_giftsData.GetGifts(giftCategoryId));
    }

    //Step 3 of 4 - select where the gift should be delivered
    [HttpPost]
    public ActionResult SelectDelivery(int giftId)
    {
        var order = new Order();
        order.Gift = _giftsData.GetGift(giftId);
        StoreOrderInTempData(order);
        return View();
    }

    //Step 4 of 4 - save the finished order and display it
    [HttpPost]
    public ActionResult ShowOrder(DeliveryLocation deliveryLocation)
    {
        var order = GetOrderFromTempData();
        order.DeliveryLocation = deliveryLocation;

        //this line is added to emulate getting the current user
        //from an authentication scheme (login)
        //which is not included in this code sample
        order.EmployeeId = _authenticationProvider.GetCurrentUserId();
        _ordersData.AddOrder(order);
        StoreOrderInTempData(order);
        return View(order);
    }

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
}