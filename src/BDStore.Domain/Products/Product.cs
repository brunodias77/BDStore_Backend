using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDStore.Domain.Abstraction;

namespace BDStore.Domain.Products
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Value { get; set; }
        public DateTime DateAdded { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
    }
}