using HotelManagementSystem.Controllers;
using HotelManagementSystem.Models.Vouchers;
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
    public class VouchersControllerTest
    {
        [Fact]
        public void ShouldReturnAllRoomTypes()
        {
            MyController<VouchersController>
                .Instance()
                .WithData(GeneralMocking.GetVoucher())
                .Calling(m => m.All())
                .ShouldReturn()
                .View(v => v.WithModelOfType<IEnumerable<ListAllVouchersViewModel>>()
                    .Passing(m => 
                    {
                        Assert.Equal("HappyBirthDay", m.FirstOrDefault().Name);
                        Assert.Equal(10, m.FirstOrDefault().Discount);
                        
                    })
                );
        }

        [Fact]
        public void ShoulHaveAuthorizeAttribute()
        {
            MyController<VouchersController>
                .Instance()
                .ShouldHave()
                .Attributes(attr => attr.RestrictingForAuthorizedRequests());

        }

        [Fact]
        public void ShouldLoadVoucherNameAndDiscountWhenEdit()
        {
            MyController<VouchersController>
                .Instance()
                .WithData(GeneralMocking.GetVoucher())
                .Calling(m => m.Edit("TestId"))
                .ShouldReturn()
                .View(v => v.WithModelOfType<EditVoucherFormModel>()
                .Passing(v => 
                {
                    Assert.Equal("HappyBirthDay", v.Name);
                    Assert.Equal(10, v.Discount);
                }
                ));
        }

        [Fact]
        public void ShouldRedirectWhenRoomTypeIsEdit()
        {
            MyController<VouchersController>
                .Instance()
                .WithData(GeneralMocking.GetVoucher())
                .Calling(m => m.Edit(GeneralMocking.EditVoucher()))
                .ShouldReturn()
                .RedirectToAction("All", "Vouchers");
        }

        [Fact]
        public void ShouldReturnViewOnAdd()
        {
            MyController<VouchersController>
                .Instance()
                .Calling(m => m.Add())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ShouldRedirectOnAddVoucher()
        {
            MyController<VouchersController>
                .Instance()
                .WithData(GeneralMocking.GetVoucher())
                .Calling(m => m.Add(GeneralMocking.AddVoucher()))
                .ShouldReturn()
                .RedirectToAction("All", "Vouchers");
        }

        [Fact]
        public void ShouldRedirectOnDeleteVoucher()
        {
            MyController<VouchersController>
                .Instance()
                .WithData(GeneralMocking.GetVoucher())
                .Calling(d => d.Delete("TestId"))
                .ShouldReturn()
                .RedirectToAction("All", "Vouchers");
        }
    }
}
