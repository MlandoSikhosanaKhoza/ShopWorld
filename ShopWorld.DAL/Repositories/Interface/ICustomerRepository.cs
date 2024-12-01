namespace ShopWorld.DAL
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomers();
        List<Customer> SearchForCustomers(string Search);
        bool MobileNumberExists(string Mobile);
        Customer AddCustomer(Customer Customer);
        bool UpdateCustomer(Customer Customer);
        bool DeleteCustomer(int CustomerId);
        Customer GetCustomer(int CustomerId);
        Customer GetCustomerByMobileNumber(string MobileNumber);
        Customer ConfigureCustomer(Customer Customer);
    }
}
