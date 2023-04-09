using System.Collections.Generic;

namespace damn.Classes
{
    public class User : IHasList<User>
    {
        public readonly static List<User> Users = new List<User>();

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }

        public User() { }

        public User(int id, string login, string password, string name, string phone, int roleid)
        {
            Id = id;
            Name = name;
            Password = password;
            Login = login;
            RoleID = roleid;
            Phone = phone;
        }

        public List<User> GetList()
        {
            return Users;
        }
    }
}
