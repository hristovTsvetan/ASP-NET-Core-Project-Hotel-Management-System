using HotelManagementSystem.Models.Invoices;

namespace HotelManagementSystem.Services
{
    public interface IInvoicesService
    {
        AllInvoicesQueryModel All(AllInvoicesQueryModel query);

        void Pay(string id);

        DetailsInvoiceViewModel Details(string id);

        void Delete(string id);
    }
}
