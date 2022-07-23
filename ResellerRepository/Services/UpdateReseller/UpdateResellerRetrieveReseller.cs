namespace ResellerRepository
{
    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    using ResellerRepositoryTypes;

    public class UpdateResellerRetrieveReseller : IUpdateReseller
    {
        private readonly IUpdateReseller updateReseller;

        public UpdateResellerRetrieveReseller(IUpdateReseller updateReseller)
        {
            this.updateReseller = updateReseller;
        }

        async Task<UpdateResellerResponse> IUpdateReseller.UpdateResellerAsync(DPContext dpContext, UpdateResellerRequest request)
        {
            var response = await this.updateReseller.UpdateResellerAsync(dpContext, request);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var organization = await dpContext.Organization.SingleAsync(o => o.Id == request.OrganizationId);
            var Reseller = await dpContext.Reseller.SingleOrDefaultAsync(x => x.Id == request.ResellerDto.Id);
            if (Reseller == null)
            {
                Reseller = new Reseller()
                {
                    Name = request.ResellerDto.Name,
                    Organization = organization
                };
                dpContext.Reseller.Add(Reseller);
                await dpContext.SaveChangesAsync();
            }

            response.Reseller = Reseller;

            return response;
        }
    }
}
