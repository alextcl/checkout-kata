using checkout_kata_lib;

namespace checkout_kata_test;

public class CheckoutServiceTests
{
    private List<StockKeepingUnit> units = new List<StockKeepingUnit>();
    private List<Offer> offers = new List<Offer>();

    [SetUp]
    public void Setup()
    {
        units.Add(new StockKeepingUnit{ Identifier = "A", UnitPrice = 50 });
        units.Add(new StockKeepingUnit{ Identifier = "B", UnitPrice = 30 });
        units.Add(new StockKeepingUnit{ Identifier = "C", UnitPrice = 20 });
        units.Add(new StockKeepingUnit{ Identifier = "D", UnitPrice = 10 });
    }

    [Test]
    public void Scan_should_accept_valid_item()
    {
        //Assemble
        var sku = "A";
        var service = new CheckoutService(units, offers);

        //Act
        service.Scan(sku);

        //Assert
    }
}