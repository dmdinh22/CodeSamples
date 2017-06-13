using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dinh.Mvc.Domain
{
    public class Author
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
    }
}