using CommunityToolkit.Diagnostics;

namespace checkout_kata_lib;

public class CheckoutService : ICheckout
{
    private readonly IEnumerable<StockKeepingUnit> stockKeepingUnits;
    private readonly IEnumerable<Offer> offers;
    private readonly IDictionary<string, int> checkoutRepository;

    public CheckoutService(IEnumerable<StockKeepingUnit> stockKeepingUnits, IEnumerable<Offer> offers, IDictionary<string, int> checkoutRepository)
    {
        this.stockKeepingUnits = stockKeepingUnits;
        this.offers = offers;
        this.checkoutRepository = checkoutRepository;
    }

    public int GetTotalPrice()
    {
        var totalPrice = 0;
        foreach(var item in checkoutRepository)
        {
            var sku = stockKeepingUnits.Single(s => s.Identifier == item.Key);
            totalPrice += item.Value * sku.UnitPrice;
        }
        return totalPrice;
    }

    public void Scan(string item)
    {
        Guard.IsNotNullOrEmpty(item);

        var sku = stockKeepingUnits.FirstOrDefault(x => string.Equals(x.Identifier, item, StringComparison.OrdinalIgnoreCase));
        if (sku == null)
            throw new ArgumentException($"Invalid item {item}");

        if(!checkoutRepository.ContainsKey(sku.Identifier))
            checkoutRepository.Add(sku.Identifier, 1);
        else
            checkoutRepository[sku.Identifier]++;
    }
}
