namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class Organization : BaseEntity
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string APIKey { get; set; }
    }
}
