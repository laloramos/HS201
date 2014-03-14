namespace HS201_FinalAssignment.Domain.Entities
{
    public class Session
    {
        public virtual int Id { get; set; }
        public virtual string Title { get;  set; }
        public virtual string Abstract { get;  set; }
        public virtual Speaker Speaker { get;  set; }
    }
}