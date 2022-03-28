using System.Collections.Generic;

namespace Swimming_Club_2.Models
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<User> usersWithThisRole = new List<User>();
    }
}
