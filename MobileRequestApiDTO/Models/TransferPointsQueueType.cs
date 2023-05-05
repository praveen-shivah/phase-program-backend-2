namespace ApiDTO
{
    using System.ComponentModel;
    public enum TransferPointsQueueTypeEnum
    {
        none = 1,

        [Description("org to reseller")]
        orgToReseller = 2,

        [Description("reseller to player")]
        resellerToPlayer = 3,

        [Description("reseller to org")]
        resellerToOrg = 4,

    }
}
