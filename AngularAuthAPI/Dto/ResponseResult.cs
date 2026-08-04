namespace AngularAuthAPI.Dto
{
    public class ResponseResult
    {
        public object Data { get; set; }
        public ResponseStatus Result { get; set; }
        public string message { get; set; }
        public enum ResponseStatus
        {
            Error,
            Success
        }
    }
}
