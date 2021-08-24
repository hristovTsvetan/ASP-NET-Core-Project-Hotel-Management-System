using HotelManagementSystem.Areas.Admin.Controllers;
using HotelManagementSystem.Areas.Admin.Models.Company;
using HotelManagementSystem.Test.Moq;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace HotelManagementSystem.Test.Controllers
{
    public class CompanyControllerTest
    {
        [Fact]
        public void ShoulHaveAuthorizeAttribute()
        {
            MyController<CompanyController>
                .Instance()
                .ShouldHave()
                .Attributes(attr =>
                    attr.RestrictingForAuthorizedRequests()
                );
        }

        [Fact]
        public void ShouldReturnCorrectCompanyForCompanyInfo()
        {
            MyController<CompanyController>
              .Instance()
              .WithData(GeneralMocking.GetCompany())
              .Calling(m => m.CompanyInfo())
              .ShouldReturn()
              .View(v => v.WithModelOfType<CompanyFormModel>()
              .Passing(c =>
              {
                  Assert.Equal("My company", c.Name);
                  Assert.Equal("Po Box 101", c.Address);
                  Assert.Equal("company@company.bg", c.Email);
                  Assert.Equal("53326345345", c.Phone);
              }));
        }

        [Fact]
        public void ShouldRedirectWhenChangeCompanyWhenEdit()
        {
            MyController<CompanyController>
                .Instance()
                .WithData(GeneralMocking.GetCompany())
                .Calling(m => m.CompanyInfo(GeneralMocking.CompanyFormModel()))
                .ShouldReturn()
                .View(v => v.WithModelOfType<CompanyFormModel>()
                  .Passing(c =>
                  {
                      Assert.Equal("My company", c.Name);
                      Assert.Equal("Po Box 101", c.Address);
                      Assert.Equal("company@company.bg", c.Email);
                      Assert.Equal("53326345345", c.Phone);
                  }));
        }
    }
}
