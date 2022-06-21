namespace Microsoft.eShopWeb.Web.Pages.Basket;

public class OrderModel
{
    public string UserName { get; set; }
    public int CardId { get; set; }
    public decimal Price { get; set; }
    public CreditCardModel CreditCard { get; set; }
    private DateTime date;
    public DateTime Date
    {
        set
        {
            date = DateTime.Now.Date;
        }
        get => date;
    }
}
