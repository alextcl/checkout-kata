using CommunityToolkit.Diagnostics;

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
        Guard.IsNotNullOrEmpty(item);

        var sku = stockKeepingUnits.FirstOrDefault(x => string.Equals(x.Identifier, item, StringComparison.OrdinalIgnoreCase));
        if (sku == null)
            throw new ArgumentException($"Invalid item {item}");

        throw new NotImplementedException();
    }
}
