namespace Domain.Entities
{
    public class Order : EntityBase
    {
        public double OrderID { get; set; }
        public string OrderDate { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string ProductID { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public double Sales { get; set; }
        public double Discount { get; set; }
        public double Profit { get; set; }

    }
}
