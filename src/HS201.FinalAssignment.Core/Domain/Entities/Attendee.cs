namespace HS201.FinalAssignment.Core.Domain.Entities
{
    public class Attendee
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
    }
}