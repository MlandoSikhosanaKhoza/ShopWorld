namespace ShopWorld.DAL
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Customer>();
        }

        public List<Customer> GetAllCustomers()
        {
            return _repository.GetAll().ToList();
        }

        public List<Customer> SearchForCustomers(string Search)
        {
            Search = Search.ToLower();
            return (_repository.Get(s => s.Name.ToLower().Contains(Search) ||
                 s.Surname.ToLower().Contains(Search))).ToList();
        }
        public bool MobileNumberExists(string Mobile)
        {
            if (string.IsNullOrEmpty(Mobile))
            {
                return false;
            }
            return _repository.Get(u => u.Mobile.Equals(Mobile)).Any();
        }
        public Customer AddCustomer(Customer Customer)
        {
            Customer customer = _repository.Insert(Customer);
            _unitOfWork.SaveChanges();
            return customer;
        }
        public bool UpdateCustomer(Customer Customer)
        {
            _repository.Update(Customer);
            _unitOfWork.SaveChanges();
            return true;
        }
        public bool DeleteCustomer(int CustomerId)
        {
            _repository.DeleteById(CustomerId);
            _unitOfWork.SaveChanges();
            return true;
        }
        public Customer GetCustomer(int CustomerId)
        {
            return _repository.GetById(CustomerId);
        }

        public Customer GetCustomerByMobileNumber(string MobileNumber)
        {
            Customer? customer = _repository.Get(c => c.Mobile.Equals(MobileNumber)).FirstOrDefault();
            return customer;
        }

        public Customer ConfigureCustomer(Customer Customer)
        {
            Customer customer = GetCustomerByMobileNumber(Customer.Mobile);
            if (customer == null)
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
