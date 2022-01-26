using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation_With_WebApi.Entity
{
    public class UserMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }

    }
}