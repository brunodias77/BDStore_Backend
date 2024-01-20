using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDStore.Domain.Users.ValueObjects
{
    public class UserId
    {
        public Guid Value { get; private set; }

        public UserId() { }
        public UserId(Guid value)
        {
            Value = value;
        }
    }
}