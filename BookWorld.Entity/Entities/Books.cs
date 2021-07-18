
using System.Collections.Generic;

namespace BookWorld.Entity.Entities
{
    public class Books : BaseEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }

        public virtual ICollection<UserBooks> Relation2 { get; set; }
    }
}