using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI.WebControls;

namespace OnlineStore.Reusorces
{
    public static class ErrorMessage
    {
        public const string CategoryIdNotSpecified = "Category id is not specified";

        public const string CategoryDoesNotExist = "Chosen category does not exist";

        public const string NoCategoryParameterProvided = "No category id parameter provided";

        public const string SizeIdNotSpecified = "Size id is not specified";

        public const string SizeDoesNotExist = "Chosen size does not exist";

        public const string ProductIdNotSpecified = "Product id is not specified";

        public const string ProductDoesNotExist = "Chosen product does not exist";

        public const string UserIdNotSpecified = "User id is not specified";

        public const string UserDoesNotExist = "Chosen user does not exist";

        public const string CartDoesNotExist = "Chosen cart does not exist";

        public const string CartItemIdNotSpecified = "Cart item id is not specified";

        public const string CartItemDoesNotExist = "Chosen cart item does not exist";
    }
}