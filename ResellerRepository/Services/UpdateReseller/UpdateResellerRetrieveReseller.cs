namespace ResellerRepository
{
    using CommonServices;

    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    using ResellerRepositoryTypes;

    public class UpdateResellerRetrieveReseller : IUpdateReseller
    {
        private readonly IUpdateReseller updateReseller;

        public UpdateResellerRetrieveReseller(IUpdateReseller updateReseller)
        {
            this.updateReseller = updateReseller;
        }

        async Task<UpdateResellerResponse> IUpdateReseller.UpdateResellerAsync(DataContext dataContext, UpdateResellerRequest request)
        {
            var response = await this.updateReseller.UpdateResellerAsync(dataContext, request);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var organization = await dataContext.Organization.SingleAsync(o => o.Id == request.OrganizationId);
            var reseller = await dataContext.Reseller.SingleOrDefaultAsync(x => x.Id == request.ResellerDto.Id);
            if (reseller == null)
            {
                reseller = new Reseller()
                {
                    Name = request.ResellerDto.Name,
                    Organization = organization
                };
                dataContext.Reseller.Add(reseller);
                await dataContext.SaveChangesAsync();
            }

            response.Reseller = reseller;

            return response;
        }
    }
}
