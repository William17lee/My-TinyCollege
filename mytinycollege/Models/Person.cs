using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mytinycollege.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FristMidName { get; set; }
        public string Email { get; set; }

        public string FullName
        {
            get
            {
                return LastName + ", " + FristMidName;
            }
        }

    }
}