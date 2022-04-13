namespace LoggingServicesLibrary
{
    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    public class SignificantEventLogDbPostingSave : ISignificantEventLogDbPosting
    {
        private readonly ISignificantEventLogDbPosting significantEventLogDbPosting;

        public SignificantEventLogDbPostingSave(ISignificantEventLogDbPosting significantEventLogDbPosting)
        {
            this.significantEventLogDbPosting = significantEventLogDbPosting;
        }

        SignificantEventLogDbPostingResponse ISignificantEventLogDbPosting.Post(DPContext dataContext, SignificantEventLogDbPostingRequest significantEventLogDbPostingRequest)
        {
            var response = this.significantEventLogDbPosting.Post(dataContext, significantEventLogDbPostingRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var significantEvent = new SignificantEvent
            {
                CreatedBy = significantEventLogDbPostingRequest.CreatedBy,
                EventTypeId = (int)significantEventLogDbPostingRequest.SignificantEventType,
                ShortDescription = significantEventLogDbPostingRequest.ShortDescription,
                LongDescription = significantEventLogDbPostingRequest.LongDescription
            };
            dataContext.SignificantEvent.Add(significantEvent);

            return response;
        }
    }
}