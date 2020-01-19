namespace galdino.bloodOrange.api_v1.Models.Base
{
    public class BaseViewModel<T> where T : new()
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
        public long ProcessingTimer { get; set; } = 0;
        public T ObjectReturn { get; set; } = new T();
        public string Version { get; set; } = "V 1.0";

        public void GerarRetornoErro(T objeto, string mensagem)
        {
            Message = mensagem;
            Success = false;
        }
    }
}
