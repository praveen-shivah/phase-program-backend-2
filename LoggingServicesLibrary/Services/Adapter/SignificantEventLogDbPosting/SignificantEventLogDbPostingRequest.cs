namespace LoggingServicesLibrary
{
    using LoggingLibraryTypes;

    public class SignificantEventLogDbPostingRequest
    {
        public SignificantEventLogDbPostingRequest(int createdBy,
                                                   SignificantEventType significantEventType,
                                                   string shortDescription,
                                                   string longDescription)
        {
            this.CreatedBy = createdBy;
            this.SignificantEventType = significantEventType;
            this.ShortDescription = shortDescription;
            this.LongDescription = longDescription;
        }

        public int CreatedBy { get; }
        public string LongDescription { get; }
        public string ShortDescription { get; }
        public SignificantEventType SignificantEventType { get; }
    }
}