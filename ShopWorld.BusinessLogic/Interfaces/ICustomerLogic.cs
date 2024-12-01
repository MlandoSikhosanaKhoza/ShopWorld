using ShopWorld.Shared.Models;
namespace ShopWorld.BusinessLogic
{
    public interface ICustomerLogic
    {
        IEnumerable<CustomerModel> GetAllCustomers();
        CustomerModel AddCustomer(CustomerModel Customer);
        CustomerModel ConfigureCustomer(CustomerModel Customer);
        CustomerModel GetCustomer(int CustomerId);
        CustomerModel GetCustomerByMobileNumber(string MobileNumber);
        bool UpdateCustomer(CustomerModel Customer);
        bool DeleteCustomer(int CustomerId);
        IEnumerable<CustomerModel> SearchForCustomers(string Search);
        bool MobileNumberExists(string Mobile);
    }
}
