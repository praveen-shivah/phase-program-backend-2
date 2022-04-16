namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class ContactPersonsDetail : BaseEntity
    {
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string LastName { get; set; }
        public string ContactPersonId { get; set; }
        public bool IsPrimaryContact { get; set; }
        public string PhotoUrl { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}