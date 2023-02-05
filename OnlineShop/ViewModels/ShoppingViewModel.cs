namespace OnlineShop.ViewModels
{
    public class ShoppingViewModel
    {
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Count { get; set; }

        public string? Des { get; set; }
    }
}
