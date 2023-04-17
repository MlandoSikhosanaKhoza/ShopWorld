using ShopWorld.Shared.Entities;
using ShopWorld.Shared;
using ShopWorld.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ShopWorld.BusinessLogic
{
    public class OrderLogic : IOrderLogic
    {
        private GenericRepository<Order> OrderRepository { get; set; }
        private GenericRepository<Customer> CustomerRepository { get; set; }
        private IUnitOfWork _unitOfWork;
        public OrderLogic(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            OrderRepository = UnitOfWork.GetRepository<Order>();
            CustomerRepository = UnitOfWork.GetRepository<Customer>();
        }

        public List<Order> GetAllOrders()
        {
            return OrderRepository.GetAll().ToList();
        }

        public List<Order> GetOngoingOrdersForCustomer(int CustomerId)
        {
            return OrderRepository.Get(o => o.CustomerId == CustomerId && o.DateFulfilled == null && o.EmployeeId == null, includeProperties: $"{nameof(Customer)}").Select(x => new Order
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
            return OrderRepository.Get(o => o.CustomerId == CustomerId && o.DateFulfilled != null && o.EmployeeId != null, includeProperties: $"{nameof(Employee)},{nameof(Customer)}").ToList();
        }

        public List<Order> GetOngoingOrders()
        {
            return OrderRepository.Get(o => o.DateFulfilled == null && o.EmployeeId == null, includeProperties: $"{nameof(Customer)}").ToList();
        }

        public List<Order> GetCompleteOrders()
        {
            return OrderRepository.Get(o => o.DateFulfilled != null && o.EmployeeId != null, includeProperties: $"{nameof(Employee)},{nameof(Customer)}").ToList();
        }

        public List<CustomerOrderResult> GetNumberOfCustomerOrders()
        {
            List<CustomerOrderResult> customerOrderViews = new List<CustomerOrderResult>();
            if (CustomerRepository.Any() && OrderRepository.Any())
            {
                try
                {
                    List<Customer> customers=CustomerRepository.Get().ToList();
                    List<Order> orders=OrderRepository.Get().ToList();
                    var customerOrderQuery = from c in customers
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

        public List<CustomerOrderPriceResult> GetTotalSpentOfCustomerOrders()
        {
            //var totalSpentQuery = OrderRepository.GetAll().GroupBy(o => o.CustomerId).Select(
            //    o => new { CustomerId = o.Key, Price = o.Sum(o => o.GrandTotal) });
            List<CustomerOrderPriceResult> customerOrderPrices = new List<CustomerOrderPriceResult>();
            if (CustomerRepository.Any() && OrderRepository.Any())
            {
                var customerOrderQuery = from c in CustomerRepository.GetAll()
                                         join o in (OrderRepository.GetAll().GroupBy(o => o.CustomerId).Select(
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

        public List<CustomerOrderPriceResult> GetAverageSpentOfCustomerOrders()
        {
            //var totalSpentQuery = OrderRepository.GetAll().GroupBy(o => o.CustomerId).Select(
            //    o => new { CustomerId = o.Key, Price = o.Sum(o => o.GrandTotal) });
            List<CustomerOrderPriceResult> customerOrderPrices = new List<CustomerOrderPriceResult>();
            if (CustomerRepository.Any() && OrderRepository.Any())
            {
                var customerOrderQuery = from c in CustomerRepository.GetAll()
                                         join o in (OrderRepository.GetAll().GroupBy(o => o.CustomerId).Select(
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
            Order OrderAdded = OrderRepository.Insert(Order);
            _unitOfWork.SaveChanges();
            return OrderAdded;
        }

        public Order GetOrder(int OrderId)
        {
            return OrderRepository.GetById(OrderId);
        }

        public bool UpdateOrder(Order Order)
        {
            OrderRepository.Update(Order);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteOrder(int OrderId)
        {
            OrderRepository.DeleteById(OrderId);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
