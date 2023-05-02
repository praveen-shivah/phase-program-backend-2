namespace DataSeedingLibrary
{
    using ApiDTO.Models;
    using AuthenticationRepositoryTypes;

    using DatabaseContext;
    using Microsoft.EntityFrameworkCore;

    public class SeedDataAddTransferPointsQueueTypes : ISeedData
    {
        private readonly ISeedData seedData;

        public SeedDataAddTransferPointsQueueTypes(ISeedData seedData)
        {
            this.seedData = seedData;
        }

        async Task<SeedDataResponse> ISeedData.SeedAsync(DataContext context, SeedDataRequest seedDataRequest)
        {
            var response = await this.seedData.SeedAsync(context, seedDataRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var transferPointsQueueTypeValues = Enum.GetValues(typeof(TransferPointsQueueTypeEnum));
            foreach (int transferPointsQueueTypeValue in transferPointsQueueTypeValues)
            {
                var name = Enum.GetName(typeof(TransferPointsQueueTypeEnum), transferPointsQueueTypeValue);
                if (name == null)
                {
                    continue;
                }

                var transferPointsQueueType = await context.TransferPointsQueueType.SingleOrDefaultAsync(x => x.Id == transferPointsQueueTypeValue);
                if (transferPointsQueueType != null)
                {
                    continue;
                }

                context.TransferPointsQueueType.Add(
                    new TransferPointsQueueType
                    {
                        Id = transferPointsQueueTypeValue,
                        Name = name
                    });
            }

            return response;
        }
    }
}
