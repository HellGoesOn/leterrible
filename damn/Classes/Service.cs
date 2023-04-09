using System.Collections.Generic;

namespace damn.Classes
{
    public class Service : IHasList<Service>
    {
        public readonly static List<Service> Services = new List<Service>();

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Service() { }

        public Service(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public List<Service> GetList()
        {
            return Services;
        }
    }
}
