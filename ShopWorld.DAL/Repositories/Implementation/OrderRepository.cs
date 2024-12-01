using ShopWorld.Shared;

namespace ShopWorld.DAL
{
    public class OrderRepository:IOrderRepository
    {
        private readonly IGenericRepository<Order> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return _repository.GetAll().ToList();
        }

        public List<Order> GetOngoingOrdersForCustomer(int CustomerId)
        {
            return _repository.Get(o => o.CustomerId == CustomerId && o.DateFulfilled == null && o.EmployeeId == null, includeProperties: $"{nameof(Customer)}").Select(x => new Order
            {
                OrderId = x.OrderId,
                CustomerId = x.CustomerId,
                EmployeeId = x.EmployeeId,
                Employee = x.Employee,
                DateFulfilled = x.DateFulfilled,
                DateCreated = x.DateCreated,
                OrderReference = x.OrderReference,
                VAT = x.VAT,
                Subtotal = x.Subtotal,
                GrandTotal = x.GrandTotal
            }).ToList();
        }

        public List<Order> GetCompleteOrdersForCustomer(int CustomerId)
        {
            return _repository.Get(o => o.CustomerId == CustomerId && o.DateFulfilled != null && o.EmployeeId != null, includeProperties: $"{nameof(Employee)},{nameof(Customer)}").ToList();
        }

        public List<Order> GetOngoingOrders()
        {
            return _repository.Get(o => o.DateFulfilled == null && o.EmployeeId == null, includeProperties: $"{nameof(Customer)}").ToList();
        }

        public List<Order> GetCompleteOrders()
        {
            return _repository.Get(o => o.DateFulfilled != null && o.EmployeeId != null, includeProperties: $"{nameof(Employee)},{nameof(Customer)}").ToList();
        }

        public List<CustomerOrderResult> GetNumberOfCustomerOrders(List<Customer> Customers)
        {
            List<CustomerOrderResult> customerOrderViews = new List<CustomerOrderResult>();
            if (Customers!=null && Customers.Any() && _repository.Any())
            {
                try
                {
                    List<Order> orders = _repository.Get().ToList();
                    var customerOrderQuery = from c in Customers
                                             join o in (orders.GroupBy(or => or.CustomerId).Select(n => new { n.Key, Count = n.Count() }))
                                             on c.CustomerId equals o.Key
                                             select new CustomerOrderResult
                                             {
                                                 CustomerId = c.CustomerId,
                                                 Name = c.Name,
                                                 Surname = c.Surname,
                                                 Mobile = c.Mobile,
                                                 NumOfOrders = o.Count
                                             };
                    customerOrderViews = customerOrderQuery.ToList();
                }
                catch (Exception)
                {
                }
            }
            return customerOrderViews;
        }

        public List<CustomerOrderPriceResult> GetTotalSpentOfCustomerOrders(List<Customer> Customers)
        {
            //var totalSpentQuery = _repository.GetAll().GroupBy(o => o.CustomerId).Select(
            //    o => new { CustomerId = o.Key, Price = o.Sum(o => o.GrandTotal) });
            List<CustomerOrderPriceResult> customerOrderPrices = new List<CustomerOrderPriceResult>();
            if (Customers!=null && Customers.Any() && _repository.Any())
            {
                var customerOrderQuery = from c in Customers
                                         join o in (_repository.GetAll().GroupBy(o => o.CustomerId).Select(
                    o => new { CustomerId = o.Key, Price = o.Sum(o => o.GrandTotal) }))
                                         on c.CustomerId equals o.CustomerId
                                         select new CustomerOrderPriceResult
                                         {
                                             CustomerId = c.CustomerId,
                                             Name = c.Name,
                                             Surname = c.Surname,
                                             Mobile = c.Mobile,
                                             Price = o.Price
                                         };
                customerOrderPrices = customerOrderQuery.ToList();
            }

            return customerOrderPrices;
        }

        public List<CustomerOrderPriceResult> GetAverageSpentOfCustomerOrders(List<Customer> Customers)
        {
            List<CustomerOrderPriceResult> customerOrderPrices = new List<CustomerOrderPriceResult>();
            if (Customers != null && Customers.Any() && _repository.Any())
            {
                var customerOrderQuery = from c in Customers
                                         join o in (_repository.GetAll().GroupBy(o => o.CustomerId).Select(
                    o => new { CustomerId = o.Key, Price = o.Average(o => o.GrandTotal) }))
                                         on c.CustomerId equals o.CustomerId
                                         select new CustomerOrderPriceResult
                                         {
                                             CustomerId = c.CustomerId,
                                             Name = c.Name,
                                             Surname = c.Surname,
                                             Mobile = c.Mobile,
                                             Price = o.Price
                                         };
                customerOrderPrices = customerOrderQuery.ToList();
            }

            return customerOrderPrices;
        }

        public Order AddOrder(Order Order)
        {
            Order OrderAdded = _repository.Insert(Order);
            _unitOfWork.SaveChanges();
            return OrderAdded;
        }

        public Order GetOrder(int OrderId)
        {
            return _repository.GetById(OrderId);
        }

        public bool UpdateOrder(Order Order)
        {
            _repository.Update(Order);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteOrder(int OrderId)
        {
            _repository.DeleteById(OrderId);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
