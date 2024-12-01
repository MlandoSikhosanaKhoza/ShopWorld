
using ShopWorld.Shared;
using ShopWorld.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ShopWorld.Shared.Models;
using AutoMapper;
namespace ShopWorld.BusinessLogic
{
    public class CustomerLogic:ICustomerLogic
    {
        private ICustomerRepository _customerRepository { get; set; }
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public CustomerLogic(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public IEnumerable<CustomerModel> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers().Select(_mapper.Map<CustomerModel>);
        }

        public IEnumerable<CustomerModel> SearchForCustomers(string Search)
        {
            return _customerRepository.SearchForCustomers(Search).Select(_mapper.Map<CustomerModel>);
        }
        public bool MobileNumberExists(string Mobile)
        {
            return _customerRepository.MobileNumberExists( Mobile);
        }
        public CustomerModel AddCustomer(CustomerModel Customer)
        {
            Customer customer = _mapper.Map<Customer>(Customer);
            _customerRepository.AddCustomer(customer);
            return _mapper.Map<CustomerModel>(customer);
        }
        public bool UpdateCustomer(CustomerModel Customer)
        {
            Customer customer = _customerRepository.GetCustomer(Customer.CustomerId);

            if (customer == null)
            {
                return false;
            }

            _mapper.Map(Customer, customer);
            _customerRepository.UpdateCustomer(customer);

            return true;
        }
        public bool DeleteCustomer(int CustomerId)
        {
            return _customerRepository.DeleteCustomer(CustomerId);
        }
        public CustomerModel GetCustomer(int CustomerId)
        {
            Customer customer = _customerRepository.GetCustomer(CustomerId);
            return _mapper.Map<CustomerModel>(customer);
        }

        public CustomerModel GetCustomerByMobileNumber(string MobileNumber)
        {
            Customer customer = _customerRepository.GetCustomerByMobileNumber(MobileNumber);
            return _mapper.Map<CustomerModel>(customer);
        }

        public CustomerModel ConfigureCustomer(CustomerModel CustomerModel)
        {
            Customer customer = _mapper.Map<Customer>(CustomerModel);
            customer = _customerRepository.ConfigureCustomer(customer);
            return _mapper.Map(customer,CustomerModel);
        }
    }
}
