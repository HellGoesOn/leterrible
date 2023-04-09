using System.Collections.Generic;

namespace damn.Classes
{
    public class Role : IHasList<Role>
    {
        public readonly static List<Role> Roles = new List<Role>();

        public int Id { get; set; }
        public string Name { get; set; }

        public Role() { }

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public List<Role> GetList()
        {
            return Roles;
        }
    }
}
