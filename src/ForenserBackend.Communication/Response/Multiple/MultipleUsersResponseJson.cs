using ForenserBackend.Communication.Response.Short;

namespace ForenserBackend.Communication.Response.Multiple
{
    public class MultipleUsersResponseJson
    {
        public List<ShortUserResponseJson> Users { get; set; } = [];
    }
}
