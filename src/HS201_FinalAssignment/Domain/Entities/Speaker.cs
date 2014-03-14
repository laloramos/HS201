namespace HS201_FinalAssignment.Domain.Entities
{
    public class Speaker
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get;  set; }
        public virtual string LastName { get;  set; }
        public virtual string Bio { get; set; }
    }
}