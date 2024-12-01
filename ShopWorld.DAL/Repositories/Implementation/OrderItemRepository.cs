using ShopWorld.Shared;

namespace ShopWorld.DAL
{
    public class OrderItemRepository:IOrderItemRepository
    {
        private GenericRepository<OrderItem> _orderItemRepository { get; set; }

        private IUnitOfWork _unitOfWork;
        public OrderItemRepository(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            _orderItemRepository = UnitOfWork.GetRepository<OrderItem>();
        }

        public List<OrderItem> GetAllOrderItems()
        {
            return _orderItemRepository.GetAll().ToList();
        }

        public List<OrderItemResult> GetOrderViewItems(int OrderId)
        {
            return _orderItemRepository.Get(oi => oi.OrderId == OrderId, includeProperties: $"{nameof(Item)}").Select(oi => new OrderItemResult
            {
                OrderItemId = oi.OrderItemId,
                Description = oi.Item.Description,
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList();
        }

        public OrderItem AddOrderItem(OrderItem OrderItem)
        {
            OrderItem orderItemAdded = _orderItemRepository.Insert(OrderItem);
            _unitOfWork.SaveChanges();
            return orderItemAdded;
        }

        public OrderItem GetOrderItem(int OrderItemId)
        {
            return _orderItemRepository.GetById(OrderItemId);
        }

        public bool UpdateOrderItem(OrderItem OrderItem)
        {
            _orderItemRepository.Update(OrderItem);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteOrderItem(int OrderItemId)
        {
            _orderItemRepository.DeleteById(OrderItemId);
            _unitOfWork.SaveChanges();
            return true;
        }

        public List<OrderItem> AddOrderItems(int OrderId, int[] ItemId, int[] Quantity, decimal[] Price)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            for (int i = 0; i < ItemId.Length; i++)
            {
                OrderItem orderItem = _orderItemRepository.Insert(new OrderItem { OrderId = OrderId, ItemId = ItemId[i], Quantity = Quantity[i], Price = Price[i] });
                orderItems.Add(orderItem);
            }
            _unitOfWork.SaveChanges();
            return orderItems;
        }
    }
}
