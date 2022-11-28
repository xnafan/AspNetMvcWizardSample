namespace AspNetMvcWizardSample.DataAccess.Model;
public class Order
{
    public int Id { get; set; }
    public DateTime TimeCreated { get; set; }
    public Gift Gift { get; set; }
    public int EmployeeId { get; set; }
    public DeliveryLocation DeliveryLocation { get; set; }
}