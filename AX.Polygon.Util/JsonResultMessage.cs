namespace AX.Polygon.Util
{
    public class JsonResultMessage
    {
        /// <summary>
        /// 操作结果
        /// 1代表成功，
        /// 0代表失败
        /// -999 代表全局拦截异常
        /// </summary>
        public int Code { get; set; }

        public string Message { get; set; }
    }

    public class JsonResultMessage<T> : JsonResultMessage
    {
        public T Data { get; set; }
    }
}