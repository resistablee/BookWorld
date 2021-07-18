using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookWorld.Entity.Entities
{
    public class Users : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserBooks> Relation1 { get; set; }
    }
}