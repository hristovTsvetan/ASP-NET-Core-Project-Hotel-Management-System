using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Validators.Messages
{
    public static class ValidatorConstants
    {
        public const string validateRankNameErrMsg = "Rank name already exist!";

        public const string validateRankId = "Rank doesn't exist!";

        public const string validateCountryId = "Country doesn't exist!";

        public const string validateCityId = "City doesn't exist!";

        public const string minLength = "Minimum length should be {1}!";

        public const string maxLength = "Maximum length should be {1}!";
    }
}
