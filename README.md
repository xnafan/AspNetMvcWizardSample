# Asp.Net Mvc Wizard
Sample ASP.NET MVC page with step-by-step wizard for ordering the company x-mas gift.

<img width="713" alt="image" src="https://user-images.githubusercontent.com/3811290/204812610-8a7e1444-ba20-46ea-b8bb-1f030913ae99.png">

##Architecture
This solution focuses on the step-by-step nature of a wizard (e.g. in creating an order).  
It is basically an ASP.NET MVC Controller class [OrdersController](https://github.com/xnafan/AspNetMvcWizardSample/blob/master/AspNetMvcWizardSample/Controllers/OrdersController.cs), which has an action for each step in the wizard.
Each action has a view, with a form which posts to the next action.

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


