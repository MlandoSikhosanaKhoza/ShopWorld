using ShopWorld.Shared.Entities;
using ShopWorld.Shared;
using ShopWorld.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ShopWorld.BusinessLogic
{
    public class CustomerLogic:ICustomerLogic
    {
        private GenericRepository<Customer> CustomerRepository { get; set; }
        private IUnitOfWork _unitOfWork;
        public CustomerLogic(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            CustomerRepository = UnitOfWork.GetRepository<Customer>();
        }
        public List<Customer> GetAllCustomers()
        {
            return CustomerRepository.GetAll().ToList();
        }

        public List<Customer> SearchForCustomers(string Search)
        {
            Search = Search.ToLower();
            return (CustomerRepository.Get(s => s.Name.ToLower().Contains(Search) ||
                 s.Surname.ToLower().Contains(Search))).ToList();
        }
        public bool MobileNumberExists(string Mobile)
        {
            if (string.IsNullOrEmpty(Mobile))
            {
                return false;
            }
            return CustomerRepository.Get(u => u.Mobile.Equals(Mobile)).Any();
        }
        public Customer AddCustomer(Customer Customer)
        {
            Customer customer=CustomerRepository.Insert(Customer);
            _unitOfWork.SaveChanges();
            return customer;
        }
        public bool UpdateCustomer(Customer Customer)
        {
            CustomerRepository.Update(Customer);
            _unitOfWork.SaveChanges();
            return true;
        }
        public bool DeleteCustomer(int CustomerId)
        {
            CustomerRepository.DeleteById(CustomerId);
            _unitOfWork.SaveChanges();
            return true;
        }
        public Customer GetCustomer(int CustomerId)
        {
            return CustomerRepository.GetById(CustomerId);
        }

        public Customer GetCustomerByMobileNumber(string MobileNumber)
        {
            Customer customer = CustomerRepository.Get(c => c.Mobile.Equals(MobileNumber)).FirstOrDefault();
            return customer;
        }

        public Customer ConfigureCustomer(Customer Customer)
        {
            Customer customer = GetCustomerByMobileNumber(Customer.Mobile);
            if (customer==null)
            {
                customer = AddCustomer(Customer);
            }
            else
            {
                customer.Name = Customer.Name;
                customer.Surname = Customer.Surname;
                UpdateCustomer(customer);
            }
            return customer;
        }
    }
}
