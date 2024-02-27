namespace DSU24_Grupp5.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }

        public HttpResponseMessage Response { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsSuccessful => string.IsNullOrEmpty(ErrorMessage);
    }
}
