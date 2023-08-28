namespace checkout_kata_lib;

public class Offer
{
    public HashSet<string> SKUs {get; set;}
    public int Quantity { get; set; }

    public decimal SpecialPrice {get;set;}
}
