namespace DataLayer.Response
{
    public class EServiceResponseTypes
    {
        public enum EResponseType
        {
            Success,
            NotFound,
            CannotCreate,
            CannotUpdate,
            CannotDelete,
            Forbid
        }
    }
}
