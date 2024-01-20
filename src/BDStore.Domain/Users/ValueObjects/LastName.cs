using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDStore.Domain.Users.ValueObjects
{
    public class LastName
    {
        public string Value { get; private set; }

        public LastName(string value)
        {
            Value = value;
        }

        public LastName() { }
    }
}