using static DataLayer.Response.EServiceResponseTypes;

namespace DataLayer.Response
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public EResponseType ResponseType { get; set; }
        public string Message { get; set; } = null;
    }
}
