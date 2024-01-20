using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDStore.Domain.Users.ValueObjects
{
    public class FirstName
    {
        public string Value { get; private set; }

        // Adicione um construtor padrão sem parâmetros (necessário para Entity Framework Core)
        private FirstName() { }

        // Use esse construtor para criar instâncias de FirstName
        public FirstName(string value)
        {
            Value = value;
        }
    }
}