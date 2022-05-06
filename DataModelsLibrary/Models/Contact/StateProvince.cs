namespace DataModelsLibrary
{
    public class StateProvince : BaseOrganizationEntity
    {
        public Country Country { get; set; }
        public string Name { get; set; }
    }
}
