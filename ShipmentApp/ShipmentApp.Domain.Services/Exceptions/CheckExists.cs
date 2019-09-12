using System.Net;

namespace ShipmentApp.Domain.Services.Exceptions
{
    public static class CheckExists
    {
        public static void EnsureExists(this object entity)
        {
            if(entity == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound);
            }
        }
    }
}
