using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinPerformanceTest
{
    public class Contact
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Number { get; set; }

        public Contact(string firstName, string lastName, string number)
        {
            FirstName = firstName;
            LastName = lastName;
            Number = number;
        }
    }
}
