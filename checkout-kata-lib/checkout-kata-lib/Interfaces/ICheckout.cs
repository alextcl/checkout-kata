namespace checkout_kata_lib;

public interface ICheckout
{
    void Scan(string item);
    int GetTotalPrice();
}
