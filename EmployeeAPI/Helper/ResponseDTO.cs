
namespace EmployeeAPI.Helper
{
    public class ResponseDTO<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Result { get; set; }
    }
}
