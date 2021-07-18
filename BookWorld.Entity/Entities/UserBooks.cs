
namespace BookWorld.Entity.Entities
{
    public class UserBooks:BaseEntity
    {
        public int UserID { get; set; }
        public int BookID { get; set; }

        public virtual Users UserRelation { get; set; }
        public virtual Books BookRelation { get; set; }
    }
}
