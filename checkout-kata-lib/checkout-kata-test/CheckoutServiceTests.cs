using checkout_kata_lib;

namespace checkout_kata_test;

public class CheckoutServiceTests
{
    private List<StockKeepingUnit> units;
    private List<Offer> offers;
    private IDictionary<string, int> checkoutRepository;

    [SetUp]
    public void Setup()
    {
        units = new List<StockKeepingUnit>
        {
            new StockKeepingUnit { Identifier = "A", UnitPrice = 50 },
            new StockKeepingUnit { Identifier = "B", UnitPrice = 30 },
            new StockKeepingUnit { Identifier = "C", UnitPrice = 20 },
            new StockKeepingUnit { Identifier = "D", UnitPrice = 10 }
        };

        offers = new List<Offer>();

        checkoutRepository = new Dictionary<string, int>();
    }

    [Test]
    public void Scan_should_accept_valid_item()
    {
        //Assemble
        var sku = "A";
        var service = new CheckoutService(units, offers, checkoutRepository);

        //Act
        service.Scan(sku);

        //Assert
        Assert.IsTrue(checkoutRepository.ContainsKey(sku));
        Assert.That(checkoutRepository[sku], Is.EqualTo(1));
    }

    [Test]
    public void Scan_should_reject_invalid_item()
    {
        //Assemble
        var sku = "E";
        var service = new CheckoutService(units, offers, checkoutRepository);

        //Act
        Assert.Throws<ArgumentException>(() => service.Scan(sku));

        //Assert
    }

    [Test]
    public void GetTotalPrice_calculate_single_price_with_no_offer()
    {
        //Assemble
        var service = new CheckoutService(units, offers, checkoutRepository);

        //Act
        service.Scan("A");
        var total = service.GetTotalPrice();

        //Assert
        Assert.AreEqual(50, total);
    }
}