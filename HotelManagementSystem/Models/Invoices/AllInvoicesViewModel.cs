namespace HotelManagementSystem.Models.Invoices
{
    public class AllInvoicesViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public decimal Price { get; set; }

        public string Paid { get; set; }

        public string IssuedDate { get; set; }
    }
}
