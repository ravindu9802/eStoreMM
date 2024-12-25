namespace Orders.Domain.Enums;

public enum OrderStatus
{
    Pending = 0,
    Processing = 1,
    ReadyToShip = 2,
    Shipped = 3,
    Delivered = 4,
    Returned = 5,
    Cancelled = 6,
    Completed = 7
}