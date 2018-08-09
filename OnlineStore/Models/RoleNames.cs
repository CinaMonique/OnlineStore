using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class RoleNames
    {
        public const string Manager = "MANAGER";

        public const string Admin = "ADMIN";

        public const string User = "USER";

        public const string ManagerOrAdmin = Manager + "," + Admin;
    }
}