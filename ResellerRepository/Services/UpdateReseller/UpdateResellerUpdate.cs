namespace ResellerRepository
{
    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    using ResellerRepositoryTypes;

    public class UpdateResellerUpdate : IUpdateReseller
    {
        private readonly IUpdateReseller updateReseller;

        public UpdateResellerUpdate(IUpdateReseller updateReseller)
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

            response.Reseller.Name = request.ResellerDto.Name;

            return response;
        }
    }
}
