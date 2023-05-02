using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDTO.Models
{
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
