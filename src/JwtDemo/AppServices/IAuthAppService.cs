namespace JwtDemo.AppServices
{
    public interface IAuthAppService
    {
        AuthClientResult AuthClient(AuthClientVo vo);
    }

    public class AuthClientVo
    {
        public string ClientId { get; set; }
        public string ClientConnectKey { get; set; }
    }

    public class AuthClientResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
