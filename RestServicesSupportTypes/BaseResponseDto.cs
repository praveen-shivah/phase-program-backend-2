using System.Net;

namespace RestServicesSupportTypes
{
    public enum ResponseTypeEnum
    {
        notSet,
        success,
        cannotReach3rdParty,
        cannotAuthenticateWith3rdParty,
        errorCalling3rdParty,
        invalidResponseFrom3rdParty,
        idNotFound,
        databaseError,
        gatewayUnavailable,
        notLoggedIn,
        outOfRangeError,
        serviceBusy,
        hardwareError,
        printerError,
        invalidObjectReturned,
        exceptionOccurred
    }

    public class BaseResponseDto
    {
        public bool IsSuccessful { get; set; }
        
        public HttpStatusCode HttpStatusCode { get; set; }

        public ResponseTypeEnum ResponseTypeEnum { get; set; }

        public string ErrorMessage { get; set; }
    }
}
