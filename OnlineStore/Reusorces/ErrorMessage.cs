﻿using System;
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

        public const string CategoryDoesNotExist = "Choosen category does not exist";

        public const string NoCategoryParameterProvided = "No category id parameter provided";

        public const string SizeIdNotSpecified = "Size id is not specified";

        public const string SizeDoesNotExist = "Choosen size does not exist";

        public const string ProductIdNotSpecified = "Product id is not specified";

        public const string ProductDoesNotExist = "Choosen product does not exist";
    }
}