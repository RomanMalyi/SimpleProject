using System;

namespace SimpleProject.Data.Models
{
    public class Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Value { get; set; }
    }
}
