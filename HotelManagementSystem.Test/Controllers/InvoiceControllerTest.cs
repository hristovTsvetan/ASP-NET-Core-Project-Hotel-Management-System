using HotelManagementSystem.Controllers;
using HotelManagementSystem.Models.Invoices;
using HotelManagementSystem.Test.Moq;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelManagementSystem.Test.Controllers
{
    public class InvoiceControllerTest
    {
        [Fact]
        public void ShouldReturnInvoicesRooms()
        {
            MyController<InvoicesController>
                .Instance()
                .WithData(GeneralMocking.GetInvoice())
                .Calling(rooms => rooms.All(With.Default<AllInvoicesQueryModel>()))
                .ShouldReturn()
                .View(v => v.WithModelOfType<AllInvoicesQueryModel>()
                    .Passing(all =>
                    {
                        Assert.Equal(200, all.Invoices.FirstOrDefault().Price);
                        Assert.Equal("TestId", all.Invoices.FirstOrDefault().Id);
                        Assert.True(true, all.Invoices.FirstOrDefault().Paid);
                    }
                ));
        }

        [Fact]
        public void ShouldReturnCorrectInvoiceForDetailsView()
        {
            MyController<InvoicesController>
                .Instance()
                .WithData(GeneralMocking.GetInvoice())
                .Calling(m => m.Details("TestId"))
                .ShouldReturn()
                .View(v => v.WithModelOfType<DetailsInvoiceViewModel>()
                .Passing(inv =>
                {
                    Assert.Equal("Tsvetan reservation", inv.ReservationName);
                    Assert.Equal(200, inv.Price);
                }));
        }

        [Fact]
        public void ShouldRedirectOnDeleteInvoice()
        {
            MyController<InvoicesController>
                .Instance()
                .WithData(GeneralMocking.GetInvoice())
                .Calling(d => d.Delete("TestId"))
                .ShouldReturn()
                .RedirectToAction("All", "Invoices");
        }

        [Fact]
        public static void ShouldRedirectWhenInvoiceIsPaid()
        {
            MyController<InvoicesController>
               .Instance()
               .WithData(GeneralMocking.GetInvoice())
               .Calling(m => m.Pay("TestId"))
               .ShouldReturn()
              .RedirectToAction("All", "Invoices");
        }

        [Fact]
        public void ShoulHaveAuthorizeAttribute()
        {
            MyController<InvoicesController>
                .Instance()
                .ShouldHave()
                .Attributes(attr => attr.RestrictingForAuthorizedRequests());
        }

    }
}
