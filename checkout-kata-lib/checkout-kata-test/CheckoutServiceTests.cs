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

        offers = new List<Offer>
        {
            new Offer { Sku = "A", Quantity = 3, SpecialPrice = 130 },
            new Offer { Sku = "B", Quantity = 2, SpecialPrice = 45 }
        };

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
        offers.Clear();
        var service = new CheckoutService(units, offers, checkoutRepository);

        //Act
        service.Scan("A");
        var total = service.GetTotalPrice();

        //Assert
        Assert.That(total, Is.EqualTo(50));
    }

    [Test]
    public void GetTotalPrice_calculate_single_price_with_target_offer()
    {
        //Assemble
        var sku = "A";
        var service = new CheckoutService(units, offers, checkoutRepository);

        //Act
        foreach(var _ in Enumerable.Repeat(0, 3))
        {
            service.Scan(sku);
        }

        var total = service.GetTotalPrice();

        //Assert
        Assert.That(total, Is.EqualTo(130));
    }

    [Test]
    public void GetTotalPrice_calculate_single_price_with_target_offer_and_extra_item()
    {
        //Assemble
        var sku = "A";
        var service = new CheckoutService(units, offers, checkoutRepository);

        //Act
        foreach(var _ in Enumerable.Repeat(0, 4))
        {
            service.Scan(sku);
        }
        var total = service.GetTotalPrice();

        //Assert
        Assert.That(total, Is.EqualTo(180));
    }

    [Test]
    public void GetTotalPrice_calculate_multiple_items_with_multiples_offers()
    {
        //Assemble
        var service = new CheckoutService(units, offers, checkoutRepository);

        //Act
        var items = Enumerable.Repeat(0, 3).Select(_ =>"A").ToList();
        items.AddRange(Enumerable.Repeat(0, 2).Select(_ => "B"));

        foreach(var sku in items)
        {
            service.Scan(sku);
        }
        var total = service.GetTotalPrice();

        //Assert
        Assert.That(total, Is.EqualTo(175));
    }

    [Test]
    public void GetTotalPrice_calculate_offer_nonoffer_items()
    {
        //Assemble
        var service = new CheckoutService(units, offers, checkoutRepository);

        //Act
        var items = Enumerable.Repeat(0, 3).Select(_ =>"A").ToList();
        items.AddRange(Enumerable.Repeat(0, 2).Select(_ => "B"));
        items.AddRange(Enumerable.Repeat(0, 2).Select(_ => "C"));
        items.AddRange(Enumerable.Repeat(0, 2).Select(_ => "D"));

        foreach(var sku in items)
        {
            service.Scan(sku);
        }
        var total = service.GetTotalPrice();

        //Assert
        Assert.That(total, Is.EqualTo(235));
    }
}