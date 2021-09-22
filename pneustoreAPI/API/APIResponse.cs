namespace pneustoreAPI.API
{
    public class APIResponse<T>
    {
        public string Message { get; set; }
        public bool Succeed { get; set; }
        public T Results { get; set; }
    }
}