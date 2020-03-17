namespace VueShopServer.Api.Module
{
    public class ApiResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Result { get; set; }
    }
}
