namespace ResellerRepository
{
    using DatabaseContext;

    using ResellerRepositoryTypes;

    public class UpdateResellerUpdate : IUpdateReseller
    {
        private readonly IUpdateReseller updateReseller;

        public UpdateResellerUpdate(IUpdateReseller updateReseller)
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

            response.Reseller.Name = request.ResellerDto.Name;

            return response;
        }
    }
}
