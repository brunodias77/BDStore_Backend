using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDStore.Domain.Users.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }

        public Email() { }
        public Email(string value)
        {
            Value = value;
        }
    }
}