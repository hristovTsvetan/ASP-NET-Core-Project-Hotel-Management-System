using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Validators.Messages
{
    public static class ValidatorConstants
    {
        public const string validateRankNameErrMsg = "Rank name already exist!";

        public const string validateRoomName = "Room name lready exist!";

        public const string validateRankId = "Rank doesn't exist!";

        public const string validateCountryId = "Country doesn't exist!";

        public const string validateCityId = "City doesn't exist!";

        public const string validateIdentityId = "Idnetity card already exist!";

        public const string minLength = "Minimum length should be {1}!";

        public const string maxLength = "Maximum length should be {1}!";

        public const string phone = "Phone can contain only numbers, spaces and +!";

        public const string roomType = "Room name already exist, use different name!";
    }
}
