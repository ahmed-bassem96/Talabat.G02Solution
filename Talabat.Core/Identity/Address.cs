namespace Talabat.Core.Identity
{
    public class Address
    {
        public int Id { get; set; }

        public string Fname { get; set; }

        public string Lname { get; set; }

        public string street { get; set; }

        public string city { get; set; }

        public string Country { get; set; }

        public string AppUserId { get; set; }//FK
        public AppUser User { get; set; }
    }
}