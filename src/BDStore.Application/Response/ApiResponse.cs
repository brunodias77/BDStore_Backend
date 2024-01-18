namespace BDStore.Application.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }

        //Construtor para sucesso
        public ApiResponse(T data, string message = "")
        {
            this.Success = true;
            this.Message = message;
            this.Data = data;
        }

        //Construtir para falha
        public ApiResponse(string message)
        {
            this.Success = false;
            this.Message = message;
            this.Data = default(T);
        }
    }
}