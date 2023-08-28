using checkout_kata_lib;

namespace checkout_kata_test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Scan_should_accept_valid_item()
    {
        //Assemble
        var sku = "A";
        var service = new CheckoutService();

        //Act
        service.Scan(sku);

        //Assert
    }
}