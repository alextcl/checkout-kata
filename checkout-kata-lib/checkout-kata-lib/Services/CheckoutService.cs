namespace checkout_kata_lib;

public class CheckoutService : ICheckout
{
    private readonly IEnumerable<StockKeepingUnit> stockKeepingUnits;
    private readonly IEnumerable<Offer> offers;

    private readonly IDictionary<string, int> unitQuantity;

    public CheckoutService(IEnumerable<StockKeepingUnit> stockKeepingUnits, IEnumerable<Offer> offers)
    {
        this.stockKeepingUnits = stockKeepingUnits;
        this.offers = offers;
    }

    public int GetTotalPrice()
    {
        throw new NotImplementedException();
    }

    public void Scan(string item)
    {
        throw new NotImplementedException();
    }
}
